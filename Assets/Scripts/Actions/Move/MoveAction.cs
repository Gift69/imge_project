using System.Collections.Generic;
using UnityEngine;

public class MoveAction : Action
{
    public MoveAction()
    {
        type = Type.MOVE;
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

    public override bool requiresInput()
    {
        return true;
    }
}