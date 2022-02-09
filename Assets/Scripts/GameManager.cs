using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

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

    public static void BatteryOff()
    {
        Debug.Log("Game Over");
        FindPopUp("EndPopUp").TriggerPopUp("Game Over", Color.red, false);
    }

    public static void FinishLevel()
    {
        Debug.Log("Nivel completado");
        ChangeLevels.instance.UnlockLevel(PersistanceData.level+1);
        FindPopUp("EndPopUp").TriggerPopUp("Victory", Color.yellow, true);
    }

    public static PopUpBehaviour FindPopUp(string tag)
    {
        PopUpBehaviour[] popUps = FindObjectsOfType<PopUpBehaviour>();
        foreach(PopUpBehaviour p in popUps)
        {
            if (p.tag.Equals(tag)) return p;
        }
        return null;
    }

    public static void Play()
    {
        SceneManager.LoadScene("LevelSelector");
    }

    public static void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

    public static void Back()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public static void Exit()
    {
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
