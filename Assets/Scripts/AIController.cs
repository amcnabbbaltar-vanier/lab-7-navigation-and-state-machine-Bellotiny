using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public StateMachine StateMachine { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Animator Animator { get; private set; }
    
    public Transform[] Waypoints;
    public Transform Player;
    public float SightRange = 10f;
    public float AttackRange = 2f;
    public LayerMask PlayerLayer;
    public StateType currentState;

    void Start(){
        Agent = GetComponent<NavMeshAgent>();
        Animator = GetComponent<Animator>();
        StateMachine = new StateMachine();
        StateMachine.AddState(new IdleState(this));
        StateMachine.AddState(new PatrolState(this));

        StateMachine.AddState(new ChaseState(this));
        StateMachine.AddState(new AttackState(this));
        StateMachine.TransitionToState(StateType.Idle);
    }

    void Update(){
        StateMachine.Update();
        Animator.SetFloat("CharacterSpeed", Agent.velocity.magnitude);
        currentState = StateMachine.GetCurrentStateType();
    }

    public bool CanSeePlayer(){
        float distanceToPlayer = Vector3.Distance(transform.position,
        Player.position);

        if (distanceToPlayer <= SightRange)
        {
            return true;
        }
        return false;
    }

    public bool IsPlayerInAttackRange()
    {
        float distanceToPlayer = Vector3.Distance(transform.position,
        Player.position);
        return distanceToPlayer <= AttackRange;
    }

}
