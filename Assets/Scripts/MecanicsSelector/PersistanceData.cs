using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistanceData : MonoBehaviour
{
    private static PersistanceData instance;
    public static int[] habilities = new int [3];
    public static int level;
    void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
