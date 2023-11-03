using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using DG.Tweening;

public class Player : MonoBehaviour {
	#region PublicVariables
	public Func<int, IEnumerator> OnMove {
		get {
			return _onMove;
		}
		set {
			_onMove = value;
		}
	}
	#endregion

	#region PrivateVariables
	private const int MAX_CASTING_GUAGE = 5;

	[Title("Player Settings")]
	[SerializeField] private int _maxHealth;

	[Title("Player Properties")]
	[ShowInInspector, ReadOnly] private int _health;
	[ShowInInspector, ReadOnly] private int _defence;
	[ShowInInspector, ReadOnly] private int _magicStack;
	[ShowInInspector, ReadOnly] private int _castingGuage;

	[ShowInInspector, ReadOnly] private int _index;

	// 플레이어 이벤트
	private Func<int, IEnumerator> _onMove;
	#endregion

	#region PublicMethod
	public void Init() {
		_health = _maxHealth;
		_defence = 0;

		_castingGuage = 0;
		_magicStack = 0;

		_index = 0;
	}

	public void Defence(int value) {
		_defence += value;
	}

	public void Hit(int damage) {
		_defence -= damage;

		if (_defence < 0) {
			_health += _defence;
			_defence = 0;
		}
	}

	public IEnumerator Move(int value) {
		StartCoroutine(_onMove?.Invoke(value));

		for(int i = 0; i < value; i++)
        {
            _index = BoardManager.I.GetNextIndex(_index);
            Vector3 nextPos = BoardManager.I.GetTilePos(_index);
            transform.DOMove(nextPos, 0.5f);
            yield return new WaitForSeconds(0.5f);

			BoardManager.I.GetTile(_index).debuff?.OnDebuff();
        }
	}

	public void CastMagic(int value) {
		_castingGuage += value;
		if (_castingGuage >= MAX_CASTING_GUAGE) {
			_magicStack += _castingGuage / MAX_CASTING_GUAGE;
			_castingGuage %= MAX_CASTING_GUAGE;
		}
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}