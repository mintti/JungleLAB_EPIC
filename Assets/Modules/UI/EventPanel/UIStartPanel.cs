using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStartPanel : MonoBehaviour
{
    private bool _wait;
    public void OnTileEvent()
    {
        gameObject.SetActive(true);  
    }

    public void B_Execute()
    {
        if (!_wait)
        {
            StartCoroutine(EventExecutor());
        }
    }


    public IEnumerator EventExecutor()
    {
        _wait = true;
        
        // [TODO] 플레이어 체력 회복
        GameManager.Log.Log("시작 지점 효과로 체력이 회복됩니다.");
        
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
        _wait = false;
    }
}
