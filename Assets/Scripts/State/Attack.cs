using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : State
{
    private float timer;

    public Attack(GameObject _npc, NavMeshAgent _agent, Animator _animator, Transform _player, Transform[] _waypoints) : base(_npc, _agent, _animator, _player, _waypoints)
    {
        stateName = STATE.ATTACK;
        agent.isStopped = true;
    }

    public override void Enter()
    {
        timer = 0;
        animator.SetTrigger("attack");
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
        animator.ResetTrigger("attack");
        base.Exit();
    }
}
