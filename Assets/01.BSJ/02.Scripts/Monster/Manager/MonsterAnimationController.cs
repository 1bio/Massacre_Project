using UnityEngine;

public class MonsterAnimationController
{
    private Animator _animator;
    private string _currentAnimationName;
    private bool _isLockedInAnimation;
    private float _locomotionBlendValue;
    private float _blendTransitionSpeed;

    public MonsterAnimationController(Animator animator, float blendTransitionSpeed)
    {
        _animator = animator;
        _blendTransitionSpeed = blendTransitionSpeed;
        _locomotionBlendValue = 0f;
        _currentAnimationName = string.Empty;
    }

    public bool IsLockedInAnimation { get => _isLockedInAnimation; set => _isLockedInAnimation = value; }
    public float LocomotionBlendValue => _locomotionBlendValue;
    public float BlendTransitionSpeed => _blendTransitionSpeed;

    public void SetLocomotion(float targetBlendValue)
    {
        _locomotionBlendValue = Mathf.Lerp(_locomotionBlendValue, targetBlendValue, _blendTransitionSpeed * Time.deltaTime);

        if (Mathf.Abs(_locomotionBlendValue - targetBlendValue) <= 0.1f)
        {
            _locomotionBlendValue = targetBlendValue;
        }

        _animator.SetFloat("Locomotion", _locomotionBlendValue);
    }

    public void PlayIdleAnimation()
    {
        SetLocomotion(0);
    }

    public void PlayWalkAnimation()
    {
        SetLocomotion(1);
    }

    public void PlayAttackAnimation(int attackCount)
    {
        _isLockedInAnimation = true;
        int randNum = Random.Range(0, attackCount) + 1;
        _currentAnimationName = $"Attack{randNum}";
        _animator.SetTrigger(_currentAnimationName);
    }

    public void PlayGotHitAnimation()
    {
        _isLockedInAnimation = true;
        _currentAnimationName = "GotHit";
        _animator.SetTrigger(_currentAnimationName);
    }

    public void PlayDeathAnimation()
    {
        _isLockedInAnimation = true;
        _currentAnimationName = "Death";
        _animator.SetTrigger(_currentAnimationName);
    }
}
