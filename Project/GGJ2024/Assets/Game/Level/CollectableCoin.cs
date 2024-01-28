using UnityEngine;

[RequireComponent(typeof(Collider))]
class CollectableCoin : LevelObject
{
    [field: SerializeField]
    [field:Min(1)]
    public int Value { get; private set; } = 1;

    public float rotationSpeed;
    public Transform visualsTransform;
    [SerializeField]
    GameObject audioPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constants.PLAYER_TAG))
        {
            Level.AddCoins(Value);

            // TODO: Maybe play an animation here?
            Instantiate(audioPrefab, transform.position, transform.rotation);

            // Destroy for now
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        visualsTransform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
    }
}
