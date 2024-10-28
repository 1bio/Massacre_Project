using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class TransparencyController : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private int _layerMask = 0;
    private List<Collider> _raycastHitColliders = new List<Collider>();

    private bool _hasHit = false;

    private void Start()
    {
        _layerMask = (1 << LayerMask.NameToLayer(GameLayers.Obstacle.ToString()));
    }

    private void FixedUpdate()
    {
        Vector3 direction = (_playerTransform.position - transform.position).normalized;
        float maxDistance = Vector3.Distance(transform.position, _playerTransform.position);
        _hasHit = Physics.Raycast(transform.position, direction, out RaycastHit hitinfo, maxDistance, _layerMask);

        if (_hasHit)
        {
            foreach (RaycastHit hit in Physics.RaycastAll(transform.position, direction, maxDistance, _layerMask))
            {
                if (hit.collider != null && !_raycastHitColliders.Contains(hit.collider) && hit.collider is MeshCollider)
                {
                    _raycastHitColliders.Add(hit.collider);
                }
            }
        }
    }

    private void LateUpdate()
    {
        Material[] materials = new Material[_raycastHitColliders.Count];
        Color[] colors = new Color[_raycastHitColliders.Count];

        if (_raycastHitColliders?.Count > 0)
        {
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i] = _raycastHitColliders[i].gameObject.GetComponent<Renderer>().material;
            }
        }

        if (_hasHit)
        {
            StartFadeOut(materials, colors);
        }
        else
        {
            StartFadeIn(materials, colors);
            _raycastHitColliders.Clear();
        }
    }

    public void StartFadeIn(Material[] materials, Color[] colors)
    {
        FadeInOut(materials, colors, 0.5f, 1);
    }

    public void StartFadeOut(Material[] materials, Color[] colors)
    {
        FadeInOut(materials, colors, 1, 0.5f);
    }

    private void FadeInOut(Material[] materials, Color[] colors, float startAlpha, float endAlpha)
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = materials[i].color;
            colors[i].a = startAlpha;
            materials[i].color = colors[i];
        }

        for (int i = 0; i < materials.Length; i++)
        {
            colors[i].a = endAlpha;
            materials[i].color = colors[i];
        }
    }
}
