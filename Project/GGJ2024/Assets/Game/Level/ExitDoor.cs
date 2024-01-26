public class ExitDoor : LevelObject
{
    protected override void Start()
    {
        base.Start();

        Level.OnCoinCollectionGoalReached.AddListener(OpenDoor);
    }

    public void OpenDoor()
    {
        Destroy(gameObject);
    }
}
