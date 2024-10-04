using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MonsterBehaviourSpawn : MonsterBehaviour
{
    private Animator _animator;
    private float _currentTime;
    private float _spawnDuration = 1f;

    public override void OnBehaviourStart(Monster monster)
    {
        _animator = monster.Animator;

        monster.IsLockedInAnimation = true;
        _currentTime = 0f;

        _animator.SetFloat("Locomotion", 0);
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _spawnDuration)
            monster.IsLockedInAnimation = false;
    }

    public override void OnBehaviourEnd(Monster monster)
    {

    }
}
