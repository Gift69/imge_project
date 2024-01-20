using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SwordSlashAction : Action
{
    public override void execute(Player player)
    {
        throw new NotImplementedException();
    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        throw new NotImplementedException();
    }

    public override ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return new SwordSlashSelection(vPlayer);
    }
}
