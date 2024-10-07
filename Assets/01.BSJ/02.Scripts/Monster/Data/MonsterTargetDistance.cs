using UnityEngine;

[System.Serializable]
public class MonsterTargetDistance
{
    [SerializeField] private float _minTargetDistance;
    [SerializeField] private float _maxTargetDistance;
    [SerializeField] private float _idealTargetDistance;
    [SerializeField] private float _idealTargetDistanceThreshold;

    public MonsterTargetDistance(float minTargetDistance, float maxTargetDistance, float idealTargetDistance, float idealTargetDistanceThreshold)
    {
        _minTargetDistance = minTargetDistance;
        _maxTargetDistance = maxTargetDistance;
        _idealTargetDistance = idealTargetDistance;
        _idealTargetDistanceThreshold = idealTargetDistanceThreshold;
    }

    public float MinTargetDistance { get => _minTargetDistance; set => _minTargetDistance = value; }
    public float MaxTargetDistance { get => _maxTargetDistance; set => _maxTargetDistance = value; }
    public float IdealTargetDistance { get => _idealTargetDistance; set => _idealTargetDistance = value; }
    public float IdealTargetDistanceThreshold { get => _idealTargetDistanceThreshold; set => _idealTargetDistanceThreshold = value; }
}
