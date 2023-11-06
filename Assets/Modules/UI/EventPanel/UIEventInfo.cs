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
        eventInfoTMP.text = "���� �̺�Ʈ�� �����մϴ�.";
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
                new("��", Blank),
                new("���� �������� �ɾ �̵��մϴ�.", WalkToStartTile, 1f),
                new("���� ī�带 2�� ��ο� �մϴ�.", Draw2Card, 1f),
                new("������ ���� ��ȭ�� 2�ϵ��� �̴ϴ�.", Week3ToEnemy, 1f),
                new("������ 3ĭ �̵��մϴ�.", Move3, 1f),
                new("�п� �ִ� ���� ī�� �ϳ��� ���ڸ� 2�� ���ݴϴ�.", Multiple2),
                new("�п� �ִ� ���� ī�� �ϳ��� �������ݴϴ�.", Copy),
                new("�������� 1�� ȹ���մϴ�.", GetMagicCircle1, 1f),
                // new("���ϴ� �������� �����̵� �մϴ�. (�̺�Ʈ ����)", MoveToWantedTile),
                new("���� ī�带 ���ϴ� ��ŭ ������, ������ŭ �̽��ϴ�.", DropNDrawN),
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
        // ���� �̺�Ʈ ����
        var idx = Random.Range(0, Events.Count);
        var evt = Events[idx];
        
        eventInfoTMP.text = evt.Name;

        // ����
        yield return evt.Action();
        
        // 1�� �� �׼� ����
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
            "ī�� ����",
            "���õ� ī���� ���ڸ� 2��� ����ϴ�.",
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
            "ī�� ����",
            "���õ� ī�带 �����մϴ�.",
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
            "ī�� ����",
            "���õ� ī�带 ������ ���� �̽��ϴ�.\n"+
            "(���� �� ����)",
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