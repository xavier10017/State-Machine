using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chase : State
{
    



    public Chase(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player, Transform[] _waypoints) : base(_npc, _agent, _animator, _player, _waypoints)
    {
        stateName = STATE.CHASE;
        agent.speed = 5;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        animator.SetTrigger("run");
        base.Enter();
    }


    public override void Update()
    {
        agent.SetDestination(player.position);

        if (agent.hasPath)
        {
            float distanceToPlayer = Vector3.Distance(npc.transform.position, player.position);

            if (distanceToPlayer <= 2.0f) 
            {
                nextState = new Attack(npc, agent, animator, player, waypoints);
                stage = EVENT.EXIT;
            }
            else if (!CanSeePlayer())
            {
                nextState = new Patrol(npc, agent, animator, player, waypoints);
                stage = EVENT.EXIT;
            }
        }
    }


    public override void Exit()
    {
        animator.ResetTrigger("run");
        base.Exit();
    }


}
