using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GambleTile : BaseTile
{
    public override void OnAction(int num = 0)
    {
        UIManager.I.UIGambleInfo.OnTileEvent();
    }

    public override void OnTurnEnd()
    {

    }
}
