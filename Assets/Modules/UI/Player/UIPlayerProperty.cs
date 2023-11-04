using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace TH.Core {

public class UIPlayerProperty : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	[ValueDropdown("GetProperty", AppendNextDrawer = true)]
	[SerializeField] private PlayerProperty<int> _targetProperty;

	private ComponentGetter<TextMeshProUGUI> _text
		= new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.Child, "ValueText");
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	private void Awake() {
		Init();
	}

	private void Init() {
		_targetProperty.Subscribe(UpdateValue);
	}

	private void UpdateValue(int value) {
		_text.Get(gameObject).text = value.ToString();
	}
	#endregion

	#region OdinInspector
	private IEnumerable GetProperty() {
		Player player = FindAnyObjectByType<Player>();
		List<PlayerProperty<int>> properties 
			= player.gameObject.GetComponents<PlayerProperty<int>>().ToList();

		for (int i = 0; i < properties.Count; i++) {
			yield return properties[i];
		}
	}
	#endregion
}

}