public class MoveSelection : ActionSelection
{
    public MoveSelection(VirtualPlayer player) : base(player)
    {
    }

    public override Cell[] getInnerIndicatorCells(HexField.Coord coord)
    {
        return vPlayer.cell.getCellRelative(coord).getSequence(coord);
    }

    public override Cell[] getOuterIndicatorCells()
    {
        return vPlayer.cell.getCircle(1);
    }
}