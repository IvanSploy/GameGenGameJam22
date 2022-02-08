using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManagerMecanicsSelector : MonoBehaviour
{
    [SerializeField] private GameObject [] _cardsHabilities = new GameObject[3];
    [SerializeField] private Image [] _habilitiesSelected = new Image[3];
    [SerializeField] private int[] _selected = new int[3];
    [SerializeField] private GameObject _goBackSelector;
    [SerializeField] private GameObject _goPlayLevel;
    [SerializeField] private GameObject[] _buttonsPositionsOutScene = new GameObject[3];
    [SerializeField] private GameObject[] _buttonsPositionsInScene = new GameObject[3];

    [SerializeField] private Sprite[] _imagesHabilitiesSelected = new Sprite[10];
    [SerializeField] private Sprite[] _imagesHabilitiesButtons = new Sprite[9];
    [SerializeField] private List<int> ordenAleatorio;

    //ARRAY QUE CONTIENE LO IMPORTANTE PARA ENVIAR A LA SIGUIENTE ESCENA :D
    private int [] _finalHabilities = new int[3];

    private int round;
    private Vector3 scale;

    void Start()
    {
        scale = _cardsHabilities[0].GetComponent<RectTransform>().localScale;        
        DOTween.Init();
        round = 0;
        _goPlayLevel.GetComponent<Button>().interactable = false;
        _goBackSelector.GetComponent<Button>().interactable = false;
        EnableButtons(false);
        //Suffle -> Orden Aleatorio
        List<int> orden = new List<int>();
        for (int i = 0; i < _imagesHabilitiesButtons.Length; i++)
        {
            orden.Add(i);
        }
        while(orden.Count > 0)
        {
            int index = Random.Range(0, orden.Count);
            ordenAleatorio.Add(orden[index]);
            orden.RemoveAt(index);
        }
        ////
        for (int i = 0; i < 3; i++)
        {
            _cardsHabilities[i].GetComponent<Button>().image.sprite = _imagesHabilitiesButtons[ordenAleatorio[i]];
            _selected[i] = ordenAleatorio[i];
            _cardsHabilities[i].GetComponent<RectTransform>().position =
                _buttonsPositionsOutScene[i].GetComponent<RectTransform>().position;
            _cardsHabilities[i].GetComponent<RectTransform>()
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
        _habilitiesSelected[round - 1].sprite = _imagesHabilitiesSelected[ordenAleatorio[numberButton + ((round - 1) * 3)]];
        PersistanceData.instance.habilities[round - 1] = _selected[numberButton];
        if (round == 3)
        {
            _goPlayLevel.transform.DOMoveX(_goPlayLevel.transform.position.x - 1500, 2f).OnComplete(() => AnimPlayButton(true));
            
            for (int i = 0; i < 3; i++)
            {
                if (i != numberButton)
                {
                    Sequence mySequence = DOTween.Sequence();
                    mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                        .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>EnableBackSelect(true)));
                }
                else
                {
                    int j = i;
                    Sequence mySequence = DOTween.Sequence();
                    mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                        .DOMove(_habilitiesSelected[round - 1].GetComponent<RectTransform>().position, 1));
                    mySequence.Join(_cardsHabilities[i].GetComponent<RectTransform>()
                        .DOScale(new Vector3(0, 0, 1), 1)).OnComplete(() => EnableButtons(true));
                    mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                        .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 0.5f));
                    mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>().DOScale(scale, 0.5f));
                }
            }
            return;
        }
        for (int i = 0; i < 3; i++)
        {
            if (i != numberButton)
            {
                int j = i;
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>AnimationChangeHabilities(j)));
                mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsInScene[i].GetComponent<RectTransform>().position, 2));
            }
            else
            {
                int j = i;
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_habilitiesSelected[round - 1].GetComponent<RectTransform>().position, 1).OnComplete(()=>AnimationChangeHabilities(j)));
                mySequence.Join(_cardsHabilities[i].GetComponent<RectTransform>()
                    .DOScale(new Vector3(0, 0, 1), 1));
                mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 0.5f));
                mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>().DOScale(scale, 0.5f));
                mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsInScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>EnableBackSelect(true)));
            }
        }
    }

    private void AnimPlayButton(bool isEnable)
    {
        _goPlayLevel.GetComponent<Button>().interactable = isEnable;
    }

    private void AnimationChangeHabilities(int i)
    {
        EnableButtons(true);
        _cardsHabilities[i].GetComponent<Button>().image.sprite = _imagesHabilitiesButtons[ordenAleatorio[i + (round * 3)]];
        _selected[i] = ordenAleatorio[i + (round * 3)];
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
            _cardsHabilities[i].GetComponent<Button>().interactable = isEnable;
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
                int j = i;
                Sequence mySequence = DOTween.Sequence();
                mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsOutScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>AnimationChangeHabilities(j)));
                mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsInScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>EnableBackSelect(true)));
            }
        }
        else
        {
            AnimPlayButton(false);
            _goPlayLevel.transform.DOMoveX(_goPlayLevel.transform.position.x + 1500, 1f);
            for (int i = 0; i < 3; i++)
            {
                int j = i;
                Sequence mySequence = DOTween.Sequence();
                AnimationChangeHabilities(j);
                mySequence.Append(_cardsHabilities[i].GetComponent<RectTransform>()
                    .DOMove(_buttonsPositionsInScene[i].GetComponent<RectTransform>().position, 2).OnComplete(()=>EnableBackSelect(true)));
            }
        }
    }

    public void GoBackMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoPlayLevel()
    {
        SceneManager.LoadScene("Level" + PersistanceData.instance.level);
    }
}
