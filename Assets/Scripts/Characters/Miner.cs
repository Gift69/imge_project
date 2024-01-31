public class Miner : Character
{
    public override Action GetSpecialAction1()
    {
        return new BombAction();
    }

    public override Action GetSpecialAction2()
    {
        return new MiningAction();
    }

}