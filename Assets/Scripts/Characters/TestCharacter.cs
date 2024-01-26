public class TestCharacter : Character
{
    public override Action GetSpecialAction1()
    {
        return new MoveAction();
    }

    public override Action GetSpecialAction2()
    {
        return new MoveAction();
    }

}