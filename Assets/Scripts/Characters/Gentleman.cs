public class Gentleman : Character
{
    public override Action GetSpecialAction1()
    {
        return new DoNothingAction();
    }

    public override Action GetSpecialAction2()
    {
        return new DoNothingAction();
    }

}