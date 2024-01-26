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