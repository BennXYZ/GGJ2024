using UnityEngine;

public class PlayerGasBombWeapon : PlayerAmmunitionWeapon
{
    public override KeyCode InputIdentifier => KeyCode.Q;

    [SerializeField]
    ThrowableBomb bombPrefab;

    [SerializeField]
    GameObject audioPrefab;

    [SerializeField]
    float throwStrength;

    public override void FireWeapon()
    {
        ammunition--;
        Instantiate(bombPrefab, transform.position + transform.forward * 2 + Vector3.up, transform.rotation).Throw(Camera.main.transform.forward * throwStrength);
        Instantiate(audioPrefab, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<CollectabltAmmo>())
        {
            ammunition++;
            Destroy(other.gameObject);
        }
    }
}
