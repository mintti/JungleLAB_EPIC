using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager I { get; private set; }
    private void Awake()
    {
        if (I == null)
        {
            I = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    
    [Header("Game Flow Variable")]
    [SerializeField] private bool _gameEnd;
    
    public void B_Start()
    {
        // Init Data
        
        // 게임 시작
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        Debug.Log("게임 시작");
        
        _gameEnd = false;
        do
        {
            yield return PlayerEvent();
            yield return BossEvent();
            
            // 각 행동 종료 및 턴 증가
            yield return BoardEvent();
            
        } while (!_gameEnd);
    }

    IEnumerator PlayerEvent()
    {
        // [TODO] 플레이어 턴 시작 전달
        yield return WaitNext(); 
    }
    
    IEnumerator BossEvent()
    {
        // [TODO] 보스 턴 시작 전달
        yield return WaitNext(); 
    }
    
    IEnumerator BoardEvent()
    {
        // [TODO] 보드 설정 시작 전달
        yield return WaitNext(); 
    }
    
    
    
    #region Util
    private bool _next;
    public IEnumerator WaitNext()
    {
        _next = false;
        yield return new WaitUntil(() => _next);
    }

    public void Next()
    {
        _next = true;
    }

    public bool Record()
    {
        return false;
    }

    public void Log(string log, float time = 1f)
    {
        //UIManager.I.OutputInfo(log, time);
        Debug.Log(log);
    }
    
    public IEnumerator Timer(int time, Action endAction = null)
    {
        yield return new WaitForSeconds(time);
        endAction?.Invoke();
    }
    #endregion
}
