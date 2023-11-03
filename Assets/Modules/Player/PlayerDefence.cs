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
		base.ChangeValue(value);
		if (_value < 0) {
			_value = 0;
		}
	}
	#endregion

	#region PrivateMethod
	#endregion
}

}