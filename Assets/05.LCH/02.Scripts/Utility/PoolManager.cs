using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    public List<GameObject>[] pools;


    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int i = 0; i < pools.Length; i++)
        {
            pools[i] = new List<GameObject>();
        } 
    }


    // ����ü ������Ʈ Ǯ��
    public GameObject Get(int index)
    {
        GameObject select = null;

        // Ǯ ������Ʈ ���� O => ������Ʈ Ȱ��ȭ
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;

                select.SetActive(true);

                StartCoroutine(Deactivate(select, 1f));

                break;
            }
        }

        // Ǯ ������Ʈ ���� X => ������Ʈ ����
        if (!select)
        {
            select = Instantiate(prefabs[index], transform);

            StartCoroutine(Deactivate(select, 1f));

            pools[index].Add(select);
        }

        return select;
    }


    // ������Ʈ ��Ȱ��ȭ
    IEnumerator Deactivate(GameObject select, float delay) 
    {
        yield return new WaitForSeconds(delay);
        select.SetActive(false);
    }
}
