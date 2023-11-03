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
	[Button]
	public void Init() {
		// 속성 값 초기화
		_health = GetComponent<PlayerHealth>();
		_defence = GetComponent<PlayerDefence>();
		_health.ResetValue();
		_defence.ResetValue();

		// 플레이어 능력 초기화
		_abilities = GetComponents<PlayerAbility>().ToDictionary(x => x.GetType(), x => x);

		// 플레이어 위치 초기화
		_position = 0;
		MoveTo(_position);
	}

	public T Ability<T>() where T : PlayerAbility {
		return _abilities[typeof(T)] as T;
	}

	public IEnumerator UpdatePlayer() {
		// 플레이어 능력 업데이트
		foreach (var ability in _abilities.Values) {
			ability.PreUpdate();
		}

		// 플레이어 능력 업데이트
		foreach (var ability in _abilities.Values) {
			ability.UpdateAbility();
		}

		// 플레이어 능력 업데이트
		foreach (var ability in _abilities.Values) {
			ability.PostUpdate();
		}

		yield return null;
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
            yield return MoveToCoroutine(_position, 0.5f);

			BoardManager.I.GetTile(_position).debuff?.OnDebuff();
        }
	}
	#endregion
    
	#region PrivateMethod
	private IEnumerator MoveToCoroutine(int index, float time) {
		Vector3 targetPos = BoardManager.I.GetTilePos(index);
		transform.DOMove(targetPos, time);
		yield return new WaitForSeconds(time);
	}

	private void MoveTo(int index) {
		Vector3 targetPos = BoardManager.I.GetTilePos(index);
		transform.position = targetPos;
	}
	#endregion
}