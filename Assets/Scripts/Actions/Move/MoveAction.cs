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
        GameObject virtualArrow = vPlayer.init(vPlayer.arrow, value);
        vPlayer.cell.getCellRelative(value).placeBoardPiece(vPlayer);
        return new List<GameObject>(new GameObject[] {virtualArrow});
    }

    public override ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return new MoveSelection(vPlayer);
    }
    public override Sprite getIcon(){
        return Resources.Load<Sprite>("sword-icon");;
    }
}