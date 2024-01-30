using System.Collections.Generic;
using UnityEngine;

public class SwordSlashAction : Action
{
    public SwordSlashAction()
    {
        type = Type.SWORD_SLASH;
    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        return new List<GameObject>();
    }

    public override ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return new SwordSlashSelection(vPlayer);
    }

    public override bool requiresInput()
    {
        return true;
    }
}
