
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Action
{
    public enum Type
    {
        NOTHING,
        MOVE,
        COIN,
        SHOOT,
        SWORD_SLASH,
        BOMB,
        MINING,
        STUN_FIELD
    };

    public Type type = Type.NOTHING;
    public bool selected = false;
    protected bool hasValue = false;
    protected HexField.Coord value;

    public Button selectableButton;


    public abstract List<GameObject> executeVirtual(VirtualPlayer vPlayer);

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
        return new SyncAction(type, value);
    }
}