using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSelection : ActionSelection
{
    private Cell selected;
    public BombSelection(VirtualPlayer player) : base(player)
    {
    }

    public override Cell[] getInnerIndicatorCells(HexField.Coord coord)
    {
        int index= -1;
        for (int i = 0; i < HexField.Coord.BASE_COORDS.Length; i++)
        {
            if (HexField.Coord.BASE_COORDS[i] == coord)
            {
                index = i;
            }
        }

        //vPlayer.cell.getCoord();
       selected =vPlayer.cell.getCellRelative(HexField.Coord.BASE_COORDS[(index + 0) % 6]);

        return new Cell[]
            {
                selected.getCellRelative(HexField.Coord.BASE_COORDS[(index +1) % 6]),
                selected.getCellRelative(HexField.Coord.BASE_COORDS[(index +2) % 6]),
                selected.getCellRelative(HexField.Coord.BASE_COORDS[(index + 3) % 6]),
                selected.getCellRelative(HexField.Coord.BASE_COORDS[(index + 4) % 6]),
                selected.getCellRelative(HexField.Coord.BASE_COORDS[(index + 5) % 6]),
                selected.getCellRelative(coord),
                vPlayer.cell.getCellRelative(coord)


            };

    }

    public override Cell[] getOuterIndicatorCells()
    {


        return vPlayer.cell.getCircle(1);
            //selected.getCircle(1);

    }
}
