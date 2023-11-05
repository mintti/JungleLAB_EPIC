using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public abstract class BaseTile : MonoBehaviour
{
    public int index;
    [ShowInInspector] public Debuff debuff;
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
        Debug.Log(debuff);
        Debug.Log(debuff.DebuffCount);
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
            Debug.Log(debuff.DebuffCount);
            if (debuff.DebuffCount <= 0)
            {
                Debug.Log("destroy");
                debuff = null;
                Destroy(_debuffEffect);
            }
        }
    }

    public virtual void OnCurse(int count)
    {

    }

    public virtual void OffCurse()
    {

    }

}
