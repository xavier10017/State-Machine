using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class State
{
    public enum STATE 
    { 
        IDLE, PATROL, CHASE, ATTACK
    }

    public enum EVENT
    {
        ENTER, UPDATE, EXIT
    }

    public STATE stateName;
    protected EVENT stage;
    protected GameObject npc;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected Transform player;
    protected Transform[] waypoints;
    protected State nextState;

    float visDist = 15.0f;
    float visAngle = 30.0f;

    public State(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player, Transform[] _waypoints)
    {
        npc = _npc;
        agent = _agent;
        animator = _animator;
        player = _player;
        waypoints = _waypoints;

    }

    public virtual void Enter()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Update()
    {
        stage = EVENT.UPDATE;
    }

    public virtual void Exit()
    {
        stage = EVENT.EXIT;
    }

   /* public State Process()
    {
        if(stage == EVENT.ENTER)
        {
            Enter();
        }
        else if(stage == EVENT.UPDATE)
        {
            Update();
        }
        else
        {
            Exit();
            return nextState;
        }

        return this;
    }*/
    public State Process()
    {
        switch (stage)
        {
            case EVENT.ENTER:
                Enter();
                break;
            case EVENT.UPDATE:
                Update();
                break;
            case EVENT.EXIT:
                Exit();
                return nextState;
        }

        return this;
    }


    public bool CanSeePlayer()
    {
        Vector3 direction = player.position - npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward);

        if (direction.magnitude < visDist && angle < visAngle)
        {
            return true;
        }
        return false;
    }


}
