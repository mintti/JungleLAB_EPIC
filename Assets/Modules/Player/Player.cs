using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using TH.Core;
using Mono.CSharp;
using System;

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

	// 플레이어 이벤트
	private Func<int, IEnumerator> _onMove;
	#endregion

	#region PublicMethod
	public void Init() {
		_health = _maxHealth;
		_defence = 0;

		_castingGuage = 0;
		_magicStack = 0;
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

	public void Move(int value) {
		StartCoroutine(_onMove?.Invoke(value));
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