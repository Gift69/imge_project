
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TerrainUtils;

public abstract class Action
{
    public bool selected = false;
    protected bool hasValue = false;
    protected HexField.Coord value;


    public abstract List<GameObject> executeVirtual(VirtualPlayer vPlayer);
    public abstract void execute(Player player);

    public virtual ActionSelection getActionSelection(VirtualPlayer vPlayer)
    {
        return null;
    }

    public virtual bool requiresInput()
    {
        return false;
    }

    public bool isValid() { 
        return !requiresInput() || hasValue;
    }

    public void setValue(HexField.Coord value)
    {
        hasValue = true;
        this.value = value;
    }

    public void unsetValue(HexField.Coord value)
    {
        hasValue = false;
    }

    public HexField.Coord getValue()
    {
        return value;
    }
}