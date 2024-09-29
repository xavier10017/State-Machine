using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrol :State
{
    int currentIndex = -1;

    public Patrol(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player, Transform[] _waypoints) : base(_npc, _agent, _animator, _player, _waypoints)
    {
        stateName = STATE.PATROL;
        agent.speed = 1.5f;
        agent.isStopped = false;
    }

    public override void Enter()
    {
        float lastDist = Mathf.Infinity;

        for(int i = 0; i < waypoints.Length; i++)
        {
            Transform thisWP = waypoints[i];
            float distance = Vector3.Distance(npc.transform.position, thisWP.position);
            if(distance < lastDist)
            {
                currentIndex = i - 1;
                lastDist = distance;
            }
        }
        animator.SetTrigger("walk");
        base.Enter();
    }

    public override void Update()
    {
        if(agent.remainingDistance < 1)
        {
            if (currentIndex >= waypoints.Length - 1)
                currentIndex = 0;
            else
                currentIndex++;

            agent.SetDestination(waypoints[currentIndex].position);
        }

        if (CanSeePlayer())
        {
            nextState = new Chase(npc, agent, animator, player, waypoints);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        animator.ResetTrigger("walk");
        base.Exit();
    }

}
