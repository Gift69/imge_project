using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MiningSelection : ActionSelection
{
    public MiningSelection(VirtualPlayer player) : base(player)
    {
    }

    public override Cell[] getInnerIndicatorCells(HexField.Coord coord)
    {
        return vPlayer.cell.getCellRelative(coord).getSequence(coord);
    }

    public override Cell[] getOuterIndicatorCells()
    {
        Cell[] cell= new Cell[6];
        int i = 0;
        foreach (var coord in HexField.Coord.BASE_COORDS)
        { cell[i++] = vPlayer.cell.getCellRelative(coord).getSequence(coord)[
                    vPlayer.cell.getCellRelative(coord).getSequence(coord).Length - 1];
        }

        return cell;
    }
}
