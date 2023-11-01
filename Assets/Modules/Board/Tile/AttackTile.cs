using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTile : BaseTile
{
    private bool _isCurse;
    private int _curseTurnCount;

    
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
                _isCurse = true;
            }
        }
        

    }
    public void OnCurse()
    {

    }
}
