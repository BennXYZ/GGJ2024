using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasArea : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    [field: SerializeField]
    public float Size { get; private set; }

    [SerializeField]
    bool isPlayerWeapon = false;

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

    private void OnEnable()
    {
        GameManager.Instance.CurrentLevel.AddGasArea(this);
    }

    private void Update()
    {
        if (isPlayerWeapon)
            return;

        if (Time.time < startTime + duration)
            return;

        if(particles)
            particles.Stop();

        if (Time.time < startTime + duration + particles.main.startLifetime.constant)
            return;

        Destroy(gameObject);
    }

    public void SetSize(float size)
    {
        Size = size;

        if (particles)
        {
            ParticleSystem.ShapeModule shape = particles.shape;
            shape.radius = Mathf.Max(0, Size - 0.7f);
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.CurrentLevel.RemoveGasArea(this);
        SetReceivers(new List<IGasReceiver>());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, Size);
    }

    private void OnValidate()
    {
        SetSize(Size);
    }

    public void SetReceivers(List<IGasReceiver> inArea)
    {
        for (int i = currentReceivers.Count - 1; i >= 0; i--)
        {
            IGasReceiver receiver = currentReceivers[i];
            if (!inArea.Contains(receiver))
            {
                currentReceivers.RemoveAt(i);
                receiver.ExitedGasArea(this);
            }
        }

        for (int i = 0; i < inArea.Count; i++)
        {
            IGasReceiver receiver = inArea[i];
            if (!currentReceivers.Contains(receiver))
            {
                currentReceivers.Add(receiver);
                receiver.EnteredGasArea(this);
            }
        }
    }

    List<IGasReceiver> currentReceivers = new List<IGasReceiver>();
}

public interface IGasReceiver
{
    void EnteredGasArea(GasArea gasArea);
    void ExitedGasArea(GasArea gasArea);

    Vector3 Position { get; }
}
