public class TestCharacter : Character
{
    public override Action GetSpecialAction1()
    {
        return new ShootAction();
    }

    public override Action GetSpecialAction2()
    {
        return new MoveAction();
    }

}