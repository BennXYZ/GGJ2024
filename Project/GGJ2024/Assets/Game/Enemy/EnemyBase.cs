using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyBase : LevelObject, IGasReceiver
{
    public NavMeshAgent Agent { get; private set; }

    protected override void Start()
    {
        base.Start();

        Level.AddEnemy(this);

        Agent = GetComponent<NavMeshAgent>();
        Agent.SetDestination(Level.Player.transform.position);
    }

    private void OnDestroy()
    {
        if (Level != null)
            Level.RemoveEnemy(this);
    }

    public void EnteredGasArea(GasArea gasArea)
    {
        Destroy(gameObject);
    }

    public void ExitedGasArea(GasArea gasArea)
    {

    }
}
