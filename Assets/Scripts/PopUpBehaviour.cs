using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PopUpBehaviour : MonoBehaviour
{
    //Referencias
    public static PopUpBehaviour instance;
    public Button nextLevelButton; 
    public TMP_Text info;
    private Animator anim;
    public bool pauseOnTrigger = false;
    [HideInInspector]
    public bool paused = false;

    void Awake()
    {
        if (instance) Destroy(gameObject);
        instance = this;
    }

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void TriggerPopUp(string text, Color color, bool passed)
    {
        nextLevelButton.interactable = passed;
        info.SetText(text);
        info.color = color;
        TriggerPopUp();
    }

    [ContextMenu("Trigger")]
    public void TriggerPopUp()
    {
        if (pauseOnTrigger)
        {
            if (paused)
            {
                Time.timeScale = 1;
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                paused = true;
            }
        }
        anim.SetTrigger("ChangeState");
    }

    public void GoToMainMenu()
    {
        TriggerPopUp();
        SceneTransitioner.instance.OnTransition.AddListener(()=>ChangeLevels.instance.BackToMainMenu());
        SceneTransitioner.instance.StartTransition("Loading...","", 1);
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
        ChangeLevels.instance.nowLevel++;
        SceneTransitioner.instance.OnTransition.AddListener(() => ChangeLevels.instance.ChangeLevel(ChangeLevels.instance.nowLevel));
        SceneTransitioner.instance.StartTransition($"Level {ChangeLevels.instance.nowLevel}", "Go to recharge.", 1);
    }
}
