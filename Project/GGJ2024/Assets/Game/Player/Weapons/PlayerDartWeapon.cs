using UnityEngine;

public class PlayerDartWeapon : PlayerCooldownWeapon
{
    public override KeyCode InputIdentifier => KeyCode.Mouse1;

    public override void FireWeapon()
    {
        // TODO: Hitscan an enemy and inject the gas
    }
}
