using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourGotHit : MonsterBehaviour
{
    private Monster _monster;

    public override void OnBehaviourStart(Monster monster)
    {
        _monster = monster;
        _monster.MonsterCombatController.Health.ImpactEvent += OnImpact;

        monster.AnimationController.PlayGotHitAnimation();
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        _monster.MonsterCombatController.Health.ImpactEvent -= OnImpact;
    }

    private void OnImpact()
    {
        //체력 감소
    }
}
