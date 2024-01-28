public abstract class Character
{
    public virtual Action GetMoveAction() {
        return new BombAction();
        //return new MoveAction();
    }
    public virtual Action GetCoinAction() {
        return new BombAction();
    }
    public virtual Action GetDoNothingAction() {
        return new BombAction();
    }
    public abstract Action GetSpecialAction1();
    public abstract Action GetSpecialAction2();

}
