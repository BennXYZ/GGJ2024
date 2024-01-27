using UnityEngine;

public class ExitDoor : LevelObject
{
    [SerializeField]
    Animator vaultDoorAnimator;

    protected override void Start()
    {
        base.Start();

        Level.OnCoinCollectionGoalReached.AddListener(OpenDoor);
    }

    public void OpenDoor()
    {
        vaultDoorAnimator.SetTrigger("OpenVaultDoor");
    }
}
