using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAction : Action
{
    public BombAction() {
        type = Type.BOMB;
    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        GameObject virtualBomb = GameObject.Instantiate(vPlayer.bomb);
        virtualBomb.transform.position = vPlayer.cell.getCellRelative(this.value).transform.position;
        //vPlayer.cell.getCellRelative(value).placeBoardPiece(vPlayer);
        return new List<GameObject>(new GameObject[] {virtualBomb});

    }

    public override ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return new BombSelection(vPlayer);
    }

    public override Sprite getIcon()
    {
        return Resources.Load<Sprite>("BOMB");;
    }

    public override bool requiresInput()
    {
        return true;
    }
}
