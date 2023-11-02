using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    private int _playerOnIndex;
    public int PlayerOnIndex
    {
        get => _playerOnIndex;
    }
    [SerializeField] private int _lineTileCount;
    public List<BaseTile> tiles = new();
    public List<ISummon> summons = new();

    private void Start()
    {
        Init();
    }
    public void OnTurnEnd()
    {
        foreach(BaseTile t in tiles)
        {
            t.OnTurnEnd();
        }

        foreach(ISummon s in summons)
        {
            StartCoroutine(s.OnTurnEnd());
        }
    }

    private void Init()
    {
        for(int i = 0; i < tiles.Count; i++)
        {
            tiles[i].index = i;
        }
    }

    public int GetNextIndex(int index)
    {
        int nextIndex = index + 1;
        if (nextIndex >= tiles.Count)
        {
            nextIndex = 0;
        }

        return nextIndex;
    }

    public Vector3 GetTilePos(int index)
    {
        return tiles[index].transform.position;
    }

    public void AddSummon(ISummon s)
    {
        summons.Add(s);
    }
}
