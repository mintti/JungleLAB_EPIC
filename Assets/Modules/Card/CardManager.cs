using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class CardManager: IManager
{
	private const int DRAW_COUNT = 5;

    #region PublicVariables
	public static Sprite GetCardEmblem(Card.Type type) {
		switch (type) {
			case Card.Type.Wizard:
				return GameManager.Resource.LoadSprite(ResourceManager.Sprites.UI_EMBLEM_WIZARD);
			default:
				return null;
		}
	}
	#endregion

	#region PrivateVariables
	private ComponentGetter<CardDeck> _cardDeck
		= new ComponentGetter<CardDeck>(TypeOfGetter.Global);
	private ComponentGetter<UICardInfo> _uICardInfo
		= new ComponentGetter<UICardInfo>(TypeOfGetter.Global);
	#endregion

	#region PublicMethod
	public void Init() {
		_cardDeck.Get().Init();
		_uICardInfo.Get().Init(_cardDeck.Get());

		// TODO: 임시로 여기에 넣어둠
		_cardDeck.Get().DrawCard(DRAW_COUNT);
		_uICardInfo.Get().UpdateUI();
	}

	public void SelectedCardAction() {
		_uICardInfo.Get().SelectedCardAction();
		_uICardInfo.Get().UpdateUI();
	}

	public void SelectedCardMove() {
		_uICardInfo.Get().SelectedCardMove();
		_uICardInfo.Get().UpdateUI();
	}

	public void DrawCard() {
		_cardDeck.Get().DiscardHand();
		_cardDeck.Get().DrawCard(DRAW_COUNT);

		_uICardInfo.Get().UpdateUI();
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}