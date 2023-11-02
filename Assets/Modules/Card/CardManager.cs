using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

public class CardManager: IManager
{
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
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}

}