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
        // �÷��̾�� _damage��ŭ ������ �ֱ�
        _debuffCount -= 1;

    }

}
