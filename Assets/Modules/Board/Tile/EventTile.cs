using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTile : BaseTile
{
    public override void OnAction(int num = 0)
    {
        UIManager.I.UIEventInfo.OnTileEvent();
    }

    public override void OnTurnEnd()
    {

    }
}
