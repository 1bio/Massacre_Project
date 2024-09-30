public interface IMonsterCombat
{
    int Health { get; set; } // ü��
    int MaxHealth { get; }  // �ִ� ü��
    int MoveSpeed { get; }
    int TurnSpeed { get; }

    // ���� ����
    int AttackPower { get; } // ���ݷ�
    float AttackCooldown { get; }   // ���� ��Ÿ��

    float MinTargetDistance { get; }    // �ּ� �Ÿ�
    float MaxTargetDistance { get; }    // �ִ� �Ÿ�
    float IdealTargetDistance { get; }  // ���� �Ÿ�
    float IdealTargetDistanceThreshold { get; } // ���� �Ÿ� ���� ����


    bool IsDead { get; set; }    // ���� ����
    bool IsAttacking { get; set; }   // ���� ������ Ȯ��
    bool IsStaggered { get; set; }   // �¾Ҵ��� Ȯ��
    bool IsMoving { get; set; }  // �����̴��� Ȯ��
}

