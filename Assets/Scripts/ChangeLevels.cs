using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeLevels : MonoBehaviour
{
    public static ChangeLevels instance;

    static public int desLevels;
    private string levelsPrefsName = "Levels";

    private void Awake()
    {
        if (instance) Destroy(this);
        instance = this;
        Load();
    }

    //Call this method when finish condition is true
    public void ChangeLevel(int level)
    {
        PersistanceData.level = level;
        SceneTransitioner.instance.OnTransition.AddListener(() => SceneManager.LoadScene("MecanicsSelector"));
        SceneTransitioner.instance.StartTransition($"Level {level}", "Go to recharge.", 1);
    }

    public void UnlockLevel(int level)
    {
        if (desLevels < level)
        {
            desLevels = level;
        }
        Save();
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void BackToMenu()
    {
        ChangeLevel(0);
    }

    public void BackToMecanics()
    {
        SceneManager.LoadScene("MecanicsSelector");
    }

    public void Save()
    {
        PlayerPrefs.SetInt(levelsPrefsName, desLevels);
    }

    public void Load()
    {
        desLevels = PlayerPrefs.GetInt(levelsPrefsName, 1);
    }
}
