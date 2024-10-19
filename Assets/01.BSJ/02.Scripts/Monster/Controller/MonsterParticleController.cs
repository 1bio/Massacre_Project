using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterParticleController
{
    private MonsterSkillData[] _skillDatas;
    public MonsterParticleController(MonsterSkillData[] skillDatas, Transform parentTransform)
    {
        _skillDatas = skillDatas;
        VFX = new Dictionary<string, ParticleSystem>();
        InstantiateVFX(parentTransform);
    }

    public Dictionary<string, ParticleSystem> VFX { get; private set; }

    public void InstantiateVFX(Transform parentTransform)
    {
        for (int i = 0; i < _skillDatas.Length; i++)
        {
            for (int j = 0; j < _skillDatas[i].VFX.Length; j++)
            {
                GameObject vfxPrefab = _skillDatas[i].VFX[j];

                if (!parentTransform.Find(vfxPrefab.name))
                {
                    GameObject vfxInstance = GameObject.Instantiate(vfxPrefab, parentTransform);
                    vfxInstance.transform.localPosition = Vector3.zero;

                    ParticleSystem particleSystem = vfxInstance.GetComponent<ParticleSystem>();

                    VFX.Add(vfxPrefab.name, particleSystem);
                }
                else
                {
                    VFX.Add(vfxPrefab.name, vfxPrefab.GetComponent<ParticleSystem>());
                }

                VFX[vfxPrefab.name].Stop();
            }
        }
    }

    public void RePlayVFX(string vfxName)
    {
        int index = vfxName.IndexOf('(');

        if (index != -1)
        {
            vfxName = vfxName.Substring(0, index);
        }

        if (VFX.ContainsKey(vfxName))
        {
            VFX[vfxName].Stop();
            VFX[vfxName].Clear();
            VFX[vfxName].Play();
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
}
