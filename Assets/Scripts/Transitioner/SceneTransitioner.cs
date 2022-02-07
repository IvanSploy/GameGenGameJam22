using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class SceneTransitioner : MonoBehaviour
{
    //Referencias
    public TMP_Text title;
    public TMP_Text subtitle;
    public Image imagen;
    public Image logo;
    public Image backgroundColor;
    public static SceneTransitioner instance;
    private Animator anim;

    //Eventos
    public AnimationClip[] delayClips;
    public UnityEvent OnTransition;
    public UnityEvent OnEnd;

    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        instance = this;
        DontDestroyOnLoad(gameObject);

        anim = GetComponent<Animator>();
    }

    /*private void Start()
    {
        StartTransition("Hola chami", 1);
    }*/

    public void StartTransition(string text, float delay)
    {
        SetTitle(text);
        StartTransition(delay);
    }

    public void StartTransition(string text)
    {
        anim.SetTrigger("ChangeState");
        SetTitle(text);
        StartCoroutine(WaitForTransition(0));
    }
    public void StartTransition(float delay)
    {
        anim.SetTrigger("ChangeState");
        StartCoroutine(WaitForTransition(delay));
    }

    public void SetImage(Sprite imagen)
    {
        this.imagen.sprite = imagen;
    }
    public void SetLogo(Sprite logo)
    {
        this.logo.sprite = logo;
    }
    public void SetTitle(string text)
    {
        title.SetText(text);
    }
    public void SetSubtitle(string text)
    {
        subtitle.SetText(text);
    }
    public void SetBackgroundColor(Color color)
    {
        backgroundColor.color = color;
    }
    public void SetTextColor(Color color)
    {
        title.color = color;
        subtitle.color = color;
    }

    public void ResetEvents()
    {
        OnTransition.RemoveAllListeners();
        OnEnd.RemoveAllListeners();
        
    }

    private IEnumerator WaitForTransition(float delay)
    {
        foreach(AnimationClip ac in delayClips)
             delay += ac.length;
        yield return new WaitForSeconds(delay);
        OnTransition.Invoke();
        EndTransition();
    }

    public void EndTransition()
    {
        anim.SetTrigger("ChangeState");
        OnEnd.Invoke();
    }
}
