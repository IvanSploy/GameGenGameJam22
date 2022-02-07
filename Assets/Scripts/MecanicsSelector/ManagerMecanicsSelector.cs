using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerMecanicsSelector : MonoBehaviour
{
    [SerializeField] private GameObject [] _buttonsHabilities = new GameObject[3];
    [SerializeField] private Image [] _habilitiesSelected = new Image[3];
    [SerializeField] private GameObject _goBackSelector;
    [SerializeField] private GameManager _goPlayLevel;

    [SerializeField] private Sprite[] _imagesHabilitiesSelected = new Sprite[10];
    [SerializeField] private Sprite[] _imagesHabilitiesButtons = new Sprite[9];

    //ARRAY QUE CONTIENE LO IMPORTANTE PARA ENVIAR A LA SIGUIENTE ESCENA :D
    private int [] _finalHabilities = new int[3];

    private int round;

    void Start()
    {
        round = 0;
        _goPlayLevel.GetComponent<Button>().interactable = false;
        _goBackSelector.GetComponent<Button>().interactable = false;
        for (int i = 0; i < 3; i++)
        {
            _buttonsHabilities[i].GetComponent<Button>().image.sprite = _imagesHabilitiesButtons[i];
            _habilitiesSelected[i].sprite = _imagesHabilitiesSelected[9];
        }
    }

    public void OnHabilitySelected(int numberButton)
    {
        round++;
        EnableButtons(false);
        //ANIMACION.OnComplete... -> ChangeHabilities ... -> OnComplete EnableButtons
        ChangeHabilities(numberButton);
    }

    private void ChangeHabilities(int numberButton)
    {
        _habilitiesSelected[round - 1].sprite = _imagesHabilitiesSelected[numberButton + ((round - 1) * 3)];
        PersistanceData.instance.habilities[round - 1] = numberButton + ((round - 1) * 3);
        if (round == 3)
        {
            _goPlayLevel.GetComponent<Button>().interactable = true;
            return;
        }
        for (int i = 0; i < 3; i++)
        {
            _buttonsHabilities[i].GetComponent<Button>().image.sprite = _imagesHabilitiesButtons[i + (round * 3)];
        }
        EnableButtons(true);
        _goBackSelector.GetComponent<Button>().interactable = true;
    }

    private void EnableButtons(bool isEnable)
    {
        for (int i = 0; i < 3; i++)
        {
            _buttonsHabilities[i].GetComponent<Button>().interactable = isEnable;
        }
    }

    public void BackSelect()
    {
        _goPlayLevel.GetComponent<Button>().interactable = false;
        EnableButtons(false);
        //ANIMACION.OnComplete -> ChangeHabilites ... -> OnComplete EnableButtoms
        _habilitiesSelected[round - 1].sprite = _imagesHabilitiesSelected[9];
        round--;
        for (int i = 0; i < 3; i++)
        {
            _buttonsHabilities[i].GetComponent<Button>().image.sprite = _imagesHabilitiesButtons[i + (round * 3)];
        }
        EnableButtons(true);
        if (round == 0)
            _goBackSelector.GetComponent<Button>().interactable = false;
    }

    public void GoBackMenu()
    {
        //PONER LA ESCENA DE SELECCION DE NIVELES
        //SceneManager.LoadScene("OtherSceneName");
    }

    public void GoPlayLevel()
    {
        //PONER LA ESCENA DEL NIVEL CORRESPONDIENTE
        //SceneManager.LoadScene("OtherSceneName");
    }
}
