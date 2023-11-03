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
        // ResetCount
    }

    private void ResetCount()
    {
        RemainingCount = 2;
    }
    
    
    public void OnTileEvent()
    {
        gameObject.SetActive(true);   
    }
    
    public void B_Execute() => StartCoroutine(EventExecutor());


    public IEnumerator EventExecutor()
    {
        if (RemainingCount > 0) 
        {
            RemainingCount--;
            GameManager.Log.Log("���� ���� ���� (���� �ʿ�)");
        
            // [TODO] ü�� 
            // [TODO] ��ο� ī�� 1��
        }
        else
        {
            GameManager.Log.Log("���� ���� ��� Ƚ���� �����Ͽ� �̿� �� �� ����");
        }
        
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);   
    }
}