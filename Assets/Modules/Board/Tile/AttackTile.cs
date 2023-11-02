using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTile : BaseTile
{
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
                _isCurse = false;
                // ���̾ ��ȯ
            }
        }
        

    }
    public override void OnCurse(int count)
    {
        // �ϴ� PoC���� ���ְ� �ϳ��ۿ� ���..
        _isCurse = true;
        _curseTurnCount = count;
    }
}
