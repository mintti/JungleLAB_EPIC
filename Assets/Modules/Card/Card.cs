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

	public Card(CardData cardData) {
		_cardData = cardData;
	}

    #region PublicVariables
	public CardData CardData => _cardData;
	#endregion

	#region PrivateVariables
	[ShowInInspector, ReadOnly] private CardData _cardData;
	#endregion

	#region PublicMethod
	public IEnumerator Use(ActionType actionType) {
		if (actionType == ActionType.Move) {
			yield return GameManager.Player.Move(_cardData.CardNumber);
		} else if (actionType == ActionType.Activate) {
			if (_cardData.CardType == Type.Wizard) {
				GameManager.Player.Ability<PlayerMagic>().CastingGauge += _cardData.CardNumber;
			}
			
			BoardManager.I.tiles[BoardManager.I.PlayerOnIndex].OnAction(_cardData.CardNumber);
		}
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}