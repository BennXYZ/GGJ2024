using UnityEngine;

public class PlayerGasBombWeapon : PlayerAmmunitionWeapon
{
    public override KeyCode InputIdentifier => KeyCode.Q;

    public override void FireWeapon()
    {
        // TODO: Instantiate physics simulated bomb
    }
}
