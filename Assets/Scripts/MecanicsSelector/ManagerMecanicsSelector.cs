using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerMecanicsSelector : MonoBehaviour
{
    [SerializeField] private GameObject [] _buttonsHabilities = new GameObject[3];
    [SerializeField] private Image [] _habilitiesSelected = new Image[3];
    [SerializeField] private GameObject _goBackSelector;
    [SerializeField] private GameObject _goPlayLevel;
    [SerializeField] private GameObject[] _buttonsPositionsOutScene = new GameObject[3];
    [SerializeField] private GameObject[] _buttonsPositionsInScene = new GameObject[3];

    [SerializeField] private Sprite[] _imagesHabilitiesSelected = new Sprite[10];
    [SerializeField] private Sprite[] _imagesHabilitiesButtons = new Sprite[9];

    //ARRAY QUE CONTIENE LO IMPORTANTE PARA ENVIAR A LA SIGUIENTE ESCENA :D
    private int [] _finalHabilities = new int[3];

    private int round;
    private Vector3 scale;

    void Start()
    {
        scale = _buttonsHabilities[0].GetComponent<RectTransform>().localScale;        
        DOTween.Init();
        round = 0;
        _goPlayLevel.GetComponent<Button>().interactable = false;
        _goBackSelector.GetComponent<Button>().interactable = false;
        EnableButtons(false);
        for (int i = 0; i < 3; i++)
        {
            _buttonsHabilities[i].GetComponent<Button>().image.sprite = _imagesHabilitiesButtons[i];
            _buttonsHabilities[i].GetComponent<RectTransform>().position =
                _buttonsPositionsOutScene[i].GetComponent<RectTransform>().position;
            _buttonsHabilities[i].GetComponent<RectTransform>()
                .DOMove(_buttonsPositionsInScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>EnableButtons(true));
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
        EnableBackSelect(false);
        EnableButtons(false);
        _habilitiesSelected[round - 1].sprite = _imagesHabilitiesSelected[numberButton + ((round - 1) * 3)];
        PersistanceData.instance.habilities[round - 1] = numberButton + ((round - 1) * 3);
        if (round == 3)
        {
            _goPlayLevel.GetComponent<Button>().interactable = true;
            for (int i = 0; i < 3; i++)
            {
                if (i != numberButton)
                {
                    Sequence mySequence = DOTween.Sequence();
                    mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                        .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>EnableBackSelect(true)));
                }
                else
                {
                    Sequence mySequence = DOTween.Sequence();
                    mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                        .DOMove(_habilitiesSelected[round - 1].GetComponent<RectTransform>().position, 1));
                    mySequence.Join(_buttonsHabilities[i].GetComponent<RectTransform>()
                        .DOScale(new Vector3(0, 0, 1), 1)).OnComplete(()=>AnimationChangeHabilities(i));
                    mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                        .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 0.5f));
                    mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>().DOScale(scale, 0.5f));
                }
            }
            return;
        }
        for (int i = 0; i < 3; i++)
        {
            if (i != numberButton)
            {
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>AnimationChangeHabilities(i)));
                mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsInScene[i].GetComponent<RectTransform>().position, 2));
            }
            else
            {
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_habilitiesSelected[round - 1].GetComponent<RectTransform>().position, 1).OnComplete(()=>AnimationChangeHabilities(i)));
                mySequence.Join(_buttonsHabilities[i].GetComponent<RectTransform>()
                    .DOScale(new Vector3(0, 0, 1), 1)).OnComplete(()=>AnimationChangeHabilities(i));
                mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 0.5f));
                mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>().DOScale(scale, 0.5f));
                mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsInScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>EnableBackSelect(true)));
            }
        }
    }

    private void AnimationChangeHabilities(int i)
    {
        EnableButtons(true);
        _buttonsHabilities[i].GetComponent<Button>().image.sprite = _imagesHabilitiesButtons[i + (round * 3)];
    }

    private void EnableBackSelect(bool isEnable)
    {
        if (round == 0)
        {
            _goBackSelector.GetComponent<Button>().interactable = false;
            return;
        }
        _goBackSelector.GetComponent<Button>().interactable = isEnable;
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
        EnableBackSelect(false);
        //ANIMACION.OnComplete -> ChangeHabilites ... -> OnComplete EnableButtoms
        _habilitiesSelected[round - 1].sprite = _imagesHabilitiesSelected[9];
        round--;
        if (round == 0)
            _goBackSelector.GetComponent<Button>().interactable = false;
        if (round != 2)
        {
            for (int i = 0; i < 3; i++)
            {
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>AnimationChangeHabilities(i)));
                mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsInScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>EnableBackSelect(true)));
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                Sequence mySequence = DOTween.Sequence();
                AnimationChangeHabilities(i);
                mySequence.Append(_buttonsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsInScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>EnableBackSelect(true)));
            }
        }
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
