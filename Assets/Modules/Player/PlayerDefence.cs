using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class PlayerDefence: PlayerProperty<int> {
	#region PublicVariables
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	public override void Init()
	{
		_isMaxDefault = false;
		_minValue = 0;

		ResetValue();
	}

	public override void ChangeValue(int value) {
		_value += value;
		if (_value < 0) {
			_value = 0;
		}

		_onValueUpdated?.Invoke(_value);
	}
	#endregion

	#region PrivateMethod
	#endregion
}

}