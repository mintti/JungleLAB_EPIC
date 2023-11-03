using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using System;
using DG.Tweening;
using TH.Core;
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerDefence))]
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

	public int Position => _position;
	#endregion

	#region PrivateVariables
	private const int MAX_CASTING_GUAGE = 5;

	[Title("Player Properties")]
	[ShowInInspector, ReadOnly] private int _magicStack;
	[ShowInInspector, ReadOnly] private int _castingGuage;

	[ShowInInspector, ReadOnly] private int _position;

	// 플레이어 이벤트
	private Func<int, IEnumerator> _onMove;

	// 플레이어 속성들
	private PlayerHealth _health;
	private PlayerDefence _defence;

	// 플레이어 능력
	private Dictionary<Type, PlayerAbility> _abilities;
	#endregion

	#region PublicMethod
	public void Init() {
		// 속석 값 초기화
		_health.ResetValue();
		_defence.ResetValue();

		// 플레이어 능력 초기화
		_abilities = GetComponents<PlayerAbility>().ToDictionary(x => x.GetType(), x => x);

		_castingGuage = 0;
		_magicStack = 0;

		_position = 0;
	}

	public void Defence(int value) {
		_defence.ChangeValue(value);
	}

	public void Hit(int damage) {
		_defence.ChangeValue(-damage);

		if (_defence.Value < 0) {
			_health.ChangeValue(_defence.Value);
			_defence.ResetValue();
		}
	}

	public IEnumerator Move(int value) {
		if (_onMove != null) {
			StartCoroutine(_onMove?.Invoke(value));
		}

		for(int i = 0; i < value; i++)
        {
            _position = BoardManager.I.GetNextIndex(_position);
            Vector3 nextPos = BoardManager.I.GetTilePos(_position);
            transform.DOMove(nextPos, 0.5f);
            yield return new WaitForSeconds(0.5f);

			BoardManager.I.GetTile(_position).debuff?.OnDebuff();
        }
	}

	public void CastMagic(int value) {
		_castingGuage += value;
		if (_castingGuage >= MAX_CASTING_GUAGE) {
			_magicStack += _castingGuage / MAX_CASTING_GUAGE;
			_castingGuage %= MAX_CASTING_GUAGE;

			UIManager.I.UIPlayerInfo.UIPlayerSkill.UpdateMagicCircleCount(_magicStack);
		}
		
		UIManager.I.UIPlayerInfo.UIPlayerSkill.UpdateCastingGauge(_castingGuage, MAX_CASTING_GUAGE);
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}