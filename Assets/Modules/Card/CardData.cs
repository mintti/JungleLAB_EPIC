using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TH.Core {

[Serializable]
public class CardData
{
	public CardData(int cardNumber, Card.Type cardType) {
		CardNumber = cardNumber;
		CardType = cardType;
	}

    #region PublicVariables
	public Card.Type CardType;
	public int CardNumber;
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	#endregion
}

}