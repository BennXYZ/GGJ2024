using UnityEngine;

public class PlayerGasBombWeapon : PlayerAmmunitionWeapon
{
    public override KeyCode InputIdentifier => KeyCode.Q;

    [SerializeField]
    ThrowableBomb bombPrefab;

    [SerializeField]
    float throwStrength;

    public override void FireWeapon()
    {
        ammunition--;
        Instantiate(bombPrefab, transform.position + transform.forward * 2 + Vector3.up, transform.rotation).Throw(Camera.main.transform.forward * throwStrength);
    }
}
