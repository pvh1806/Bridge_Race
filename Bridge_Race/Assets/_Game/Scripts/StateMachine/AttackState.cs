using UnityEngine;

public class AttackState : IState<AIMovement>
{
    public void OnEnter(AIMovement t)
    {
        Transform target = GameObject.FindWithTag("Finish").transform;
        t.SetDestination(target.position);
    }

    public void OnExcute(AIMovement t)
    {
        if (t.BrickCount == 0)
        {
            t.ChangeState(new PatrolState());
        }
    }

    public void OnExit(AIMovement t)
    {
       
    }
}
