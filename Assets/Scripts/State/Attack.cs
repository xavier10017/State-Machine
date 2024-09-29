using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    
    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player, Transform[] _waypoints) : base(_npc, _agent, _animator, _player, _waypoints)
    {
        stateName = STATE.ATTACK;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        animator.SetTrigger("attack");
        base.Enter();
    }

    public override void Update()
    {
        
        float distanceToPlayer = Vector3.Distance(npc.transform.position, player.position);

        if (distanceToPlayer > 2.0f) 
        {
            nextState = new Chase(npc, agent, animator, player, waypoints);
            stage = EVENT.EXIT;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("attack");
        base.Exit();
    }
}
