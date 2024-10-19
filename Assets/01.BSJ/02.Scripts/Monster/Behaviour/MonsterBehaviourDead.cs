using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehaviourDead : MonsterBehaviour
{
    private float _currentTime;
    private float _deathDuration = 2f;

    private GameObject _monsterObj;

    private int itemMaxCount = 3;

    public override void OnBehaviourStart(Monster monster)
    {
        monster.AnimationController.PlayDeathAnimation();
        _monsterObj = monster.gameObject;

        monster.MonsterLootItemController.LootItems(monster.transform.position, itemMaxCount);

       
    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        _currentTime += Time.deltaTime;

        if (_currentTime > _deathDuration)
        {
            monster.AnimationController.IsLockedInAnimation = false;
            _monsterObj.SetActive(false);
        }
    }

    public override void OnBehaviourEnd(Monster monster)
    {
        
    }
}