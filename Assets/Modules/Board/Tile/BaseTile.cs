using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class BaseTile : MonoBehaviour
{
    public int index;
    public Debuff debuff;
    private GameObject _debuffEffect;

    protected bool _isCurse;
    public bool IsCurse
    {
        get => _isCurse;
    }
    protected int _curseTurnCount;

    [Button]
    public void AddDebuff(Debuff d)
    {
        debuff = d;
        _debuffEffect = Instantiate(d.Effect, transform.position, Quaternion.identity);
    }
    public virtual void OnAction(int num=0)
    {

    }

    public virtual void OnTurnEnd()
    {

        if (debuff != null)
        {
            debuff.DebuffCount -= 1;
            if (debuff.DebuffCount <= 0)
            {
                debuff = null;
                Destroy(_debuffEffect);
            }
        }
    }

    public virtual void OnCurse(int count)
    {

    }

}
