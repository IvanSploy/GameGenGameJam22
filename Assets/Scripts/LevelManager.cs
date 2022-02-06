using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private int keys;

    void Start()
    {
        keys = FindObjectsOfType<Keys>().Length;
        Debug.Log(keys);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void decKeys()
    {
        keys--;
    }
    public int getKeys()
    {
        return keys;
    }
}
