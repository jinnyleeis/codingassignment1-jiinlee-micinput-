using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    Mic mic;
    public GameObject micobject;
    public Button backbutton;
    public Button frontbutton;
    private void Start()
    {

        mic = micobject.GetComponent<Mic>();
        Button button0 = this.gameObject.GetComponent<Button>();//접근
        //접근 & 이벤트 등록
        button0.onClick.AddListener(Clicked0);
        Button buttonback = backbutton.GetComponent<Button>();//접근
        //접근 & 이벤트 등록
       buttonback.onClick.AddListener(Clicked1);
        Button buttonfront = frontbutton.GetComponent<Button>();//접근
        //접근 & 이벤트 등록
        buttonfront.onClick.AddListener(Clicked2);
        
    }

    private void Clicked0() {
        //여기서 재생시키자. 
        mic.isrecoreded = true;
        mic.StartPlaying();
    }
    private void Clicked1() {
        //여기서 재생시키자. 
        mic.PlayBackStorage();
    }
    private void Clicked2() {
        //여기서 재생시키자. 
        mic.PlayFrontStorage();
    }
}
