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
	private CardAfterUse _cardAfterUse;
	private CardUseRestriction _cardUseRestriction;

	private bool _isPointerEnter = false;
	private int _usedCardsCount = 0;
	private int _lastUsedCardNumber = -1;
	private bool _isCardNumberAcsending = false;

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
		CardAfterUse cardAfterUse=CardAfterUse.KeepToGraveyard,
		CardUseRestriction cardUseRestriction=CardUseRestriction.JustOne,
		bool isCloseButtonVisible=false,
		Action onCloseButtonClick=null
	) {
		_onPointerEnter = onPointerEnter;
		_onPointerExit = onPointerExit;
		_cardAction = cardAction;
		_cardAfterUse = cardAfterUse;
		_cardUseRestriction = cardUseRestriction;

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

	public bool UseCard(Card card) {
		if (_cardUseRestriction == CardUseRestriction.JustOne) {
			if (_usedCardsCount > 0) {
				GameManager.Log.Log("This panel already accepted card", LogManager.LogType.Warning);
				return false;
			}
		} else if (_cardUseRestriction == CardUseRestriction.Succesive) {
			if (!IsSuccesiveNumber(card.CardData.CardNumber)) {
				return false;
			}
		}

		_usedCardsCount++;
		_lastUsedCardNumber = card.CardData.CardNumber;

		GameManager.Card.CardDeck.ExtractCardFromHand(card);

		_cardAction?.Invoke(card.CardData.CardType, card.CardData.CardNumber);

		if (_cardAfterUse == CardAfterUse.Remove) {
			GameManager.Card.CardDeck.RemoveCard(card);
		} else if (_cardAfterUse == CardAfterUse.KeepToHand) {
			GameManager.Card.CardDeck.PutCardIntoHand(card);
		} else if (_cardAfterUse == CardAfterUse.KeepToGraveyard) {
			GameManager.Card.CardDeck.PutCardIntoGraveyard(card);
		}

		if (_cardUseRestriction == CardUseRestriction.JustOne) {
			Close();
		}

		return true;
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

	private bool IsSuccesiveNumber(int value) {
		if (_cardUseRestriction == CardUseRestriction.Succesive) {
			if (_usedCardsCount == 1) {
				if (_lastUsedCardNumber - 1 == value) {
					_isCardNumberAcsending = false;
				} else if (_lastUsedCardNumber + 1 == value) {
					_isCardNumberAcsending = true;
				} else {
					return false;
				}
			} else if (_usedCardsCount > 1) {
				if (_isCardNumberAcsending) {
					if (_lastUsedCardNumber + 1 != value) {
						return false;
					}
				} else {
					if (_lastUsedCardNumber - 1 != value) {
						return false;
					}
				}
			} else if (_usedCardsCount == 0) {
				return true;
			}
		} else {
			return true;
		}

		return true;
	}
	#endregion
}

public enum CardAfterUse {
	Remove,
	KeepToHand,
	KeepToGraveyard,
}

public enum CardUseRestriction {
	JustOne,
	Succesive,
	Unlimited,
}

}