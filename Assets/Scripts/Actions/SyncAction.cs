public struct SyncAction
{
    public Action.Type type;
    public HexField.Coord value;

    public SyncAction(Action.Type type, HexField.Coord value)
    {
        this.type = type;
        this.value = value;
    }
}