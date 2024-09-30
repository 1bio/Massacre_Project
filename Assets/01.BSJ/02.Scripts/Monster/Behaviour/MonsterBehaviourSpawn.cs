using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static Monster;

public class MonsterBehaviourSpawn : MonsterBehaviour
{
    public override void OnBehaviourStart(Monster monster)
    {
        monster.SetMovementControl(MonsterMovementControlType.AnimationDriven);
    }

    public override void OnBehaviourUpdate(Monster monster)
    {

    }

    public override void OnBehaviourEnd(Monster monster)
    {

    }
}
