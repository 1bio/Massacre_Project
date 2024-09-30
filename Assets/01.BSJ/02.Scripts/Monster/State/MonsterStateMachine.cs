using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class MonsterStateMachine : StateMachine
{
    private Monster monster;

    private void Start()
    {
        monster = GetComponent<Monster>();

        var currentBehaviour = new MonsterBehaviourMovement();
        ChangeState(ChangeBehaviour(currentBehaviour));
    }

    private new void Update()
    {
        base.Update();
    }

    protected State ChangeBehaviour(MonsterBehaviour monsterBehaviour)
    {
        var currentBehaviour = monsterBehaviour;
        var currentState = new MonsterBehaviourState(monster, currentBehaviour);
        return currentState;
    }
}