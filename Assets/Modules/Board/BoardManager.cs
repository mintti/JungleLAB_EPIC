using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class BoardManager : Singleton<BoardManager>
{
    private int _playerOnIndex;
    public int PlayerOnIndex
    {
        get => GameManager.Player.Position;
    }
    public List<BaseTile> tiles = new();
    public List<ISummon> summons = new();
    private void Start()
    {
        Init();
    }

    [Button]
    public void TestTurnEnd()
    {
        StartCoroutine(OnTurnEnd());
    }
    public IEnumerator OnTurnEnd()
    {
        foreach(BaseTile t in tiles)
        {
            t.OnTurnEnd();
        }

        foreach(ISummon s in summons)
        {
            yield return s.OnTurnEnd();
        }
    }

    protected override void Init()
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

    public int GetPrevIndex(int index)
    {
        int prevIndex = index - 1;
        if (prevIndex < 0)
        {
            prevIndex = tiles.Count-1;
        }
        return prevIndex;
    }

    public Vector3 GetTilePos(int index)
    {
        return tiles[index].transform.position;
    }

    public void AddSummon(ISummon s)
    {
        summons.Add(s);
    }

    public void DeleteSummon(ISummon s)
    {
        summons.Remove(s);
    }

    public BaseTile GetTile(int index)
    {
        return tiles[index];
    }
}
