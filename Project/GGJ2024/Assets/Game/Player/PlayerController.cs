using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : LevelObject
{
    private List<PlayerWeapon> weapons = new List<PlayerWeapon>();

    [SerializeField]
    GameObject ragdoll;

    [SerializeField]
    GameObject modell;

    bool died = false;

    protected override void Start()
    {
        base.Start();

        GetComponents(weapons);

        Level.Player = this;
    }

    public IReadOnlyList<PlayerWeapon> Weapons => weapons;

    public void Death()
    {
        if (died)
            return;
        died = true;
        EnableRagdoll(true);
        GetComponent<PlayerMovement>().enabled = false;
        GameObject.Find("HeadUpDisplay").GetComponent<HeadUpDisplay>().Death();
    }

    public void EnableRagdoll(bool value)
    {
        ragdoll.SetActive(value);
        modell.SetActive(!value);
        if (value)
        {
            ragdoll.transform.localPosition = modell.transform.localPosition;
            ragdoll.transform.localRotation = modell.transform.localRotation;
            CopyTransformToRagdoll(modell.transform, ragdoll.transform);
        }
    }

    void CopyTransformToRagdoll(Transform parent, Transform ragdollParent)
    {
        foreach (Transform child in parent)
        {
            Transform ragdollChild = ragdollParent.Find(child.name);
            if (ragdollChild)
            {
                ragdollChild.localPosition = child.localPosition;
                ragdollChild.localRotation = child.localRotation;
                if (child.childCount > 0)
                    CopyTransformToRagdoll(child, ragdollChild);
            }
        }
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

    [field:SerializeField]
    public Sprite Sprite { get; private set; }

    private void Update()
    {
        if (CanFire && Input.GetKeyDown(InputIdentifier))
            FireWeapon();
        if (Input.GetKeyUp(InputIdentifier))
            EndUsingWeapon();
    }

    public virtual void FireWeapon() { }

    public virtual void EndUsingWeapon() { }
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
    protected int ammunition;

    public override int Ammunition => ammunition;

    public override bool CanFire => ammunition > 0;
}