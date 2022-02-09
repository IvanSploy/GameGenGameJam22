using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUnlocker : MonoBehaviour
{
    [SerializeField] private Button[] levels;

    private void Start()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            int j = i;
            levels[i].onClick.AddListener(() => 
            {
                ChangeLevel(j + 1);
            });
        }
        UpdateBlocks();
    }

    public void UpdateBlocks()
    {
        int howMany = ChangeLevels.desLevels;
        for (int i = 0; i < howMany; i++)
        {
            levels[i].interactable = true;
        }
    }

    public void ChangeLevel(int level)
    {
        ChangeLevels.instance.ChangeLevel(level);
    }
}
