using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : Action
{
    public ShootAction() {
        type = Type.SHOOT;
    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        GameObject[] shooting = new GameObject[vPlayer.cell.getCellRelative(value).getSequence(value).Length];
        //GameObject virtualShot = GameObject.Instantiate(vPlayer.shoot);
        for (int i = 0; i < vPlayer.cell.getCellRelative(value).getSequence(value).Length; i++)
        {
            GameObject virtualShot = GameObject.Instantiate(vPlayer.shoot);
            virtualShot.transform.position =
                vPlayer.cell.getCellRelative(value).getSequence(value)[i].transform.position;
            shooting[i] = virtualShot;
        }


        return new List<GameObject>(shooting);
        //return new List<GameObject>();
    }

    public override ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return new ShootSelection(vPlayer);
    }

    public override Sprite getIcon()
    {
        return Resources.Load<Sprite>("MAGIC SHOOT");;
    }

    public override bool requiresInput()
    {
        return true;
    }
}
