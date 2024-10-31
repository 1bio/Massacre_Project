using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class MonsterParticleController
{
    private MonsterSkillData[] _skillDatas;
    private Transform _parentTransform;
    private Monster _monster;

    public MonsterParticleController(MonsterSkillData[] skillDatas, Transform parentTransform, Monster monster)
    {
        _skillDatas = skillDatas;
        VFX = new Dictionary<string, List<ParticleSystem>>();
        InstantiateVFX(parentTransform, monster);
        _parentTransform = parentTransform;
        _monster = monster;
    }

    public Dictionary<string, List<ParticleSystem>> VFX { get; private set; }
    public Dictionary<string, Transform> VFXTransform { get; private set; } = new Dictionary<string, Transform>();
    public ParticleSystem CurrentParticleSystem { get; private set; }

    public void InstantiateVFX(Transform parentTransform, Monster monster)
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
                    MonsterDamageSource damageSource = particleSystem.GetComponentInChildren<MonsterDamageSource>();
                    damageSource?.SetMonster(monster);

                    particleSystem.Stop();
                    particleSystem.Clear();

                    if (!VFX.ContainsKey(vfxPrefab.name))
                    {
                        VFX[vfxPrefab.name] = new List<ParticleSystem>();
                    }
                    VFX[vfxPrefab.name].Add(particleSystem);
                }
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
            ParticleSystem currentParticle = GetAvailableParticle(vfxName);
            
            currentParticle.Stop();
            currentParticle.Clear();
            currentParticle.Play();
        }
    }

    public void RePlayVFX(string vfxName, float scaleFactor)
    {
        int index = vfxName.IndexOf('(');

        if (index != -1)
        {
            vfxName = vfxName.Substring(0, index);
        }

        if (VFX.ContainsKey(vfxName))
        {
            ParticleSystem currentParticle = GetAvailableParticle(vfxName);

            currentParticle.transform.localScale = new Vector3(scaleFactor, scaleFactor, scaleFactor);

            currentParticle.Stop();
            currentParticle.Clear();
            currentParticle.Play();
        }
    }

    public void AllClearVFXs()  
    {
        foreach (KeyValuePair<string, List<ParticleSystem>> vfxs in VFX)
        {
            foreach (ParticleSystem vfx in VFX[vfxs.Key])
            {
                vfx.Stop();
                vfx.Clear();
            }
        }
    }

    public ParticleSystem GetAvailableParticle(string vfxName)
    {
        if (VFX.ContainsKey(vfxName))
        {
            ParticleSystem currentParticle = new ParticleSystem();
            for (int i = 0; i < VFX[vfxName].Count; i++)
            {
                if (Mathf.Approximately(VFX[vfxName][i].time, 0))
                {
                    currentParticle = VFX[vfxName][i];

                    return VFX[vfxName][i];
                }
            }

            if (currentParticle == null)
            {
                GameObject vfxInstance = GameObject.Instantiate(VFX[vfxName][0].gameObject, _parentTransform);
                vfxInstance.transform.position = VFXTransform.ContainsKey(vfxName) ? VFXTransform[vfxName].position : Vector3.zero;
                vfxInstance.transform.rotation = VFXTransform.ContainsKey(vfxName) ? VFXTransform[vfxName].rotation : Quaternion.identity;
                
                ParticleSystem particleSystem = vfxInstance.GetComponent<ParticleSystem>();
                MonsterDamageSource damageSource = particleSystem.GetComponentInChildren<MonsterDamageSource>();
                damageSource?.SetMonster(_monster);

                particleSystem.Stop();
                particleSystem.Clear();

                VFX[vfxName].Add(particleSystem);

                return particleSystem;
            }
        }
        return null;
    }
}
