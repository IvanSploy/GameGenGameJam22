using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    //Referencias
    public static LevelLoader instance;
    private AsyncOperation scene;

    int level = 0;

    private void Awake()
    {
        if (instance)
            Destroy(this);
        instance = this;
    }

    private void Start()
    {
        PreloadGame();
    }

    public void ChangeScene(int level)
    {
        this.level = level;
        SceneManager.LoadScene(level);
    }
    public void NextLevel()
    {
        level++;
        if (level >= SceneManager.sceneCountInBuildSettings) level = 0;
        SceneManager.LoadScene(level);
    }
    public void PreviousLevel()
    {
        level--;
        if (level < 0) level = SceneManager.sceneCountInBuildSettings - 1;
        SceneManager.LoadScene(level);
    }

    public async void PreloadGame()
    {
        scene = SceneManager.LoadSceneAsync(1);
        scene.allowSceneActivation = false;
        while (scene.progress < 0.9f) {
            await Task.Delay(100);
        }
        
    }
    public void LoadGame()
    {
        level = 1;
        if (scene.progress >= 0.9f)
        {
            scene.allowSceneActivation = true;
        }
        else
        {
            SceneManager.LoadScene(level);
        }
    }
}
