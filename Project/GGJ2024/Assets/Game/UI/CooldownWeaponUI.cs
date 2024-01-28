using System;
using UnityEngine;
using UnityEngine.UI;

public class CooldownWeaponUI : WeaponUI<PlayerCooldownWeapon>
{
    [SerializeField]
    private Image CooldownDisplay;
    private float shownCooldown;

    protected override void InitializeWeapon()
    {
        shownCooldown = -1;
    }

    private void Update()
    {
        if (Weapon != null)
        {
            SetCooldown(Weapon.Cooldown / Weapon.TotalCooldown);
        }
    }

    private void SetCooldown(float cooldown)
    {
        if (!Mathf.Approximately(cooldown, shownCooldown))
        {
            shownCooldown = cooldown;
            CooldownDisplay.fillAmount = shownCooldown;
        }
    }
}
