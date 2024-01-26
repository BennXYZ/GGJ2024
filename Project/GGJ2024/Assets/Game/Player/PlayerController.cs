using UnityEngine;

public class PlayerController : LevelObject
{
    protected override void Start()
    {
        base.Start();

        Level.Player = this;
    }
}

[RequireComponent(typeof(PlayerController))]
public class PlayerComponent : MonoBehaviour
{
    private PlayerController controller;

    public PlayerController Controller
    {
        get
        {
            if (controller == null)
                controller = GetComponent<PlayerController>();
            return controller;
        }
    }
}
