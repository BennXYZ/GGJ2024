using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class GasArea : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    [SerializeField]
    private SphereCollider trigger;

    [field:SerializeField]
    public float Size { get; private set; }

    private void Start()
    {
        Debug.Assert(particles);
        Debug.Assert(trigger);
    }

    public void SetSize(float size)
    {
        Size = size;

        trigger.radius = Size;
        ParticleSystem.ShapeModule shape = particles.shape;
        shape.radius = Mathf.Max(0, Size - 0.7f);
    }

    private void OnTriggerEnter(Collider other)
    {
        IGasReceiver gasReceiver = other.gameObject.GetComponent<IGasReceiver>();
        if (gasReceiver != null)
        {
            gasReceiver.EnteredGasArea(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IGasReceiver gasReceiver = other.gameObject.GetComponent<IGasReceiver>();
        if (gasReceiver != null)
        {
            gasReceiver.ExitedGasArea(this);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        SphereCollider a = GetComponent<SphereCollider>();
        Gizmos.DrawWireSphere(a.transform.position, a.radius);
    }

    private void OnValidate()
    {
        SetSize(Size);
    }
}

public interface IGasReceiver
{
    void EnteredGasArea(GasArea gasArea);
    void ExitedGasArea(GasArea gasArea);
}
