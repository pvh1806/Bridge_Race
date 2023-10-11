using UnityEngine;
using UnityEngine.AI;

public class AIMovement : CharacterController
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    private Vector3 destination;

    public IState<AIMovement> currentState;

    void Start()
    {
        ChangeState(new PatrolState());
        ChangeAnimation(AnimType.Running);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.OnExcute(this);
            isCanMove();
        }
    }
    public void ChangeState(IState<AIMovement> state)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
 
        currentState = state;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public bool isDestination => Vector3.Distance(destination,Vector3.right * transform.position.x + Vector3.forward * transform.position.z) < 0.1f;

    public void SetDestination(Vector3 position)  
    {
        destination = position;
        destination.y= 0;
        navMeshAgent.SetDestination(position);
    }
}