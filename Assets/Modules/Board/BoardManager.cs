using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

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
    public GameObject fireball;
    public GameObject fireBreath;
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

    public Vector3 GetTilePos(int index)
    {
        return tiles[index].transform.position;
    }

    public void AddSummon(ISummon s)
    {
        summons.Add(s);
    }
}
