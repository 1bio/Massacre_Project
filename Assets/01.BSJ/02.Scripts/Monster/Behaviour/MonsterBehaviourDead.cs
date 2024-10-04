using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourDead : MonsterBehaviour
{
    private float _currentTime;
    private float _deathDuration = 2f;

    private GameObject _monsterObj;

    public override void OnBehaviourStart(Monster monster)
    {
        monster.SetDeadAnimation();
        _monsterObj = monster.gameObject;
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _deathDuration)
        {
            monster.IsLockedInAnimation = false;
            _monsterObj.SetActive(false);
        }
    }

    public override void OnBehaviourEnd(Monster monster)
    {

    }
}
