using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(CapsuleCollider))]
public class EnemyBase : LevelObject, IGasReceiver
{
    [field:SerializeField]
    public ParticleSystem SleepParticles { get; private set; }

    private CapsuleCollider mainCollider;
    private new Rigidbody rigidbody;

    public NavMeshAgent Agent { get; private set; }

    public EnemyStateMachine StateMachine { get; private set; } = new EnemyStateMachine();
    public StealthState StealthState => StateMachine.StealthState;

    [SerializeField]
    TextMeshPro DebugText;

    [SerializeField]
    Animator animator;
    public Animator Animator => animator;

    [SerializeField]
    GameObject ragdoll;
    public GameObject Ragdoll => ragdoll;

    public Vector3 StartPosition { get; set; }
    public Quaternion StartRotation { get; set; }

    public Vector3 PositionOnGround => transform.position - new Vector3(0, mainCollider.height / 2);

    HashSet<GasArea> inGasArea = new HashSet<GasArea>();

    public bool InGas => inGasArea.Count > 0;

    public Vector3 Position => transform.position;

    protected override void Start()
    {
        base.Start();

        StartPosition = transform.position;
        StartRotation = transform.rotation;

        Level.AddEnemy(this);

        mainCollider = GetComponent<CapsuleCollider>();
        rigidbody = GetComponent<Rigidbody>();
        Agent = GetComponent<NavMeshAgent>();

#if !DEBUG
        Destroy(DebugText);
#endif
    }

    public void EnableRagdoll(bool value)
    {
        Ragdoll.SetActive(value);
        animator.gameObject.SetActive(!value);
        mainCollider.enabled = false;
        if (value)
        {
            ragdoll.transform.localPosition = animator.transform.localPosition;
            ragdoll.transform.localRotation = animator.transform.localRotation;
            CopyTransformToRagdoll(animator.transform, ragdoll.transform);
        }
    }

    void CopyTransformToRagdoll(Transform parent, Transform ragdollParent)
    {
        foreach(Transform child in parent)
        {
            Transform ragdollChild = ragdollParent.Find(child.name);
            if (ragdollChild)
            {
                ragdollChild.localPosition = child.localPosition;
                ragdollChild.localRotation = child.localRotation;
                if (child.childCount > 0)
                    CopyTransformToRagdoll(child, ragdollChild);
            }
        }
    }

    protected virtual void Update()
    {
        StateMachine.Update();
        if (DebugText)
            DebugText.SetText(StateMachine.CurrentState?.ToString());
        Animator.SetFloat("MoveSpeed", Agent.velocity.magnitude);
    }

    private void OnDestroy()
    {
        if (Level != null)
            Level.RemoveEnemy(this);
    }

    public void EnteredGasArea(GasArea gasArea)
    {
        inGasArea.Add(gasArea);
        StateMachine.SetState<EnemyLaughState>();
    }

    public void ExitedGasArea(GasArea gasArea)
    {
        inGasArea.Remove(gasArea);
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
