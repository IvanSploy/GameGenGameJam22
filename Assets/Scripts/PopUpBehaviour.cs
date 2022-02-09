using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopUpBehaviour : MonoBehaviour
{
    //Referencias
    public static PopUpBehaviour instance;
    private Button[] buttons;
    public Button nextLevelButton;
    public TMP_Text info;
    private Animator anim;
    public bool pauseOnTrigger = false;
    [HideInInspector] public bool paused = false;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        instance = this;
        buttons = FindObjectsOfType<Button>();
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerPopUp(string text, Color color, bool passed)
    {
        info.SetText(text);
        info.color = color;
        TriggerPopUp(passed);
    }

    [ContextMenu("Trigger")]
    public void TriggerPopUp(bool passed)
    {
        if (pauseOnTrigger)
        {
            if (paused)
            {
                ActiveButtons(false);
                Time.timeScale = 1;
                paused = false;
            }
            else
            {
                ActiveButtons(true);
                Time.timeScale = 0;
                paused = true;
            }
        }
        nextLevelButton.interactable = passed;
        anim.SetTrigger("ChangeState");
    }

    public void TriggerPopUp()
    {
        TriggerPopUp(!paused);
    }

        public void GoToMainMenu()
    {
        TriggerPopUp();
        SceneTransitioner.instance.OnTransition.AddListener(() => ChangeLevels.instance.BackToMainMenu());
        SceneTransitioner.instance.StartTransition("Loading...", "", 1);
        
    }

    public void GoToMecanics()
    {
        TriggerPopUp();
        SceneTransitioner.instance.OnTransition.AddListener(() => ChangeLevels.instance.BackToMecanics());
        SceneTransitioner.instance.StartTransition("Loading...", "", 1);
    }

    public void GoToNextLevel()
    {
        TriggerPopUp();
        ChangeLevels.instance.ChangeLevel(PersistanceData.level + 1);
    }

    public void ActiveButtons(bool active)
    {
        foreach(Button b in buttons)
        {
            b.interactable = active;
        }
    }
}