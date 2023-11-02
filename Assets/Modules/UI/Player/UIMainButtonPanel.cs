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
		_actionButton.Get(gameObject).onClick.AddListener(OnActionButtonClick);
		_moveButton.Get(gameObject).onClick.AddListener(OnMoveButtonClick);
		_actionEndButton.Get(gameObject).onClick.AddListener(OnActionEndButtonClick);
	}

	private void OnActionButtonClick() {
		GameManager.Card.SelectedCardAction();
	}

	private void OnMoveButtonClick() {
		GameManager.Card.SelectedCardMove();
	}

	private void OnActionEndButtonClick() {
		GameManager.Card.DrawCard();
	}
	#endregion
}

}