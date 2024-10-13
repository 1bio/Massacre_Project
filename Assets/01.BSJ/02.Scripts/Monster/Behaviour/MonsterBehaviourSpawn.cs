using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MonsterBehaviourSpawn : MonsterBehaviour
{
    private float _currentTime;
    private float _spawnDuration = 2f;

    public override void OnBehaviourStart(Monster monster)
    {
        monster.AnimationController.IsLockedInAnimation = true;
        _currentTime = 0f;

        //monster.AnimationController.ObjectFadeInOut.StartFadeOut(0);
        monster.AnimationController.PlayIdleAnimation();
        //monster.AnimationController.ObjectFadeInOut.StartFadeIn(_spawnDuration);
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _spawnDuration * 1.5f)
            monster.AnimationController.IsLockedInAnimation = false;
    }

    public override void OnBehaviourEnd(Monster monster)
    {

    }
}
