using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TH.Core;

public class AttackTile : BaseTile
{
    public override void OnAction(int num)
    {
        // 보스의 HP를 num만큼 감소 
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
        GetComponent<SpriteRenderer>().color = Color.black;
        _isCurse = true;
        _curseTurnCount = count;
    }

    public override void OffCurse()
    {
        _isCurse = false;
        GetComponent<SpriteRenderer>().color = Color.white;
    }
}
