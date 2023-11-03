using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

[RequireComponent(typeof(Player))]
public abstract class PlayerAbility: MonoBehaviour {
    #region PublicVariables
	#endregion

	#region PrivateVariables
	protected Player _player;
	#endregion

	#region PublicMethod
	public virtual void PreUpdate() {

	}

	public virtual void UpdateAbility() {

	}

	public virtual void PostUpdate() {

	}

	public virtual void UpdateUI() {
		
	}
	#endregion
    
	#region PrivateMethod
	protected virtual void Init() {
		_player = GetComponent<Player>();
	}
	#endregion
}

}