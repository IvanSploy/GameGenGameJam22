using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceData : MonoBehaviour
{
    public static PersistanceData instance;
    public int[] habilities = new int [3];
    public int level;
    void Awake()
    {
        if(instance)
            Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
