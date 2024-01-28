using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningAction : Action
{
    public override void execute(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        //GameObject[] mining = new GameObject[vPlayer.cell.getCellRelative(value).getSequence(value).Length];
        vPlayer.cell.getCellRelative(value).placeBoardPiece(vPlayer);
     //value = vPlayer.cell.getCellRelative(value).getSequence(value)[vPlayer.cell.getCellRelative(value).getSequence(value).Length-1].getCoord();
     for (int i = 0; i < vPlayer.cell.getCellRelative(value).getSequence(value).Length; i++)
     {
         GameObject virtualMining = GameObject.Instantiate(vPlayer.mining);
         virtualMining.transform.position =
             vPlayer.cell.getCellRelative(value).getSequence(value)[i].transform.position;
         mining[i] = virtualMining;
     }
     return new List<GameObject>();
        //return new List<GameObject>(mining);
    }

    public override ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return new MiningSelection(vPlayer);
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
