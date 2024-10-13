using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParticleController
{
    private MonsterSkillData _skillData;
    public MonsterParticleController(MonsterSkillData skillData, Transform parentTransform)
    {
        _skillData = skillData;
        VFX = new Dictionary<string, ParticleSystem>();
        InstantiateVFX(parentTransform);
    }

    public Dictionary<string, ParticleSystem> VFX { get; private set; }

    public void InstantiateVFX(Transform parentTransform)
    {
        if (_skillData != null && _skillData.VFX.Length > 0)
        {
            foreach (GameObject vfxPrefab in _skillData.VFX)
            {
                GameObject vfxInstance = GameObject.Instantiate(vfxPrefab, parentTransform);
                vfxInstance.transform.localPosition = Vector3.zero;

                ParticleSystem particleSystem = vfxInstance.GetComponent<ParticleSystem>();
                if (particleSystem != null)
                {
                    VFX.Add(vfxPrefab.name, particleSystem);
                    particleSystem.Stop();
                }
            }
        }
    }

    public void RePlayVFX(string vfxName, float scaleFactor)
    {
        if (VFX.ContainsKey(vfxName))
        {
            VFX[vfxName].transform.localScale = Vector3.one;
            VFX[vfxName].transform.localScale *= scaleFactor;
            VFX[vfxName].Stop();
            VFX[vfxName].Clear();
            VFX[vfxName].Play();
        }
    }

    public void StopVFX(string vfxName)
    {
        if (VFX.ContainsKey(vfxName))
        {
            VFX[vfxName].Stop();
        }
    }

    public bool IsVFXProgressAt(string vfxName, float percentage)
    {
        if (VFX.ContainsKey(vfxName))
        {
            ParticleSystem vfx = VFX[vfxName];

            if (vfx.isPlaying)
            {
                float currentTime = vfx.time;
                float duration = vfx.main.duration;

                float targetTime = duration * (percentage / 100f);

                return currentTime >= targetTime;
            }
            else
            {
                vfx.Play();
            }
        }

        return false;
    }
}
