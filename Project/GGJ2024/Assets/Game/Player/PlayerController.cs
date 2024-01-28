using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LevelObject
{
    private List<PlayerWeapon> weapons = new List<PlayerWeapon>();

    protected override void Start()
    {
        base.Start();

        GetComponents(weapons);

        Level.Player = this;
    }

    public IReadOnlyList<PlayerWeapon> Weapons => weapons;
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

public class PlayerWeapon : PlayerComponent
{
    public enum WeaponType
    {
        Ammunition,
        Cooldown
    }

    public virtual WeaponType Type => WeaponType.Ammunition;

    public virtual int Ammunition => 0;

    public virtual float TotalCooldown => 0;

    public virtual float Cooldown => 0;

    public virtual bool CanFire => false;

    public virtual KeyCode InputIdentifier => KeyCode.None;

    [SerializeField]
    public Sprite Sprite { get; private set; }

    public virtual void FireWeapon() { }
}

public class PlayerCooldownWeapon : PlayerWeapon
{
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float totalCooldown;

    public override WeaponType Type => WeaponType.Cooldown;

    public override float TotalCooldown => totalCooldown;

    public override float Cooldown => cooldown;

    public override bool CanFire => Mathf.Approximately(cooldown, 0);
}

public class PlayerAmmunitionWeapon : PlayerWeapon
{
    public override WeaponType Type => WeaponType.Ammunition;

    [SerializeField]
    private int ammunition;

    public override int Ammunition => ammunition;

    public override bool CanFire => ammunition > 0;
}