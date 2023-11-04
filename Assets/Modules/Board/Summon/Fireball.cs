using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fireball : MonoBehaviour, ISummon
{
    private int _index;
    [SerializeField] private int _distance;
    [SerializeField] private int _damage;

    public int Index => _index;

    public void Init(int index)
    {
        _index = index;
    }
    public void DestroyFireball()
    {
        BoardManager.I.DeleteSummon(this);
        Destroy(gameObject);
    }
   
    private void Attack()
    {
        GameManager.Player.Hit(_damage);
        BoardManager.I.DeleteSummon(this);
        Destroy(gameObject);
    }

    public IEnumerator MoveTo(int index, float time)
    {
        if (this == null)
            yield break;
        Vector3 targetPos = BoardManager.I.GetTilePos(index);
        transform.DOMove(targetPos, time);
        yield return new WaitForSeconds(time);
    }
    public IEnumerator Connect()
    {
        yield break;
    }

    public IEnumerator OnTurnEnd()
    {
        for (int i = 0; i < _distance; i++)
        {
            _index = BoardManager.I.GetNextIndex(_index);
            if (_index == BoardManager.I.PlayerOnIndex)
            {
                Attack();
            }

            yield return MoveTo(_index, 0.5f);
        }

    }

    public void OnEvent()
    {
        throw new System.NotImplementedException();
    }

    public void OnPass()
    {
        Attack();
    }
}
