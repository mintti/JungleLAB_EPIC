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
        // 디버프의 내용
        _debuffCount -= 1;
    }

}
