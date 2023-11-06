using System;
using System.Collections;
using System.Collections.Generic;
using TH.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;


public class UIEventInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI eventInfoTMP;
    [SerializeField] private Button eventDrawBtn;

    public void OnTileEvent()
    {
        gameObject.SetActive(true);
        eventInfoTMP.text = "랜덤 이벤트를 실행합니다.";
        eventDrawBtn.gameObject.SetActive(true);
    }

    public void B_Execute() => StartCoroutine(EventExecutor());
    
    [Header("Event")]
    [SerializeField] private List<Event> _events;

    List<Event> Events
    {
        get
        {
            _events ??= new()
            {
                new("꽝", Blank),
                new("시작 지점으로 걸어서 이동합니다.", WalkToStartTile, 1f),
                new("숫자 카드를 2장 드로우 합니다.", Draw2Card, 1f),
                new("적에게 취약과 약화를 2턴동안 겁니다.", Week3ToEnemy, 1f),
                new("앞으로 3칸 이동합니다.", Move3, 1f),
                new("패에 있는 숫자 카드 하나의 숫자를 2배 해줍니다.", Multiple2),
                new("패에 있는 숫자 카드 하나를 복제해줍니다.", Copy),
                new("마법진을 1개 획득합니다.", GetMagicCircle1, 1f),
                // new("원하는 지점으로 순간이동 합니다. (이벤트 제외)", MoveToWantedTile),
                new("숫자 카드를 원하는 만큼 버리고, 버린만큼 뽑습니다.", DropNDrawN),
            };

            return _events;
        }   
    }
    
    class Event
    {
        public string Name { get; }
        public Func<IEnumerator> Action { get; }
        public float WaitTime = 0;

        public Event(string name, Func<IEnumerator> action, float waitTime = 0)
        {
            Name = name;
            Action = action;
        }
    }

    IEnumerator EventExecutor()
    {
        eventDrawBtn.gameObject.SetActive(false);
        // 랜덤 이벤트 설정
        var idx = Random.Range(0, Events.Count);
        var evt = Events[idx];
        
        eventInfoTMP.text = evt.Name;

        // 수행
        yield return evt.Action();
        
        // 1초 후 액션 종료
        yield return new WaitForSeconds(evt.WaitTime);
        GameManager.Player.ShowCardPanels();
        gameObject.SetActive(false);
    }
    
    IEnumerator Blank()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator WalkToStartTile()
    {
        yield return GameManager.Player.Teleport(0);
    }

    IEnumerator Draw2Card()
    {
        GameManager.Card.CardDeck.DrawCard(2);
        GameManager.Card.UpdateUI();
        yield return null;
    }

    IEnumerator Week3ToEnemy()
    {
        GameManager.Boss.EventDebuffBoss(2, 2);
        yield return new WaitForSeconds(1f);
    }

    IEnumerator Move3()
    {
        yield return GameManager.Player.Move(3);
    }

    IEnumerator Multiple2()
    {
        bool hasDone = false;

        GameManager.Card.RequestCard(
            "카드 선택",
            "선택된 카드의 숫자를 2배로 만듭니다.",
            (type, value) => {
                Card newCard = new Card(new CardData(value * 2, type));
                GameManager.Card.CardDeck.AddCard(newCard);
                GameManager.Card.CardDeck.PutCardIntoHand(newCard);
                hasDone = true;
            },
            CardAfterUse.Remove,
            CardUseRestriction.JustOne,
            true,
            () => {
                hasDone = true;
            },
            CardRequestPosition.Left
        );

        yield return new WaitUntil(() => hasDone);
    }

    IEnumerator Copy()
    {
        bool hasDone = false;

        GameManager.Card.RequestCard(
            "카드 선택",
            "선택된 카드를 복제합니다.",
            (type, value) => {
                Card newCard = new Card(new CardData(value, type));
                GameManager.Card.CardDeck.AddCard(newCard);
                GameManager.Card.CardDeck.PutCardIntoHand(newCard);
                hasDone = true;
            },
            CardAfterUse.KeepToGraveyard,
            CardUseRestriction.JustOne,
            true,
            () => {
                hasDone = true;
            },
            CardRequestPosition.Left
        );

        yield return new WaitUntil(() => hasDone);
    }

    IEnumerator GetMagicCircle1()
    {
        PlayerMagic magic = GameManager.Player.Ability<PlayerMagic>();
        magic.AddCastingGauge(magic.MaxCastingCount);
        yield return null;
    }

    IEnumerator MoveToWantedTile()
    {
        yield return null;
    }
    
    IEnumerator DropNDrawN()
    {
        bool hasDone = false;

        GameManager.Card.RequestCard(
            "카드 선택",
            "선택된 카드를 버리고 새로 뽑습니다.\n"+
            "(여러 장 가능)",
            (type, value) => {
                GameManager.Card.CardDeck.DrawCard(1);
            },
            CardAfterUse.KeepToGraveyard,
            CardUseRestriction.Unlimited,
            true,
            () => {
                hasDone = true;
            },
            CardRequestPosition.Left
        );

        yield return new WaitUntil(() => hasDone);
    }
}