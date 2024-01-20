using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


[CreateAssetMenu(menuName = "Prefabs set", fileName = "prefabsSet_", order = 0)]
public class PrefabsSetSO : ScriptableObject
{
    public GameObject[] m_prefabOptions;

    public GameObject GetRandomPrefab()
    {
        int random = Random.Range(0, m_prefabOptions.Length);
        return m_prefabOptions[random];
    }
}
