using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public int index;
    public Debuff debuff;

    protected bool _isCurse;
    protected int _curseTurnCount;
    public virtual void OnAction(int num=0)
    {

    }

    public virtual void OnTurnEnd()
    {

        if (debuff != null)
        {
            //debuff.OnDebuff();
            debuff.DebuffCount -= 1;
            if (debuff.DebuffCount == 0)
            {
                debuff = null;
            }
        }
    }

    public virtual void OnCurse(int count)
    {

    }

}
