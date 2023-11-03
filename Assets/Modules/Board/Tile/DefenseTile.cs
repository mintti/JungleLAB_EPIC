using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TH.Core;

public class DefenseTile : BaseTile
{
    public override void OnAction(int num)
    {
        GameManager.Player.Defence(num);
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
    [Button]
    public override void OnCurse(int count)
    {
        Sprite curseSprite = GameManager.Resource.LoadSprite(ResourceManager.Sprites.TILE_CURSE);
        GetComponent<SpriteRenderer>().sprite = curseSprite;
        _isCurse = true;
        _curseTurnCount = count;
    }

    public override void OffCurse()
    {
        Sprite defenseSprite = GameManager.Resource.LoadSprite(ResourceManager.Sprites.TILE_DEFENSE);
        GetComponent<SpriteRenderer>().sprite = defenseSprite;
        _isCurse = false;
    }
}
