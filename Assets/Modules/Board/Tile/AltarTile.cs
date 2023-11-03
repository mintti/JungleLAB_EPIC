using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltarTile : BaseTile
{
    public override void OnAction(int num = 0)
    {
        UIManager.I.UIAltarInfo.OnTileEvent();
    }

    public override void OnTurnEnd()
    {

    }

}
