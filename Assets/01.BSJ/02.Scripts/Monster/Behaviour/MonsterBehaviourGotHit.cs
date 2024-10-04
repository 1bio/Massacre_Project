using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourGotHit : MonsterBehaviour
{
    public override void OnBehaviourStart(Monster monster)
    {
        monster.SetGotHitAnimation();
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        if (monster.IsLockedInAnimation)
        {
            monster.UnLockAnimation(monster.CurrentAniamtionName);
        }
    }

    public override void OnBehaviourEnd(Monster monster)
    {

    }
}
