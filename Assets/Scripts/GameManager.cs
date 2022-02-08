using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = UnityEditor.SearchService.Scene;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance) Destroy(this);
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BatteryOff()
    {
        Debug.Log("Finish");
    }

    public void FinishLevel()
    {
        Debug.Log("Nivel completado");
    }

    public void Play()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    //Metodos estaticos
    public static Vector3 CenterVector(Vector3 pos)
    {
        float x = (int)(pos.x) + Mathf.Sign(pos.x) * 0.5f;
        float y = (int)(pos.y) + Mathf.Sign(pos.y) * 0.5f;
        float z = pos.z;
        return new Vector3(x, y, z);
    }
}
