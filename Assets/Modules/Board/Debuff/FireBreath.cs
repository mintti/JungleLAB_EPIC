using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : Debuff {

    private int _damage;
    public FireBreath(int dmg)
    {
        _damage = dmg;
    }

    
    public override void OnDebuff()
    {
        base.OnDebuff();
        // 플레이어에게 _damage만큼 데미지 주기

    }

}
