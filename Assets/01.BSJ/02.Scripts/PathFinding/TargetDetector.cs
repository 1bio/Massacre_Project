using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : MonoBehaviour
{
    // Raycast ����
    private RaycastHit _hit;
    [SerializeField] private float _detectionDistance = 10f;
    private float _fanAngle = 48f;
    [SerializeField] private float _fanCount = 5f;
    private float _detectionRadius = 0.4f;

    public bool IsTargetDetected { get; set; } = false;

    private void FixedUpdate()
    {
        if (!IsTargetDetected)
        {
            if (IsInFanShapeDetection())
                IsTargetDetected = true;
        }
    }

    public bool IsInFanShapeDetection()
    {
        int layerMask = (1 << LayerMask.NameToLayer(GameLayers.Player.ToString()));

        for (int i = 0; i < _fanCount; i++)
        {
            float angle = -_fanAngle / 2 + (i * (_fanAngle / (_fanCount - 1)));
            Vector3 direction = Quaternion.Euler(0, angle, 0) * transform.forward;

            if (Physics.SphereCast(transform.position, _detectionRadius, direction, out _hit, _detectionDistance, layerMask))
            {
                Debug.DrawRay(transform.position, direction * _detectionDistance, Color.red, 0.1f);
                if (_hit.collider.gameObject.layer == LayerMask.NameToLayer(GameLayers.Player.ToString()))
                {
                    return true;
                }
            }
            else
            {
                Debug.DrawRay(transform.position, direction * _detectionDistance, Color.yellow, 0.1f);
            }
        }
        return false;
    }
}
