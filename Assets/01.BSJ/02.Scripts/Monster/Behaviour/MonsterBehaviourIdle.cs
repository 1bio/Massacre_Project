using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourIdle : MonsterBehaviour
{
    public override void OnBehaviourStart(Monster monster)
    {
        monster.SetIdleAnimation();
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        monster.LookAtTarget(monster);
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        
    }
}
