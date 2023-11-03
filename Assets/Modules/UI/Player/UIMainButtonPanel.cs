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
	private ComponentGetter<Button> _actionButton
		= new ComponentGetter<Button>(TypeOfGetter.ChildByName, "ActionBTN");
	private ComponentGetter<Button> _moveButton
		= new ComponentGetter<Button>(TypeOfGetter.ChildByName, "MoveBTN");
	private ComponentGetter<Button> _actionEndButton
		= new ComponentGetter<Button>(TypeOfGetter.ChildByName, "ActionEndBTN");
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	private void Awake() {
		_actionButton.Get(gameObject).onClick.AddListener(() => {StartCoroutine(OnActionButtonClick());});
		_moveButton.Get(gameObject).onClick.AddListener(() => {StartCoroutine(OnMoveButtonClick());});
		_actionEndButton.Get(gameObject).onClick.AddListener(OnActionEndButtonClick);
	}

	private IEnumerator OnActionButtonClick() {
		DisableButtons();
		yield return GameManager.Card.SelectedCardAction();
		EnableButtons();
	}

	private IEnumerator OnMoveButtonClick() {
		DisableButtons();
		yield return GameManager.Card.SelectedCardMove();
		EnableButtons();
	}

	private void OnActionEndButtonClick() {
		GameManager.Card.DrawCard();
	}

	private void DisableButtons() {
		_actionButton.Get(gameObject).interactable = false;
		_moveButton.Get(gameObject).interactable = false;
		_actionEndButton.Get(gameObject).interactable = false;
	}

	private void EnableButtons() {
		_actionButton.Get(gameObject).interactable = true;
		_moveButton.Get(gameObject).interactable = true;
		_actionEndButton.Get(gameObject).interactable = true;
	}
	#endregion
}

}