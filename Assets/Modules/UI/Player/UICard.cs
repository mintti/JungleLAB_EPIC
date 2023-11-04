using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;
using System;
using TMPro;
using UnityEngine.UI;

namespace TH.Core {

    public class UICard : MonoBehaviour, IPointerDownHandler
    {
        #region PublicVariables
		public Card Card => _card;
        #endregion

        #region PrivateVariables
		private Card _card;
		private Action<UICard> _onSelect;

		private bool _isSelectable = false;
		private bool _isSelected = false;

		private ComponentGetter<RectTransform> _rectTransform
			= new ComponentGetter<RectTransform>(TypeOfGetter.This);
		private ComponentGetter<TextMeshProUGUI> _cardNumberText
			= new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.ChildByName, "CardNumber");
		private ComponentGetter<Image> _cardImage
			= new ComponentGetter<Image>(TypeOfGetter.ChildByName, "Image");
		private ComponentGetter<Image> _panelImage
			= new ComponentGetter<Image>(TypeOfGetter.This);

		private RectTransform _parentRectTransform;
        #endregion

        #region PublicMethod
		public void Init(Card card, RectTransform parentDeck, Action<UICard> onSelect) {
			_card = card;
			_onSelect = onSelect;

			_parentRectTransform = parentDeck;

			_cardNumberText.Get(gameObject).text = card.CardData.CardNumber.ToString();
			_cardImage.Get(gameObject).sprite = CardManager.GetCardEmblem(card.CardData.CardType);
		}

		public void OnPointerDown(PointerEventData eventData)
        {
			if (!_isSelectable) {
				return;
			}

			if (_card == null) {
				return;
			}

			if (GameManager.Card.CardSlectionState != CardUIState.Idle) {
				return;
			}

			_onSelect?.Invoke(this);
        }

		public void UnSelect() {
			_rectTransform.Get(gameObject).DOScale(1f, 0.1f).SetEase(Ease.OutBack);

			_isSelected = false;
			transform.SetParent(_parentRectTransform);

			
			_cardImage.Get(gameObject).raycastTarget = true;
			_cardNumberText.Get(gameObject).raycastTarget = true;
			_panelImage.Get(gameObject).raycastTarget = true;
		}

		public void Select() {
			_rectTransform.Get(gameObject).DOScale(1.2f, 0.1f).SetEase(Ease.OutBack);

			_isSelected = true;
			transform.SetParent(UIManager.I.UIPlayerInfo.transform);

			_cardImage.Get(gameObject).raycastTarget = false;
			_cardNumberText.Get(gameObject).raycastTarget = false;
			_panelImage.Get(gameObject).raycastTarget = false;
		}

		public void MakeSelectable() {
			_isSelectable = true;
		}

		public void MakeUnSelectable() {
			_isSelectable = false;
		}
        #endregion

        #region PrivateMethod
		private void Update() {
			if (_isSelected) {
				transform.position = Input.mousePosition;
			}
		}
        #endregion
    }

}