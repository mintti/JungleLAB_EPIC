using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Fireball : MonoBehaviour, ISummon
{
    private int _index;
    [SerializeField] private int _distance;
    [SerializeField] private int _damage;

    public IEnumerator Connect()
    {
        throw new System.NotImplementedException();
    }

    public IEnumerator OnTurnEnd()
    {
        for (int i = 0; i < _distance; i++)
        {
            _index = BoardManager.I.GetNextIndex(_index);
            if (_index == BoardManager.I.PlayerOnIndex)
            {
                //플레이어에게 _damage만큼 데미지주기
            }

            Vector3 nextPos = BoardManager.I.GetTilePos(_index);
            transform.DOMove(nextPos, 0.5f);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
