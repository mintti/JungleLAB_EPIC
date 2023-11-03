using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                GameObject s=Instantiate(BoardManager.I.fireball, transform.position, Quaternion.identity);
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
