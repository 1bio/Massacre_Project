using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    public GameObject[] prefabs;
    public List<GameObject>[] pools;

    [SerializeField] private float delay;

    private void Awake()
    {
        instance = this;

        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        } 
    }


    // 투사체 오브젝트 풀링
    public GameObject Get(int index)
    {
        GameObject select = null;

        // 풀 오브젝트 존재 O => 오브젝트 활성화
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;

                select.SetActive(true);

                StartCoroutine(Deactivate(select, delay));

                break;
            }
        }

        // 풀 오브젝트 존재 X => 오브젝트 생성
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);

            StartCoroutine(Deactivate(select, delay));

            pools[index].Add(select);
        }

        return select;
    }


    // 오브젝트 비활성화
    IEnumerator Deactivate(GameObject select, float delay) 
    {
        yield return new WaitForSeconds(delay);

        select.SetActive(false);
    }
}
