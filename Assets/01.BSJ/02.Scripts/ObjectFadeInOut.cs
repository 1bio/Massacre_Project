using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFadeInOut : MonoBehaviour
{
    public Material _blackMaterial;
    private Renderer[] _renderers;
    private Material[] _originalMaterials;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        _originalMaterials = new Material[_renderers.Length];

        /*for (int i = 0; i < _renderers.Length; i++)
        {
            _originalMaterials[i] = _renderers[i].material;
            _renderers[i].material = _blackMaterial;
        }*/
    }

    public void StartFadeIn(float duration)
    {
        StartCoroutine(FadeInOut(duration, 0, 1));
    }

    public void StartFadeOut(float duration)
    {
        StartCoroutine(FadeInOut(duration, 1, 0));
    }

    private IEnumerator FadeInOut(float duration, float startAlpha, float endAlpha)
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material = _blackMaterial;

            Color color = _renderers[i].material.color;
            color.a = startAlpha;

            for (float t = 0; t <= duration; t += Time.deltaTime)
            {
                color.a = Mathf.Lerp(startAlpha, endAlpha, t / duration);
                _renderers[i].material.color = color;
                yield return null;
            }

            color.a = endAlpha;
            _renderers[i].material.color = color;

            _renderers[i].material = _originalMaterials[i];
        }
    }
}
