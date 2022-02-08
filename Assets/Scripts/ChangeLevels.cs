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
    public int nowLevel; //Cambiar por cada nivel
    [SerializeField] private Button[] levels;
    private String levelsPrefsName = "Levels";

    private void Awake()
    {
        if (instance) Destroy(this);
        instance = this;
        Load();
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "LevelSelector")
        {
            UpdateBlocks();
        }
    }
    //Call this method when finish condition is true
    public void ChangeLevel(int level)
    {

        if (level == 0)
            SceneManager.LoadScene("LevelSelector");
        else 
            SceneManager.LoadScene("PruebaNivel" + level);
    }

    public void UnlockLevel()
    {
        if (desLevels < nowLevel)
        {
            desLevels = nowLevel;
            nowLevel++;
        }
        Save();
        BackToMenu();
    }

    private void UpdateBlocks()
    {
        for (int i = 0; i < desLevels + 1; i++)
        {
            levels[i].interactable = true;
        }
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    private void BackToMenu()
    {
        ChangeLevel(0);
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
