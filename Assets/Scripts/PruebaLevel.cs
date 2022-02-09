using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PruebaLevel : MonoBehaviour
{
    public TextMeshProUGUI points;
    private int puntos = 0;
    [SerializeField] private GameObject changeScenes;
    
    public void incPoints()
    {
        puntos += 1;
        UpdateText();
        if (puntos == 10)
        {
            changeScenes.GetComponent<ChangeLevels>().UnlockLevel(PersistanceData.level+1);
        }
    }
    private void UpdateText()
    {
        points.text = "Puntos: " + puntos;
    }
}
