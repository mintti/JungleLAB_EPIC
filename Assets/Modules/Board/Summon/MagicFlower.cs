using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicFlower : MonoBehaviour,ISummon
{
    private int _damage;
    private int _index;
    public int Index => _index;
    public void Init(int damage,int index)
    {
        _damage = damage;
        _index = index;
        BoardManager.I.AddSummon(this);
    }

    public void Attack()
    {
        GameManager.Boss.HpUpdate(_damage);
        BoardManager.I.DeleteSummon(this);
        Destroy(gameObject);
    }

    public IEnumerator OnTurnEnd()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator Connect()
    {
        throw new System.NotImplementedException();
    }

    public void OnEvent()
    {
        Attack();
    }
}
