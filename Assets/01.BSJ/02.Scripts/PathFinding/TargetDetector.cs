using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    private Monster _monster;
    // Raycast ����
    private RaycastHit _hit;
    [SerializeField] private float _detectionDistance = 10f;
    private float _fanAngle = 50f;
    private float _fanCount = 10f;
    private float _detectionRadius = 0.4f;

    public bool IsTargetDetected { get; set; } = false;

    private void Awake()
    {
        _monster = GetComponent<Monster>();
        IsTargetDetected = false;
    }

    private void FixedUpdate()
    {
        if (!IsTargetDetected)
        {
            if (IsInFanShapeDetection(_detectionDistance))
                IsTargetDetected = true;
        }
        else
        {
            _monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.IsTargetWithinAttackRange =
               IsInFanShapeDetection(_monster.MonsterCombatController.MonsterCombatAbility.MonsterAttack.Range);
        }
    }

    public bool IsInFanShapeDetection(float detectionDistance)
    {
        int layerMask = (1 << LayerMask.NameToLayer(GameLayers.Player.ToString())) |
                        (1 << LayerMask.NameToLayer(GameLayers.Obstacle.ToString()));

        Vector3 startPos = transform.position + new Vector3(0, 0.5f, 0);

        for (int i = 0; i < _fanCount; i++)
        {
            float angle = 0;
            if (i > 0)
            {
                angle = (i % 2 == 0) ? angle - (i * (_fanAngle / (_fanCount - 1))) : angle + (i * (_fanAngle / (_fanCount - 1)));
            }
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            if (Physics.SphereCast(startPos, _detectionRadius, direction, out _hit, detectionDistance, layerMask))
            {
                Debug.DrawRay(startPos, direction * detectionDistance, Color.red, 0.1f);
                if (_hit.collider.gameObject.layer == LayerMask.NameToLayer(GameLayers.Player.ToString()))
                {
                    return true;
                }
                else if (_hit.collider.gameObject.layer == LayerMask.NameToLayer(GameLayers.Obstacle.ToString()))
                {
                    return false;
                }
            }
            else
            {
                Debug.DrawRay(startPos, direction * detectionDistance, Color.yellow, 0.1f);
            }
        }
        return false;
    }
}
