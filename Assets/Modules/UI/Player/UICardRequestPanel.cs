using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TH.Core {

public class UICardRequestPanel : MonoBehaviour
{
    #region PublicVariables
	#endregion

	#region PrivateVariables
	private Action<UICardRequestPanel> _onPointerEnter;
	private Action _onPointerExit;
	private Action<Card.Type, int> _cardAction;

	private bool _isPointerEnter = false;

	private ComponentGetter<TextMeshProUGUI> _requestNameText
		= new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.ChildByName, "RequestName");
	private ComponentGetter<TextMeshProUGUI> _descriptionText
		= new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.ChildByName, "Description");

	private ComponentGetter<UICardUseSlot> _cardUseSlot
		= new ComponentGetter<UICardUseSlot>(TypeOfGetter.ChildByName, "CardUseSlot");
	private ComponentGetter<Button> _closeButton
		= new ComponentGetter<Button>(TypeOfGetter.ChildByName, "CloseButton");
	#endregion

	#region PublicMethod
	public void Init(
		string requestName,
		string description,
		Action<UICardRequestPanel> onPointerEnter, 
		Action onPointerExit,
		Action<Card.Type, int> cardAction,
		bool isCloseButtonVisible=false,
		Action onCloseButtonClick=null
	) {
		_onPointerEnter = onPointerEnter;
		_onPointerExit = onPointerExit;
		_cardAction = cardAction;

		_cardUseSlot.Get(gameObject).Init(OnPointerEnter, OnPointerExit);

		_requestNameText.Get(gameObject).text = requestName;
		_descriptionText.Get(gameObject).text = description;

		if (isCloseButtonVisible) {
			_closeButton.Get(gameObject).gameObject.SetActive(true);
			_closeButton.Get(gameObject).onClick.AddListener(() => {
				if (_isPointerEnter) {
					_isPointerEnter = false;
					_onPointerExit();
				}
				onCloseButtonClick?.Invoke();
				Destroy(gameObject);
			});
		} else {
			_closeButton.Get(gameObject).gameObject.SetActive(false);
		}
	}

	public void UseCard(Card card) {
		_cardAction?.Invoke(card.CardData.CardType, card.CardData.CardNumber);
	}

	public void Close() {
		if (_isPointerEnter) {
			_isPointerEnter = false;
			_onPointerExit();
		}
		Destroy(gameObject);
	}
	#endregion
    
	#region PrivateMethod
	private void OnPointerEnter() {
		_isPointerEnter = true;
		_onPointerEnter(this);
	}

	private void OnPointerExit() {
		_isPointerEnter = false;
		_onPointerExit();
	}
	#endregion
}

}