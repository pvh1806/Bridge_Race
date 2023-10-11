using UnityEngine;
public class PatrolState : IState<AIMovement>
{
    private int targetBrick;
    public void OnEnter(AIMovement t)
    {
        targetBrick = Random.Range(2, 10);
        SeekTarget(t);
    }

    public void OnExcute(AIMovement t)
    {
        if (t.isDestination)
        {
            if (t.BrickCount >= targetBrick)
            {
                t.ChangeState(new AttackState());
            }
            else
            {
                SeekTarget(t);
            }
        }
    }

    public void OnExit(AIMovement t)
    {
        
    }

    private void SeekTarget(AIMovement t)
    {
        if (t.stage != null)
        {
            Brick brick = t.stage.SeekBrickPoint(t.color);
            if (brick == null)
            {
                t.ChangeState(new AttackState());
            }
            else
            {
                t.SetDestination(brick.transform.position);
            }
        }
        else
        {
            t.SetDestination(t.transform.position);
        }

    }
}
