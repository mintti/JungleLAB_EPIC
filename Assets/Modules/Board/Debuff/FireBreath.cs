using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class FireBreath : Debuff {

    private int _damage;
    public override GameObject Effect => BoardManager.I.fireBreath;
    public FireBreath(int dmg, int count)
    {
        _debuffCount =count;
        _damage = dmg;

    }

    [Button]
    public void testSetCount(int count)
    {
        _debuffCount = count;
    }
    
    public override void OnDebuff()
    {
        // 플레이어에게 _damage만큼 데미지 주기

    }

    public override void OffDebuff()
    {
        
    }

}
