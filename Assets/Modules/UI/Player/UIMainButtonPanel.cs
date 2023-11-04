using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TH.Core {

public class UIMainButtonPanel : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	private ComponentGetter<Button> _actionEndButton
		= new ComponentGetter<Button>(TypeOfGetter.ChildByName, "ActionEndBTN");
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	private void Awake() {
		_actionEndButton.Get(gameObject).onClick.AddListener(OnActionEndButtonClick);
	}

	private IEnumerator OnActionButtonClick() {
		DisableButtons();
		//yield return GameManager.Card.SelectedCardAction();
		yield return null;
		EnableButtons();
	}

	private IEnumerator OnMoveButtonClick() {
		DisableButtons();
		//yield return GameManager.Card.SelectedCardMove();
		yield return null;
		EnableButtons();
	}

	private void OnActionEndButtonClick() {
		GameManager.I.Next();
	}

	private void DisableButtons() {
		_actionEndButton.Get(gameObject).interactable = false;
	}

	private void EnableButtons() {
		_actionEndButton.Get(gameObject).interactable = true;
	}
	#endregion
}

}