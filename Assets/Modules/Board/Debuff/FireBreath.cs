using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBreath : Debuff {

    private int _damage;
    public FireBreath(int dmg, int count)
    {
        _debuffCount = count;
        _damage = dmg;
    }

    
    public override void OnDebuff()
    {
        // 플레이어에게 _damage만큼 데미지 주기
        _debuffCount -= 1;

    }

}
