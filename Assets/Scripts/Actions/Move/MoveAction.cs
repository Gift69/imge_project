using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    public override void execute(Player player)
    {
        // TODO: implement
    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        vPlayer.cell.getCellRelative(value).placeBoardPiece(vPlayer);
        return new List<GameObject>();
    }

    public override ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return new MoveSelection(vPlayer);
    }
}