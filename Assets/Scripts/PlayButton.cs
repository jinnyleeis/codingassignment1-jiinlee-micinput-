using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    Mic mic;
    public GameObject micobject;
    public Button buttonpause;
    
    private void Start()
    {

        mic = micobject.GetComponent<Mic>();
        
        //플레이 시작 버튼
        Button button0 = this.gameObject.GetComponent<Button>();//접근
        //접근 & 이벤트 등록
        button0.onClick.AddListener(Clicked0);
        
     
        //레코딩 중지  버튼 
        Button pausebutton = buttonpause.GetComponent<Button>();//접근
        //접근 & 이벤트 등록
        buttonpause.onClick.AddListener(Clicked2);
        
    }

    private void Clicked0() {
        //여기서 재생시키자. 
      
            mic.StartPlaying();
           
           
     
    }
 

    private void Clicked2() {//recored중지.
       
       mic.isrecoreded = false;
    }
}
