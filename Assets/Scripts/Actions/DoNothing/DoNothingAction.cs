using System.Collections.Generic;
using UnityEngine;

public class DoNothingAction : Action
{
    public DoNothingAction()
    {
        type = Type.NOTHING;
    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        return new List<GameObject>();
    }


    public override Sprite getIcon(){
        return Resources.Load<Sprite>("DO NOTHING");
    }

    public override bool requiresInput()
    {
        return false;
    }
}