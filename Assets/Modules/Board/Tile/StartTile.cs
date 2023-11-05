using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTile : BaseTile
{
    public override void OnAction(int num = 0)
    {
        UIManager.I.UIStartPanel.OnTileEvent();
    }

    public override void OnTurnEnd()
    {
        base.OnTurnEnd();
    }
}
