using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Debuff : MonoBehaviour
{

    protected int _debuffCount;
    public int DebuffCount
    {
        get => _debuffCount;
    }
    public virtual void OnDebuff()
    {
    }

}
