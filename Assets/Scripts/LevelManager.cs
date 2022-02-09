using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private int keys;

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(instance);
        }
        instance = this;
    }

    void Start()
    {
        keys = FindObjectsOfType<Keys>().Length;
    }

    public void DecKeys()
    {
        keys--;
    }
    public int GetKeys()
    {
        return keys;
    }
}
