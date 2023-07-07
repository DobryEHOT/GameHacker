namespace GameOnWinForms
{
    enum ObjectTag
    {
        Wall,
        Player,
        InteractiveObject
    }

    enum InteractiveObjectTag
    {
        Button,
        ControllPanel,
        Door,
        MovePanel,
        Holl
    }

    enum CommandType
    {
        Door,
        MovePanel
    }

    enum AllCommands
    {
        Open,
        Closed,
        Move
    }

    public enum PanelPositions
    {
        First,
        Second
    }
}
