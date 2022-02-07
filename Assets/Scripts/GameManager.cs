using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BatteryOff()
    {
        Debug.Log("Finish");
    }

    public void FinishLevel()
    {
        Debug.Log("Nivel completado");
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
