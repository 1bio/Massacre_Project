using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minotaur : Monster
{
    public enum RamAttackAnimationName
    {
        RamStart,
        RamRun,
        RamAttack,
        RamWall
    }

    // Animation Event
    public void RePlayVFX(string vfxNameWithScale)
    {
        string[] parts = vfxNameWithScale.Split('_');
        string vfxName = parts[0];
        float scaleFactor = float.Parse(parts[1]);

        MonsterParticleController.RePlayVFX(vfxName, scaleFactor);
    }
}
