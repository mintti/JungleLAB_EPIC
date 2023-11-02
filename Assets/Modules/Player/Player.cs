using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

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

	}

	public void Hit(int damage) {

	}
	#endregion
    
	#region PrivateMethod
	#endregion
}