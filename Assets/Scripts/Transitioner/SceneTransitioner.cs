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
    public Image logo;
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

    [ContextMenu("Ir a Selector de Niveles")]
    public void BackToLevels()
    {
        OnTransition.AddListener(() => ChangeLevels.instance.ChangeLevel(0));
        StartTransition(1);
    }

    public void StartTransition(string text, string subtext, float delay)
    {
        SetTitle(text);
        SetSubtitle(subtext);
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

    public void SetTitle(string text)
    {
        title.SetText(text);
    }

    public void SetSubtitle(string text)
    {
        subtitle.SetText(text);
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

    private void EndTransition()
    {
        anim.SetTrigger("ChangeState");
        OnTransition.RemoveAllListeners();
        OnEnd.Invoke();
    }
}
