public class Knight : Character
{
    public override Action GetSpecialAction1()
    {
        return new SwordSlashAction();
    }

    public override Action GetSpecialAction2()
    {
        return new DoNothingAction();
    }

}