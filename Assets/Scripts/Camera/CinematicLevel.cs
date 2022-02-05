using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using DG.Tweening;

public class CinematicLevel : MonoBehaviour
{
    [SerializeField] GameObject Player;
    public int timeCinematic;

    [SerializeField] private GameObject FreeLook;
    
    // Start is called before the first frame update
    void Start()
    {
        Player.GetComponent<PlayerController>().OnDisable();
        GetComponent<CinemachineVirtualCamera>().Follow = FreeLook.transform;
        DOTween.Init();
        FreeLook.transform.DOMove(Player.transform.position, timeCinematic).OnComplete(FinishCinenmatic);
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FinishCinenmatic()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = Player.transform;
        Destroy(FreeLook.gameObject);
        Player.GetComponent<PlayerController>().OnEnable();
    }
}
