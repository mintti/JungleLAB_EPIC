using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartPanel : MonoBehaviour
{
    [SerializeField] private int _value;
    public void OnTileEvent()
    {
        gameObject.SetActive(true);  
    }

    public void B_Execute()
    {
        EventExecutor();
    }


    public void EventExecutor()
    {
        // [TODO] 플레이어 체력 회복
        GameManager.Log.Log("시작 지점 효과로 체력이 회복됩니다.");
        GameManager.Player.Heal(_value);
        GameManager.Player.ShowCardPanels();
        gameObject.SetActive(false);
    }
}
