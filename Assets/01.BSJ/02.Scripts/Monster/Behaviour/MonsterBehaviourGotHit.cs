using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourGotHit : MonsterBehaviour
{
    public override void OnBehaviourStart(Monster monster)
    {
        monster.SetGotHitAnimation();
        monster.MonsterAbility.MonsterHealth.IsHit = false;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        
    }

    public override void OnBehaviourEnd(Monster monster)
    {

    }
}
