using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTile : BaseTile
{
    private bool _isCurse;
    private int _curseTurnCount;

    
    public override void OnAction(int num)
    {
        // ������ HP�� num��ŭ ���� 
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
