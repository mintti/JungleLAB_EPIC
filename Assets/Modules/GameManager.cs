using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TH.Core;

public class GameManager : Singleton<GameManager>
{
    #region Inner Class
    private enum ManagerType {
		LOG,
        CARD
	}
    #endregion

    #region Managers Core
    private static GameData _data;
    public static GameData Data {
        get {
            if (_data == null) {
                _data = I.GetComponent<GameData>();
            }
            return _data;
        }
    }

    public static Player Player {
        get {
            return FindAnyObjectByType<Player>();
        }
    }

    public static LogManager Log {
		get {
			return _managerList[ManagerType.LOG] as LogManager;
		}
	}

    public static CardManager Card {
        get {
            return _managerList[ManagerType.CARD] as CardManager;
        }
    }

    private static Dictionary<ManagerType, IManager> _managerList = new Dictionary<ManagerType, IManager> {
		{ManagerType.LOG, new LogManager()},
        {ManagerType.CARD, new CardManager()},
	};

    private void InitManagers() {
        foreach (var manager in _managerList) {
			manager.Value.Init();
		}
    }

    protected override void Init() {
        base.Init();

        // [TODO] 게임 매니저 초기화
        

        // 다른 매니저들 초기화
        InitManagers();
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
        Log.Log("게임 시작");

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
    
    public IEnumerator Timer(int time, Action endAction = null)
    {
        yield return new WaitForSeconds(time);
        endAction?.Invoke();
    }
    #endregion
}
