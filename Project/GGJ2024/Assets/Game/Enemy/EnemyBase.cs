using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyBase : LevelObject, IGasReceiver
{
    [field:SerializeField]
    public ParticleSystem SleepParticles { get; private set; }

    private CapsuleCollider mainCollider;

    public NavMeshAgent Agent { get; private set; }

    public EnemyStateMachine StateMachine { get; private set; } = new EnemyStateMachine();
    public StealthState StealthState => StateMachine.StealthState;

    public Vector3 StartPosition { get; set; }

    public Vector3 PositionOnGround => transform.position - new Vector3(0, mainCollider.height / 2);

    public bool InGas { get; private set; }

    protected override void Start()
    {
        base.Start();

        StartPosition = transform.position;

        Level.AddEnemy(this);

        mainCollider = GetComponent<CapsuleCollider>();
        Agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void Update()
    {
        StateMachine.Update();
    }

    private void OnDestroy()
    {
        if (Level != null)
            Level.RemoveEnemy(this);
    }

    public void EnteredGasArea(GasArea gasArea)
    {
        InGas = true;
        StateMachine.SetState<EnemyLaughState>();
    }

    public void ExitedGasArea(GasArea gasArea)
    {
        InGas = false;
    }

    public bool CanSeePlayer()
    {
        if (StateMachine.Blackboard.Player != null)
        {
            Vector3 playerPosition = StateMachine.Blackboard.Player.transform.position;
            Vector3 enemyPosition = transform.position;

            float distance = Vector3.Distance(playerPosition, enemyPosition);
            float angle = Vector3.Angle(playerPosition - enemyPosition, transform.forward);

            if (distance < 10.0f && angle < 30)
            {
                if (Physics.Raycast(transform.position, playerPosition - enemyPosition, out RaycastHit hit, 10.0f))
                {
                    return true;
                }
            }
        }
        return false;
    }
}
