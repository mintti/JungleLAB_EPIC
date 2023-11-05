using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TH.Core;
using UnityEngine;

[Serializable]
public class Card {
	public enum Type {
		Base,
		Wizard
	}

	public enum ActionType {
		Move,
		Activate
	}

	public Card(CardData cardData, bool isVolatile=false) {
		_cardData = cardData;

		_isVolatile = isVolatile;
	}

    #region PublicVariables
	public CardData CardData => _cardData;
	public bool IsVolatile => _isVolatile;
	#endregion

	#region PrivateVariables
	[ShowInInspector, ReadOnly] private CardData _cardData;

	private bool _isVolatile;
	#endregion

	#region PublicMethod
	public IEnumerator Use(ActionType actionType) {
		if (actionType == ActionType.Move) {
			yield return GameManager.Player.Move(_cardData.CardNumber);
		} else if (actionType == ActionType.Activate) {
			GameManager.Player.TileAction(_cardData.CardType, _cardData.CardNumber);
		}
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}