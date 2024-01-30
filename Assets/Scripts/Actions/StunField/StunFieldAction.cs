using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFieldAction : Action
{
    public override void execute(Player player)
    {
        throw new System.NotImplementedException();
    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        GameObject virtualStunField = GameObject.Instantiate(vPlayer.stunfield);
        virtualStunField.transform.position = vPlayer.cell.getCellRelative(this.value).transform.position;
        return new List<GameObject>(new []{virtualStunField});
    }

    public override ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return new StunFieldSelection(vPlayer);
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
