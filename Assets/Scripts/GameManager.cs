using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance && instance!=this)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void BatteryOff()
    {
        Debug.Log("Game Over");
        PopUpBehaviour.instance.TriggerPopUp("Game Over", Color.red, false);
    }

    public void FinishLevel()
    {
        Debug.Log("Nivel completado");
        ChangeLevels.instance.UnlockLevel(PersistanceData.level+1);
        PopUpBehaviour.instance.TriggerPopUp("Victory", Color.yellow, true);
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
