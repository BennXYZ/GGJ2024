using System;
using UnityEngine;


public class PlayerSprayWeapon : PlayerCooldownWeapon
{
    public override KeyCode InputIdentifier => KeyCode.Mouse0;

    public override void FireWeapon()
    {
        // TODO: Add gas area to player and enable/disable it here
    }
}
