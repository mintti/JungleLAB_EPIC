using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class PlayerHealth: PlayerProperty<int> {
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public override void Init() {
		_isMaxDefault = true;
	}

	public override void ChangeValue(int value) {
		_value += value;
		if (_value < 0) {
			_value = 0;
		}
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}