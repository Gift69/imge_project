public abstract class Character
{
    public virtual Action GetMoveAction() {
        return new MoveAction();
        //return new MoveAction();
    }
    public virtual Action GetCoinAction()
    {
        return new StunFieldAction();
    }
    public virtual Action GetDoNothingAction()
    {
        return new MiningAction();
    }
    public abstract Action GetSpecialAction1();
    public abstract Action GetSpecialAction2();

}
