using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public GameObject[] prefabs;

    List<GameObject>[] pools;

    void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];
        for (int i = 0; i < pools.Length; i++)
            pools[i] = new List<GameObject>();
    }

    public GameObject Get(int i)
    {
        GameObject select = null;

        // 풀의 노는 오브젝트 접근
        foreach (GameObject item in pools[i])
        {
            if (!item.activeSelf)
            {   // 발견하면 select에 할당
                select = item;
                select.SetActive(true);
                break;
            }
        }

        if (!select)
        {   // 못찾으면 새로 생성하고 select 변수에 할당
            select = Instantiate(prefabs[i], transform);
            pools[i].Add(select);
        }

        return select;
    }
}
