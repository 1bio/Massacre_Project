using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MonsterStateMachine : StateMachine
{
    protected Monster _monster;

    [SerializeField] protected MonsterBehaviour _currentBehaviour;

    protected MonsterBehaviourSpawn _spawnBehaviour;
    protected MonsterBehaviourAttack _attackBehaviour;
    protected MonsterBehaviourMovement _moveBehaviour;
    protected MonsterBehaviourDead _deadBehaviour;

    private void Awake()
    {
        _monster = GetComponent<Monster>();

        _spawnBehaviour = new MonsterBehaviourSpawn();
        _attackBehaviour = new MonsterBehaviourAttack();
        _moveBehaviour = new MonsterBehaviourMovement();
        _deadBehaviour = new MonsterBehaviourDead();
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
        ChangeBehaviour(_attackBehaviour);
    }

    protected void OnIdle()
    {
        _monster.MonsterStateType = MonsterStateType.Spawn;
        ChangeBehaviour(_spawnBehaviour);
    }

    protected void OnMove()
    {
        _monster.MonsterStateType = MonsterStateType.Movement;
        ChangeBehaviour(_moveBehaviour);
    }

    protected void OnDead()
    {
        _monster.MonsterStateType = MonsterStateType.Dead;
        ChangeBehaviour(_deadBehaviour);
    }
}