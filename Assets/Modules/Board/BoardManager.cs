using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    private int _playerOnIndex;
    public List<BaseTile> tiles = new();
    public List<ISummon> summons = new();

    private void OnTurnEnd()
    {
        foreach(BaseTile t in tiles)
        {
            t.OnTurnEnd();
        }

        foreach(ISummon s in summons)
        {
            s.OnTurnEnd();
        }
    }

    private void Init()
    {
        
    }

    private void GetNextTile()
    {

    }
}
