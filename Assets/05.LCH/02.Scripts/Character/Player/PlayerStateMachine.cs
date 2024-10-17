﻿using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    [field: Header("클래스")]
    [field: SerializeField] public InputReader InputReader { get; private set; }

    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }

    [field: SerializeField] public MeleeComponenet MeleeComponenet { get; private set; }

    [field: SerializeField] public RangeComponent RangeComponent { get; private set; }

    [field: SerializeField] public CoolDownController CoolDownController { get; private set; }

    [field: SerializeField] public CameraShake CameraShake { get; private set; }



    [field: Header("컴포넌트")]
    [field: SerializeField] public Animator Animator { get; private set; }

    [field: SerializeField] public CharacterController Controller { get; private set; }

    [field: SerializeField] public Transform CameraTransform { get; private set; }

    [field: SerializeField] public GameObject[] WeaponPrefabs { get; private set; } // 임시


    [field: Header("플레이어 세팅")]
    [field: SerializeField] public float RotationSpeed { get; private set; }

    [field: SerializeField] public float MeleeWeaponDetectionRange { get; private set; } = 5f;

    [field: SerializeField] public float RangeWeaponDetectionRange { get; private set; } = 15f;


    [field: Header("능력치")]
    [field: SerializeField] public float CurrentHealth { get; private set; }

    [field: SerializeField] public float MoveSpeed { get; private set; }


    // 초기화
    private void Start()
    {
        Initialized();

        DataManager.instance.SaveData();

        ChangeState(new PlayerFreeLookState(this));
    }

    private void Initialized()
    {
        MoveSpeed = DataManager.instance.playerData.statusData.moveSpeed; 
        CurrentHealth = DataManager.instance.playerData.statusData.maxHealth;

        CoolDownController.AddSkill("정조준", CoolDownController.ReturnCoolDown("정조준"));
        CoolDownController.AddSkill("트리플샷", CoolDownController.ReturnCoolDown("트리플샷"));
        CoolDownController.AddSkill("화살비", CoolDownController.ReturnCoolDown("화살비"));

        CoolDownController.AddSkill("도약베기", CoolDownController.ReturnCoolDown("도약베기"));
        CoolDownController.AddSkill("미정", CoolDownController.ReturnCoolDown("미정"));
        CoolDownController.AddSkill("회전베기", CoolDownController.ReturnCoolDown("회전베기"));
    }
}