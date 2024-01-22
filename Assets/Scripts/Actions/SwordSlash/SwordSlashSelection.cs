public class SwordSlashSelection : ActionSelection
{

    public SwordSlashSelection(VirtualPlayer player) : base(player)
    {
    }


    public override Cell[] getInnerIndicatorCells(HexField.Coord coord)
    {
        int index = -1;
        for(int i = 0; i < HexField.Coord.BASE_COORDS.Length; i++)
        {
            if (HexField.Coord.BASE_COORDS[i] == coord)
                index = i;
        }
        return new Cell[] {
            vPlayer.cell.getCellRelative(HexField.Coord.BASE_COORDS[(index + 5) % 6]),
            vPlayer.cell.getCellRelative(coord),
            vPlayer.cell.getCellRelative(HexField.Coord.BASE_COORDS[(index + 1) % 6]),
        };
    }

    public override Cell[] getOuterIndicatorCells()
    {
        return vPlayer.cell.getCircle(1);
    }
}