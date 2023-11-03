using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TH.Core {

[RequireComponent(typeof(Player))]
public abstract class PlayerProperty<T>: MonoBehaviour where T: IComparable {
    #region PublicVariables
	public T Value => _value;
	#endregion

	#region PrivateVariables
	[SerializeField, ReadOnly] protected T _value;
	[SerializeField] protected bool _isMaxDefault;
	[SerializeField, ShowIf("_isMaxDefault", true)] protected T _maxValue;
	[SerializeField, ShowIf("_isMaxDefault", false)] protected T _minValue;
	#endregion

	#region PublicMethod
	public abstract void Init();

	public virtual void ChangeValue(T value) {
		_value = value;
	}

	public virtual void ResetValue() {
		if (_isMaxDefault) {
			_value = _maxValue;
		} else {
			_value = _minValue;
		}
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}