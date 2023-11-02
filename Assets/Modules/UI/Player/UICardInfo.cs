using System.Collections.Generic;
using TH.Core;
using UnityEngine;

public class UICardInfo : MonoBehaviour
{
    private ObjectGetter _handUI
        = new ObjectGetter(TypeOfGetter.Child, "Cards");
    private ObjectGetter _drawPile
        = new ObjectGetter(TypeOfGetter.Child, "CardBeforeUse");
    private ObjectGetter _discardPile
        = new ObjectGetter(TypeOfGetter.Child, "CardAfterUse");

    private List<UICard> _handCards = new List<UICard>();

    
}