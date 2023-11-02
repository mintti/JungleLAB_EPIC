using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace TH.Core {

public class GameData : MonoBehaviour
{
    #region PublicVariables
	[SerializeField] public List<InitialCardData> InitialCards = new();
	#endregion

	#region PrivateVariables
	#endregion

	#region PublicMethod
	#endregion
    
	#region PrivateMethod
	#endregion
}

[Serializable]
public class InitialCardData {
	public CardData CardData;
	public int Count;

	public InitialCardData(CardData cardData, int count) {
		CardData = cardData;
		Count = count;
	}
}

}