using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

using DG.Tweening;
using System;
using TMPro;
using UnityEngine.UI;

namespace TH.Core {

    public class UICard : MonoBehaviour, IPointerClickHandler
    {
        #region PublicVariables
		public Card Card => _card;
        #endregion

        #region PrivateVariables
		private Card _card;
		private Action<UICard> _onSelect;
		private Action<UICard> _onAddSelect;

		private ComponentGetter<RectTransform> _rectTransform
			= new ComponentGetter<RectTransform>(TypeOfGetter.This);
		private ComponentGetter<TextMeshProUGUI> _cardNumberText
			= new ComponentGetter<TextMeshProUGUI>(TypeOfGetter.ChildByName, "CardNumber");
		private ComponentGetter<Image> _cardImage
			= new ComponentGetter<Image>(TypeOfGetter.ChildByName, "Image");
        #endregion

        #region PublicMethod
		public void Init(Card card, Action<UICard> onSelect, Action<UICard> onAddSelect) {
			_card = card;
			_onSelect = onSelect;
			_onAddSelect = onAddSelect;

			_cardNumberText.Get(gameObject).text = card.CardData.CardNumber.ToString();
			_cardImage.Get(gameObject).sprite = CardManager.GetCardEmblem(card.CardData.CardType);
		}

		public void OnPointerClick(PointerEventData eventData)
        {
			if (_card == null) {
				return;
			}
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) {
				_onAddSelect(this);
			} else {
				_onSelect(this);
			}
        }

		public void UnSelect() {
			_rectTransform.Get(gameObject).DOScale(1f, 0.1f).SetEase(Ease.OutBack);
		}

		public void Select() {
			_rectTransform.Get(gameObject).DOScale(1.2f, 0.1f).SetEase(Ease.OutBack);
		}
        #endregion

        #region PrivateMethod
        #endregion
        
    }

}