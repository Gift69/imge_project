using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public abstract class Character
{
    public virtual Action GetMoveAction() {
        return new MoveAction();
    }
    public virtual Action GetCoinAction() {
        return new MoveAction();
    }
    public virtual Action GetDoNothingAction() {
        return new MoveAction();
    }
    public abstract Action GetSpecialAction1();
    public abstract Action GetSpecialAction2();

}