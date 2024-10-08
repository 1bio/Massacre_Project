using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MonsterStateMachine : StateMachine
{
    protected Monster p_monster;

    [SerializeField] protected MonsterBehaviour p_currentBehaviour;

    private void Awake()
    {
        p_monster = GetComponent<Monster>();
    }

    private new void Update()
    {
        base.Update();
    }

    protected void ChangeBehaviour(MonsterBehaviour monsterBehaviour)
    {
        p_currentBehaviour = monsterBehaviour;

        var currentState = new MonsterBehaviourState(p_monster, p_currentBehaviour);
        Debug.Log($"{p_currentBehaviour.GetType()}");
        ChangeState(currentState);
    }

    public void OnSpawn()
    {
        p_monster.MonsterStateType = MonsterStateType.Spawn;
        ChangeBehaviour(new MonsterBehaviourSpawn());
    }

    public void OnAttack()
    {
        p_monster.MonsterStateType = MonsterStateType.Attack;
        ChangeBehaviour(new MonsterBehaviourAttack());
    }

    public void OnIdle()
    {
        p_monster.MonsterStateType = MonsterStateType.Idle;
        ChangeBehaviour(new MonsterBehaviourIdle());
    }

    public void OnMove()
    {
        p_monster.MonsterStateType = MonsterStateType.Movement;
        ChangeBehaviour(new MonsterBehaviourMovement());
    }

    public void OnDead()
    {
        p_monster.MonsterStateType = MonsterStateType.Dead;
        ChangeBehaviour(new MonsterBehaviourDead());
    }

    public void OnGotHit() 
    {
        p_monster.MonsterStateType = MonsterStateType.GotHit;
        ChangeBehaviour(new MonsterBehaviourGotHit());
    }
}