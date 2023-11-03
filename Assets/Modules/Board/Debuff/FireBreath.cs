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
        // �÷��̾�� _damage��ŭ ������ �ֱ�

    }

    public override void OffDebuff()
    {
        
    }

}
