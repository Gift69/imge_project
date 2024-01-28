using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : Action
{
    public override void execute(Player player)
    {

    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        GameObject virtualBomb = vPlayer.init(vPlayer.bomb,  vPlayer.cell.getCellRelative(value).getCoord());
        //vPlayer.cell.getCellRelative(value).placeBoardPiece(vPlayer);
        return new List<GameObject>(new GameObject[] {virtualBomb});

    }

    public override ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return new BombSelection(vPlayer);
    }

    public override Sprite getIcon()
    {
        return base.getIcon();
    }

    public override bool requiresInput()
    {
        return true;
    }
}
