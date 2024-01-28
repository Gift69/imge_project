
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Action
{
    public enum Type
    {
        MOVE,
        SWORD_SLASH,
        UNKNOWN
    };

    public Type type = Type.UNKNOWN;
    public bool selected = false;
    protected bool hasValue = false;
    protected HexField.Coord value;

    public Button selectableButton;


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

    public void unsetValue()
    {
        hasValue = false;
    }

    public HexField.Coord getValue()
    {
        return value;
    }

    public virtual Sprite getIcon(){
        return null;
    }

    public SyncAction toSync()
    {
        return new SyncAction(type, value, hasValue);
    }
}