public class Mage : Character
{
    public override Action GetSpecialAction1()
    {
        return new StunFieldAction();
    }

    public override Action GetSpecialAction2()
    {
        return new ShootAction();
    }

}