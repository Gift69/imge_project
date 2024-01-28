public struct SyncAction
{
    public Action.Type type;
    public HexField.Coord value;
    public bool hasValue;

    public SyncAction(Action.Type type, HexField.Coord value, bool hasValue)
    {
        this.type = type;
        this.value = value;
        this.hasValue = hasValue;
    }

    public Action toAction()
    {
        Action ret = null;
        switch (type)
        {
            case Action.Type.MOVE:
                ret = new MoveAction();
                break;
            case Action.Type.SWORD_SLASH:
                ret = new SwordSlashAction();
                break;
        }
        if(hasValue)
            ret.setValue(value);
        return ret;
    }
}