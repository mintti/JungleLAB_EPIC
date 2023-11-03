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
            altarCountTMP.text = $"남은 횟수 {_remainingCount}";
        }
    }

    /// <summary>
    /// [TODO] 한 바퀴 돈 후 실행되는 함수에 구독 필요
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
            GameManager.Log.Log("피의 제단 실행 (로직 필요)");
        
            // [TODO] 체력 
            // [TODO] 드로우 카드 1장
        }
        else
        {
            GameManager.Log.Log("피의 제단 사용 횟수를 소진하여 이용 할 수 없음");
        }
        
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);   
    }
}