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

    public void Init(int index,int count,int level)
    {
        _index = index;
        _lifeCount = count;
        _level = level;
        BoardManager.I.AddSummon(GetComponent<SummonCat>());
        GameManager.Player.OnMove += Move;
        GameManager.Player.OnTileAction += OnAction;
    }

    public void DestroyCat()
    {
        GameManager.Player.OnMove -= Move;
        GameManager.Player.OnTileAction -= OnAction;
        BoardManager.I.DeleteSummon(this);
        Destroy(gameObject);
    }

    public IEnumerator Move(int value)
    {
        for(int i = 0; i < value; i++)
        {
            _index = BoardManager.I.GetPrevIndex(_index);
            Vector3 nextPos = BoardManager.I.GetTilePos(_index);
            transform.DOMove(nextPos, 0.5f);
            yield return new WaitForSeconds(0.5f);

            if (BoardManager.I.OnPassCat(_index, this))
            {
                yield break;
            }
        }
    }

    public void OnAction(int _)
    {
        if (BoardManager.I.tiles[_index] is AttackTile 
            || BoardManager.I.tiles[_index] is DefenseTile)
        {
            BoardManager.I.tiles[_index].OnAction(_level);
        }
        
    }
    public IEnumerator Connect()
    {
        yield break;
    }

    public IEnumerator OnTurnEnd()
    {
        _lifeCount -= 1;
        if (_lifeCount <= 0)
        {
            DestroyCat();
           
        }
        yield break;
    }

    public void OnEvent()
    {
       
    }

    public void OnPass()
    {
        
    }
}
