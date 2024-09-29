using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : State
{
    float timer;
    


    public Idle(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player, Transform[] _waypoints) : base(_npc, _agent, _animator, _player, _waypoints)
    {
        stateName = STATE.IDLE;
    }

    public override void Enter()
    {
        agent.isStopped = true;
        animator.SetTrigger("idle");
        base.Enter();
    }

    public override void Update()
    {
        timer += Time.deltaTime;
        if (timer > 2)
        {
           nextState = new Patrol(npc, agent, animator, player, waypoints);
            stage = EVENT.EXIT;
        }

    }

    public override void Exit()
    {
        animator.ResetTrigger("idle");
        base.Exit();
    }


    

}
