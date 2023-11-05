using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TH.Core {

[RequireComponent(typeof(Player))]
public abstract class PlayerProperty<T>: MonoBehaviour where T: IComparable {
    #region PublicVariables
	public T Value => _value;
	public T MaxValue => _maxValue;
	public T MinValue => _minValue;
	#endregion

	#region PrivateVariables
	[SerializeField, ReadOnly] protected T _value;
	[SerializeField] protected bool _isMaxDefault;
	[SerializeField, ShowIf("_isMaxDefault", true)] protected T _maxValue;
	[SerializeField, ShowIf("_isMaxDefault", false)] protected T _minValue;

	protected Action<T> _onValueUpdated;
	#endregion

	#region PublicMethod
	public abstract void Init();
	public abstract void ChangeValue(T value);

	public virtual void Subscribe(Action<T> onValueUpdated) {
		_onValueUpdated += onValueUpdated;
	}

	public virtual void Unsubscribe(Action<T> onValueUpdated) {
		_onValueUpdated -= onValueUpdated;
	}

	public virtual void ResetValue() {
		if (_isMaxDefault) {
			_value = _maxValue;
		} else {
			_value = _minValue;
		}

		_onValueUpdated?.Invoke(_value);
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}