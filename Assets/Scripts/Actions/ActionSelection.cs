
using UnityEngine.TerrainUtils;

public abstract class ActionSelection
{
    protected VirtualPlayer vPlayer;

    public ActionSelection(VirtualPlayer player) {  vPlayer = player; }

    public abstract Cell[] getOuterIndicatorCells();
    public abstract Cell[] getInnerIndicatorCells(HexField.Coord coord);
}