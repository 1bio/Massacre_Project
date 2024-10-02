using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterTargetDistance
{
    [SerializeField] private float _minTargetDistance;
    [SerializeField] private float _maxTargetDistance;
    [SerializeField] private float _idealTargetDistance;
    [SerializeField] private float _idealTargetDistanceThreshold;

    public float MinTargetDistance => _minTargetDistance;
    public float MaxTargetDistance => _maxTargetDistance;
    public float IdealTargetDistance => _idealTargetDistance;
    public float IdealTargetDistanceThreshold => _idealTargetDistanceThreshold;
}
