using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunFieldSelection : ActionSelection
{
    public StunFieldSelection(VirtualPlayer player) : base(player)
    {
    }

    public override Cell[] getInnerIndicatorCells(HexField.Coord coord)
    {
        return new Cell[] {vPlayer.cell.getCellRelative(coord)};
    }

    public override Cell[] getOuterIndicatorCells()
    {
        return vPlayer.cell.getArea(13);
    }
}
