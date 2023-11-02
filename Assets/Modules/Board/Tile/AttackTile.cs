using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                _isCurse = false;
                // 파이어볼 소환
            }
        }
        

    }
    public override void OnCurse(int count)
    {
        // 일단 PoC에선 저주가 하나밖에 없어서..
        _isCurse = true;
        _curseTurnCount = count;
    }
}
