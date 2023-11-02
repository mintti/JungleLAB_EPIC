using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using TH.Core;

public class Player : MonoBehaviour {
	#region PublicVariables
	#endregion

	#region PrivateVariables
	[Title("Player Settings")]
	[SerializeField] private int _maxHealth;

	[Title("Player Properties")]
	[ShowInInspector, ReadOnly] private int _health;
	[ShowInInspector, ReadOnly] private int _defence;
	#endregion

	#region PublicMethod
	public void Init() {
		_health = _maxHealth;
	}

	public void Attack(int damage) {
		
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

	}
	#endregion
    
	#region PrivateMethod
	#endregion
}