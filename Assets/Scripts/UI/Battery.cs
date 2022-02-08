using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.Animations;

public class Battery : MonoBehaviour
{
    [SerializeField] private float seconds;
    private Slider slider;
    private int sleepTime;
    private int maxSeconds;
    private bool isSelected = false;
    [SerializeField] private Animator animator;
    [SerializeField] private AnimatorController moreTime;
    private TextMeshProUGUI TMP;
    [SerializeField] private Image _image;
    void Start()
    {
        maxSeconds = (int) seconds;
        TMP = GetComponent<TextMeshProUGUI>();
        //slider = this.GetComponent<Slider>();
        //En Unity configurar el MaxValue (tiempo de juego :D)
        //slider.maxValue = seconds;
        //slider.value = seconds;
        sleepTime = FindObjectOfType<CinematicLevel>().timeCinematic;
        StartCoroutine(BeforeStart());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator BeforeStart()
    {
        yield return new WaitForSeconds(sleepTime);
        StartCoroutine(DecrementTime());
        if (isSelected)
            PassiveMoreTime();
    }

    IEnumerator DecrementTime()
    {
        TMP.text = "" + (int)((seconds / maxSeconds) * 100) + "%";
        animator.SetInteger("BatteryLevel", (int) ((seconds / maxSeconds) * 100));
        //slider.value -= Time.deltaTime;
        seconds -= Time.deltaTime;
        yield return new WaitForEndOfFrame();
        if (seconds <= 0)
        {
            FindObjectOfType<GameManager>().BatteryOff();
        }
        else
        {
            StartCoroutine((DecrementTime()));
        }
    }
    
    //Call this method when User select Time Hability
    public void SelectPassive()
    {
        isSelected = true;
    }
    
    private void PassiveMoreTime()
    {
        seconds += 20;
        animator.runtimeAnimatorController = moreTime;
        animator.Play("MoreBattery");
    }
}
