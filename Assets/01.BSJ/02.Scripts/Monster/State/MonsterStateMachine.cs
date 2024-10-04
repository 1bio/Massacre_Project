using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MonsterStateMachine : StateMachine
{
    protected Monster _monster;

    [SerializeField] protected MonsterBehaviour _currentBehaviour;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
    }

    private new void Update()
    {
        base.Update();
    }

    protected void ChangeBehaviour(MonsterBehaviour monsterBehaviour)
    {
        _currentBehaviour = monsterBehaviour;

        var currentState = new MonsterBehaviourState(_monster, _currentBehaviour);
        Debug.Log($"{_currentBehaviour.GetType()}");
        ChangeState(currentState);
    }

    protected void OnAttack()
    {
        _monster.MonsterStateType = MonsterStateType.Attack;
        ChangeBehaviour(new MonsterBehaviourAttack());
    }

    protected void OnIdle()
    {
        _monster.MonsterStateType = MonsterStateType.Spawn;
        ChangeBehaviour(new MonsterBehaviourSpawn());
    }

    protected void OnMove()
    {
        _monster.MonsterStateType = MonsterStateType.Movement;
        ChangeBehaviour(new MonsterBehaviourMovement());
    }

    protected void OnDead()
    {
        _monster.MonsterStateType = MonsterStateType.Dead;
        ChangeBehaviour(new MonsterBehaviourDead());
    }

    protected void OnGotHit() 
    {
        _monster.MonsterStateType = MonsterStateType.GotHit;
        ChangeBehaviour(new MonsterBehaviourGotHit());
    }
}