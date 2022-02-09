using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject manageLevel;
    [SerializeField] private GameObject credits;

    [SerializeField] private GameObject mainMenuPlay;
    [SerializeField] private GameObject mainMenuExit;
    [SerializeField] private GameObject mainMenuCredits;
    [SerializeField] private GameObject mainMenuText;

    [SerializeField] private GameObject manageLevelExit;
    [SerializeField] private GameObject manageLevelLevels;
    [SerializeField] private GameObject manageLevelText;

    [SerializeField] private GameObject creditsExit;
    [SerializeField] private GameObject creditsText;
    [SerializeField] private GameObject creditsNames;
    
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
        mainMenu.SetActive(true);
        manageLevel.SetActive(true);
        credits.SetActive(true);

        EnableButtonsCredits(false);
        EnableButtonsLevels(false);
        
        manageLevelExit.transform.position = new Vector3(manageLevelExit.transform.position.x - 500, manageLevelExit.transform.position.y, manageLevelExit.transform.position.z);
        manageLevelLevels.transform.position = new Vector3(manageLevelLevels.transform.position.x, manageLevelLevels.transform.position.y + 800, manageLevelLevels.transform.position.z);
        manageLevelText.transform.position = new Vector3(manageLevelText.transform.position.x, manageLevelText.transform.position.y + 800, manageLevelText.transform.position.z);

        creditsExit.transform.position = new Vector3(creditsExit.transform.position.x - 500, creditsExit.transform.position.y, creditsExit.transform.position.z);
        creditsText.transform.position = new Vector3(creditsText.transform.position.x, creditsText.transform.position.y + 800, creditsText.transform.position.z);
        creditsNames.transform.position = new Vector3(creditsNames.transform.position.x, creditsNames.transform.position.y + 800, creditsNames.transform.position.z);
    }

    public void MainToCredits()
    {
        EnableButtonsMenu(false);
        
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(mainMenuExit.transform.DOMoveX(mainMenuExit.transform.position.x - 600, 0.5f))
            .Join(mainMenuCredits.transform.DOMoveX(mainMenuCredits.transform.position.x + 600, 0.5f))
            .Join(mainMenuPlay.transform.DOMoveY(mainMenuPlay.transform.position.y + 1000, 0.5f))
            .Join(mainMenuText.transform.DOMoveY(mainMenuText.transform.position.y + 1000, 0.5f))
            .Append(creditsExit.transform.DOMoveX(creditsExit.transform.position.x + 600, 0.5f))
            .Join(creditsText.transform.DOMoveY(creditsText.transform.position.y - 900, 0.5f))
            .Join(creditsNames.transform.DOMoveY(creditsNames.transform.position.y - 900, 0.5f))
            .OnComplete(() => EnableButtonsCredits(true));
    }

    public void CreditsToMenu()
    {
        EnableButtonsCredits(false);
        
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(creditsExit.transform.DOMoveX(creditsExit.transform.position.x - 600, 0.5f))
            .Join(creditsText.transform.DOMoveY(creditsText.transform.position.y + 900, 0.5f))
            .Join(creditsNames.transform.DOMoveY(creditsNames.transform.position.y + 900, 0.5f))
            .Append(mainMenuExit.transform.DOMoveX(mainMenuExit.transform.position.x + 600, 0.5f))
            .Join(mainMenuCredits.transform.DOMoveX(mainMenuCredits.transform.position.x - 600, 0.5f))
            .Join(mainMenuPlay.transform.DOMoveY(mainMenuPlay.transform.position.y - 1000, 0.5f))
            .Join(mainMenuText.transform.DOMoveY(mainMenuText.transform.position.y - 1000, 0.5f))
            .OnComplete(() => EnableButtonsMenu(true));
    }

    public void MainToPlay()
    {
        EnableButtonsMenu(false);
        
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(mainMenuExit.transform.DOMoveX(mainMenuExit.transform.position.x - 600, 0.5f))
            .Join(mainMenuCredits.transform.DOMoveX(mainMenuCredits.transform.position.x + 600, 0.5f))
            .Join(mainMenuPlay.transform.DOMoveY(mainMenuPlay.transform.position.y + 1000, 0.5f))
            .Join(mainMenuText.transform.DOMoveY( mainMenuText.transform.position.y + 1000, 0.5f))
            .Append(manageLevelExit.transform.DOMoveX( manageLevelExit.transform.position.x + 500, 0.5f))
            .Join(manageLevelLevels.transform.DOMoveY(manageLevelLevels.transform.position.y - 800, 0.5f))
            .Join(manageLevelText.transform.DOMoveY(manageLevelText.transform.position.y - 800, 0.5f))
            .OnComplete(() => EnableButtonsLevels(true));
    }

    public void PlayToMain()
    {
        EnableButtonsLevels(false);
        
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(manageLevelExit.transform.DOMoveX(manageLevelExit.transform.position.x - 500, 0.5f))
            .Join(manageLevelLevels.transform.DOMoveY( manageLevelLevels.transform.position.y + 800, 0.5f))
            .Join(manageLevelText.transform.DOMoveY(manageLevelText.transform.position.y + 800, 0.5f))
            .Append(mainMenuExit.transform.DOMoveX(mainMenuExit.transform.position.x + 600, 0.5f))
            .Join(mainMenuCredits.transform.DOMoveX(mainMenuCredits.transform.position.x - 600, 0.5f))
            .Join(mainMenuPlay.transform.DOMoveY( mainMenuPlay.transform.position.y - 1000, 0.5f))
            .Join(mainMenuText.transform.DOMoveY(mainMenuText.transform.position.y - 1000, 0.5f))
            .OnComplete(() => EnableButtonsMenu(true));
    }

    public void quit()
    {
        Application.Quit();
    }
    private void EnableButtonsMenu(bool isEnable)
    {
        mainMenuCredits.GetComponent<Button>().interactable = isEnable;
        mainMenuExit.GetComponent<Button>().interactable = isEnable;
        mainMenuPlay.GetComponent<Button>().interactable = isEnable;
    }
    
    private void EnableButtonsCredits(bool isEnable)
    {
        creditsExit.GetComponent<Button>().interactable = isEnable;
    }
    
    private void EnableButtonsLevels(bool isEnable)
    {
        manageLevelExit.GetComponent<Button>().interactable = isEnable;
    }
}
