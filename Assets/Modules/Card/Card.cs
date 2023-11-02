using System.Collections;
using System.Collections.Generic;
using TH.Core;
using UnityEngine;

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
	#endregion

	#region PrivateVariables
	private CardData _cardData;
	#endregion

	#region PublicMethod
	public void Use(ActionType actionType) {
		if (actionType == ActionType.Move) {
			GameManager.Player.Move(_cardData.CardNumber);
		} else if (actionType == ActionType.Activate) {
			//Activate();
		}
	}
	#endregion
    
	#region PrivateMethod
	#endregion
}