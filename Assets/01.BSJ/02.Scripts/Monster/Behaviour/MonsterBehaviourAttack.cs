using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class MonsterBehaviourAttack : MonsterBehaviour
{

    [SerializeField] private float turnSpeed = 2.75f;

    public override void OnBehaviourStart(Monster monster)
    {

    }

    public override void OnBehaviourUpdate(Monster monster)
    {
        LookAtTarget(monster);
    }   

    public override void OnBehaviourEnd(Monster monster)
    {
        
    }

    private void LookAtTarget(Monster monster)
    {
        //monster.transform.rotation = Quaternion.Lerp(monster.transform.rotation, Quaternion.LookRotation(monster.GetDirectionToTarget(), Vector3.up), turnSpeed * Time.deltaTime);
    }

}
