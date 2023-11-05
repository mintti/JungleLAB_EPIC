using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIAltarInfo : MonoBehaviour
{
    [Header("Component")]
    [SerializeField] private TextMeshProUGUI altarCountTMP;

    [SerializeField] private int _remainingCount;

    private int RemainingCount
    {
        get => _remainingCount;
        set
        {
            _remainingCount = value;
            altarCountTMP.text = $"���� Ƚ�� {_remainingCount}";
        }
    }

    /// <summary>
    /// [TODO] �� ���� �� �� ����Ǵ� �Լ��� ���� �ʿ�
    /// </summary>
    private void Start()
    {
        ResetCount();
        GameManager.Player.OneAroundEvent += ResetCount;
    }

    private void ResetCount()
    {
        RemainingCount = 2;
    }
    
    
    public void OnTileEvent()
    {
        gameObject.SetActive(true);   
    }
    
    public void B_Execute() => EventExecutor();
    
    public void B_Cancel() {
        gameObject.SetActive(false);
        GameManager.Player.ShowCardPanels();
    }

    public void EventExecutor()
    {
        if (RemainingCount > 0) 
        {
            RemainingCount--;
            GameManager.Log.Log("���� ���� ���� (���� �ʿ�)");

            GameManager.Player.Hit(5);
            GameManager.Card.CardDeck.DrawCard(1);
            GameManager.Card.UpdateUI();
        }
        else
        {
            GameManager.Log.Log("���� ���� ��� Ƚ���� �����Ͽ� �̿� �� �� ����");
        }  
    }
}