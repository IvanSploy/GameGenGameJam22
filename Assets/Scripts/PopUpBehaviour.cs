using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBehaviour : MonoBehaviour
{
    //Referencias
    private Animator anim;
    public bool pauseOnTrigger = false;
    [HideInInspector]
    public bool paused = false;

    void Start()
    {
        anim = GetComponent<Animator>();
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
}
