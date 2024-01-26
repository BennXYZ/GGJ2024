using UnityEngine;

[RequireComponent(typeof(Collider))]
class CollectableCoin : LevelObject
{
    [field: SerializeField]
    [field:Min(1)]
    public int Value { get; private set; } = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            Level.AddCoins(Value);

            // TODO: Maybe play an animation here?

            // Destroy for now
            Destroy(gameObject);
        }
    }
}
