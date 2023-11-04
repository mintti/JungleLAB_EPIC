using System;
using System.Collections;
using UnityEngine;

namespace TH.Core {

public class CardManager: IManager
{
	private const int DRAW_COUNT = 5;

    #region PublicVariables
	public CardUIState CardSlectionState => _cardUIState;

	public static Sprite GetCardEmblem(Card.Type type) {
		switch (type) {
			case Card.Type.Wizard:
				return GameManager.Resource.LoadSprite(ResourceManager.Sprites.UI_EMBLEM_WIZARD);
			default:
				return null;
		}
	}

	public CardDeck CardDeck => _cardDeck.Get();
	#endregion

	#region PrivateVariables
	private CardUIState _cardUIState = CardUIState.Idle;
	private UICardRequestPanel _targetCardRequestPanel;

	private ComponentGetter<CardDeck> _cardDeck
		= new ComponentGetter<CardDeck>(TypeOfGetter.Global);
	private ComponentGetter<UICardInfo> _uICardInfo
		= new ComponentGetter<UICardInfo>(TypeOfGetter.Global);
	#endregion

	#region PublicMethod
	public void Init() {
		_cardDeck.Get().Init();
		_uICardInfo.Get().Init(_cardDeck.Get(), OnDragStarted);

		SetCardSelectable(false);
	}

	public UICardRequestPanel RequestCard (
		string requestName,
		string description,
		Action<Card.Type, int> cardAction,
		CardAfterUse cardAfterUse=CardAfterUse.KeepToGraveyard,
		CardUseRestriction cardUseRestriction=CardUseRestriction.JustOne,
		bool isCloseButtonVisible=false,
		Action onCloseButtonClick=null,
		CardRequestPosition cardRequestPosition=CardRequestPosition.Middle
	) {
		GameObject cardUsePanelPrefab = GameManager.Resource.LoadPrefab(ResourceManager.Prefabs.UI_CARD_USE_PANEL);
		GameObject cardUsePanel = GameObject.Instantiate(cardUsePanelPrefab, UIManager.I.UIPlayerInfo.transform);
		
		(cardUsePanel.transform as RectTransform).anchoredPosition = GetCardUsePanelPosition(cardRequestPosition);

		SetCardSelectable(true);

		UICardRequestPanel requestPanel = cardUsePanel.GetComponent<UICardRequestPanel>();
		requestPanel.Init(
			requestName,
			description,
			SetTargetRequestPanel,
			ClearTargetRequestPanel,
			cardAction,
			cardAfterUse,
			cardUseRestriction,
			isCloseButtonVisible,
			onCloseButtonClick
		);

		return requestPanel;
	}

	public void DrawCard() {
		_cardDeck.Get().DiscardHand();
		_cardDeck.Get().DrawCard(DRAW_COUNT);

		_uICardInfo.Get().UpdateUI();
	}

	public void UpdateUI() {
		_uICardInfo.Get().UpdateUI();
	}

	public void MakeCardsSelectable() {
		SetCardSelectable(true);
	}

	public void MakeCardsUnSelectable() {
		SetCardSelectable(false);
	}
	#endregion
    
	#region PrivateMethod
	private Vector2 GetCardUsePanelPosition(CardRequestPosition cardRequestPosition) {
		switch (cardRequestPosition) {
			case CardRequestPosition.Left:
				return new Vector2(-800, 0);

			case CardRequestPosition.Right:
				return new Vector2(800, 0);

			case CardRequestPosition.Middle:
				return new Vector2(0, 0);

			default:
				return Vector2.zero;
		}
	}

	private void SetCardSelectable(bool isSelectable) {
		if (isSelectable) {
			_uICardInfo.Get().MakeCardsSelectable();
		} else {
			_uICardInfo.Get().MakeCardsUnSelectable();
		}
	}

	private void OnDragStarted(UICard uICard) {
		if (_cardUIState == CardUIState.Dragging) {
			GameManager.Log.Log("Already Drag Started.", LogManager.LogType.Error);
			return;
		}

		_cardUIState = CardUIState.Dragging;

		GameManager.I.StartCoroutine(Dragging());
	}

	private IEnumerator Dragging() {
		while (_cardUIState == CardUIState.Dragging) {
			if (Input.GetMouseButtonUp(0)) {
				if (_targetCardRequestPanel == null) {
					_uICardInfo.Get().FinishDragging();
				} else {
					if (!_targetCardRequestPanel.UseCard(_uICardInfo.Get().SelectedCard.Card)) {
						_uICardInfo.Get().FinishDragging();
					}
				}
				_cardUIState = CardUIState.Idle;
			}
			yield return null;
		}
	}

	private void SetTargetRequestPanel(UICardRequestPanel uICardRequestPanel) {
		_targetCardRequestPanel = uICardRequestPanel;
	}

	private void ClearTargetRequestPanel() {
		_targetCardRequestPanel = null;
	}
	#endregion
}

public enum CardRequestPosition {
		Left,
		Right,
		Middle
}

public enum CardUIState {
	Idle,
	Dragging,
}

}