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
        base.Enter();
        currentIndex = 0;
        agent.SetDestination(waypoints[currentIndex].position);
        animator.SetTrigger("walk");
       
        
    }

    public override void Update()
    {
        
        if (agent.remainingDistance < 1f && !agent.pathPending)
        {
           
            currentIndex++;

            
            if (currentIndex >= waypoints.Length)
                currentIndex = 0;

           
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
