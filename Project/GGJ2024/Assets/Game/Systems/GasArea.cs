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

    [SerializeField]
    float duration;
    float startTime;

    private void Start()
    {
        //I just want a rectangle-Gas-Area for the Player-Spray...
        //Debug.Assert(particles);
        //Debug.Assert(trigger);
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time < startTime + duration)
            return;

        if (trigger.enabled)
            trigger.enabled = false;

        particles.Stop();

        if (Time.time < startTime + duration + particles.main.startLifetime.constant)
            return;

        Destroy(gameObject);
    }

    public void SetSize(float size)
    {
        Size = size;

        if(trigger)
            trigger.radius = Size;
        if(particles)
        {
            ParticleSystem.ShapeModule shape = particles.shape;
            shape.radius = Mathf.Max(0, Size - 0.7f);
        }
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
