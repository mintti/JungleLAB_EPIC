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
        // �÷��̾�� _damage��ŭ ������ �ֱ�

    }

}
