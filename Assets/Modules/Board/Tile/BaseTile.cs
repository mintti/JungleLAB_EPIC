using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseTile : MonoBehaviour
{
    public int index;
    public Debuff debuff;
    
    public virtual void OnAction(int num=0)
    {

    }

    public virtual void OnTurnEnd()
    {

        if (debuff != null)
        {
            debuff.OnDebuff();

            if (debuff.DebuffCount == 0)
            {
                debuff = null;
            }
        }
    }

}
