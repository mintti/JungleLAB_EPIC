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
    [ShowInInspector] public List<ISummon> summons = new();
    private List<int> deleteSummonsIndexes = new();

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
            if (s == null)
            {
                continue;
            }
            yield return s.OnTurnEnd();
        }
        UpdateSummons();
    }

    public void OnEvent(int index)
    {
        if(tiles[index] is not AttackTile && tiles[index] is not DefenseTile)
        {
            tiles[index].OnAction();
        }

        if (tiles[index].IsCurse)
        {
            tiles[index].OffCurse();
        }

        foreach(ISummon s in summons)
        {
            if (s == null)
            {
                continue;
            }

            if (s.Index == index)
            {
                s.OnEvent();
            }
        }
        UpdateSummons();

    }

    public void OnPass(int index)
    {
        tiles[index].debuff?.OnDebuff();
        Debug.Log(tiles[index].debuff);
        foreach(ISummon s in summons)
        {
            if (s == null)
            {
                continue;
            }

            if (s.Index == index)
                s.OnPass();
        }
        UpdateSummons();
    }

   /* public bool OnPassCat(int index,SummonCat c)
    {
        foreach (ISummon s in summons)
        {
            if (s.Index == index
                && s is Fireball)
            {
                Fireball fb = (Fireball)s;
                fb.DestroyFireball();
                c.DestroyCat();
                return true;
            }
        }
        
        UpdateSummons();
        return false;
    }
*/
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
        int index=summons.IndexOf(s);
        deleteSummonsIndexes.Add(index);
    }
    private void UpdateSummons()
    {
        for(int i = 0; i < deleteSummonsIndexes.Count; i++)
        {
            summons[deleteSummonsIndexes[i]] = null;
        }
        summons.RemoveAll(item => item == null);
        deleteSummonsIndexes.Clear();
    }

    public BaseTile GetTile(int index)
    {
        return tiles[index];
    }
}
