using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TH.Core;

public class AttackTile : BaseTile
{
    public override void OnAction(int num)
    {
        GameManager.Boss.HpUpdate(num);
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
        if (_isCurse)
        {
            _curseTurnCount -= 1;

            if (_curseTurnCount == 0)
            {
                OffCurse();
                GameObject fireballPrefab = GameManager.Resource.LoadPrefab(ResourceManager.Prefabs.SUMMON_FIREBALL);
                GameObject s=Instantiate(fireballPrefab, transform.position, Quaternion.identity);
                s.GetComponent<Fireball>().Init(index);
                BoardManager.I.AddSummon(s.GetComponent<ISummon>());
            }
        }
        

    }
    public override void OnCurse(int count)
    {
        Sprite curseSprite = GameManager.Resource.LoadSprite(ResourceManager.Sprites.TILE_CURSE);
        GetComponent<SpriteRenderer>().sprite = curseSprite;
        _isCurse = true;
        _curseTurnCount = count;
    }

    public override void OffCurse()
    {
        _isCurse = false;
        Sprite attackSprite = GameManager.Resource.LoadSprite(ResourceManager.Sprites.TILE_ATTACK);
        GetComponent<SpriteRenderer>().sprite = attackSprite;
    }
}
