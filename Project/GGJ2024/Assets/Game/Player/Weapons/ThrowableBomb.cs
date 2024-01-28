using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableBomb : MonoBehaviour
{
    [SerializeField]
    GameObject gasArea;

    public Rigidbody rigidBody;

    [SerializeField]
    GameObject audioPrefab;

    public void Throw(Vector3 direction)
    {
        rigidBody.AddForce(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(gasArea)
            Instantiate(gasArea, transform.position, Quaternion.identity);
        if(audioPrefab)
            Instantiate(audioPrefab, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
