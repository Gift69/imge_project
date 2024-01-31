using System.Collections.Generic;
using UnityEngine;

public class CoinAction : Action
{
    public CoinAction()
    {
        type = Type.COIN;
    }

    public override List<GameObject> executeVirtual(VirtualPlayer vPlayer)
    {
        return new List<GameObject>();
    }

    public override Sprite getIcon(){
        return Resources.Load<Sprite>("coin_pickup");;
    }

    public override bool requiresInput()
    {
        return false;
    }
}