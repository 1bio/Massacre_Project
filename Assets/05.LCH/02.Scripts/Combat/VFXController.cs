using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXController : MonoBehaviour
{
    [Header("Start VFX")]
    [SerializeField] private GameObject[] s_prefabs; 

    [Header("Continuous VFX")]
    [SerializeField] private GameObject[] c_prefabs;

    [SerializeField] private float[] castingTime;

    [SerializeField] private Targeting Targeting;

    private void Start()
    {
        s_prefabs[0].GetComponent<ParticleSystem>().Stop();
        s_prefabs[1].GetComponent<ParticleSystem>().Stop();

        c_prefabs[0].GetComponent<ParticleSystem>().Stop();
    }

    public void VFX_SkyFall()
    {
        if (!s_prefabs[0].GetComponent<ParticleSystem>().isPlaying || !s_prefabs[1].GetComponent<ParticleSystem>().isPlaying)
        {
            s_prefabs[0].GetComponent<ParticleSystem>().Stop();
            s_prefabs[1].GetComponent<ParticleSystem>().Stop();
        }

        s_prefabs[0].GetComponent<ParticleSystem>().Play();
        s_prefabs[1].GetComponent<ParticleSystem>().Play();

        if (s_prefabs[1].GetComponent<AudioSource>() != null)
        {
            AudioSource soundComponentCast = s_prefabs[1].GetComponent<AudioSource>();
            AudioClip clip = soundComponentCast.clip;
            soundComponentCast.PlayOneShot(clip);

            StartCoroutine(Attack(castingTime[0]));
        }
    }

    IEnumerator Attack(float castingTime)
    {
        yield return new WaitForSeconds(castingTime);

        c_prefabs[0].transform.parent = null;
        c_prefabs[0].transform.position = Targeting.CurrentTarget.gameObject.transform.position;
        c_prefabs[0].GetComponent<ParticleSystem>().Play();

        if(c_prefabs[0].GetComponent<AudioSource>() != null)
        {
            AudioSource soundComponentCast = c_prefabs[0].GetComponent<AudioSource>();
            AudioClip clip = soundComponentCast.clip;
            soundComponentCast.PlayOneShot(clip);
        }

        yield return new WaitForSeconds(1.5f);
    }

}
