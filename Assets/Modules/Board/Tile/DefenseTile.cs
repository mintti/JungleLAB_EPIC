using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseTile : BaseTile
{
    [SerializeField] private GameObject fireball;
    public override void OnAction(int num)
    {
        // player�� ���� num��ŭ ��� 
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
        _isCurse = true;
        _curseTurnCount = count;
    }
}
