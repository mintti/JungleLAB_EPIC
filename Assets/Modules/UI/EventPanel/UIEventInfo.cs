using System;
using System.Collections;
using System.Collections.Generic;
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
                new("���� �������� �ɾ �̵��մϴ�.", WalkToStartTile),
                new("���� ī�带 2�� ��ο� �մϴ�.", Draw2Card),
                new("������ ���� ��ȭ�� 2�ϵ��� �̴ϴ�.", Week3ToEnemy),
                new("������ 3ĭ �̵��մϴ�.", Move3),
                new("�п� �ִ� ���� ī�� �ϳ��� ���ڸ� 2�� ���ݴϴ�.", Multiple2),
                new("�п� �ִ� ���� ī�� �ϳ��� �������ݴϴ�.", Copy),
                new("�������� 1�� ȹ���մϴ�.", GetMagicCircle1),
                new("���ϴ� �������� �����̵� �մϴ�. (�̺�Ʈ ����)", MoveToWantedTile),
                new("���� ī�带 ���ϴ� ��ŭ ������, ������ŭ �̽��ϴ�.", DropNDrawN),
            };

            return _events;
        }   
    }
    
    class Event
    {
        public string Name { get; }
        public Func<IEnumerator> Action { get; }

        public Event(string name, Func<IEnumerator> action)
        {
            Name = name;
            Action = action;
        }
    }

    IEnumerator EventExecutor()
    {
        // ���� �̺�Ʈ ����
        var idx = Random.Range(0, Events.Count);
        var evt = Events[idx];
        
        eventInfoTMP.text = evt.Name;

        // ����
        yield return evt.Action;
        
        // 1�� �� �׼� ����
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
    
    IEnumerator Blank()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator WalkToStartTile()
    {
        yield return null;
    }

    IEnumerator Draw2Card()
    {
        // GameManager.Card.DrawCards(2);
        yield return null;
    }

    IEnumerator Week3ToEnemy()
    {
        yield return null;
    }

    IEnumerator Move3()
    {
        yield return null;
    }

    IEnumerator Multiple2()
    {
        yield return null;
    }

    IEnumerator Copy()
    {
        yield return null;
    }

    IEnumerator GetMagicCircle1()
    {
        yield return null;
    }

    IEnumerator MoveToWantedTile()
    {
        yield return null;
    }
    
    IEnumerator DropNDrawN()
    {
        yield return null;
    }


}