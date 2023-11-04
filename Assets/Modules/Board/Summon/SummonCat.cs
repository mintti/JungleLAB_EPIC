using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SummonCat : MonoBehaviour, ISummon
{
    private int _index;
    private int _lifeCount;
    private int _level;

    public int Index => _index;

    public void Init(int count,int level)
    {
        _lifeCount = count;
        _level = level;
        BoardManager.I.AddSummon(GetComponent<SummonCat>());
        GameManager.Player.OnMove += Move;
    }

    public IEnumerator Move(int value)
    {
        for(int i = 0; i < value; i++)
        {
            _index = BoardManager.I.GetPrevIndex(_index);
            Vector3 nextPos = BoardManager.I.GetTilePos(_index);
            transform.DOMove(nextPos, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void OnAction()
    {
        BoardManager.I.tiles[_index].OnAction(_level);
    }
    public IEnumerator Connect()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator OnTurnEnd()
    {
        _lifeCount -= 1;
        if (_lifeCount <= 0)
        {
            GameManager.Player.OnMove -= Move;
            Destroy(this);
        }
        yield break;
    }

    public void OnEvent()
    {
        throw new System.NotImplementedException();
    }

    public void OnPass()
    {
        throw new System.NotImplementedException();
    }
}
