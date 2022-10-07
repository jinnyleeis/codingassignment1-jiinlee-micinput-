using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    Mic mic;
    public Button recordbutton;
    public GameObject micobject;
    public Button buttonpause;
    public Text text;
    public Text text1;
    int recordlength=0;
    private Coroutine coroutine=null;
    private bool pauseclicked = false;
    
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
        
        //레고딩 시작 버튼
        Button button = recordbutton.gameObject.GetComponent<Button>();//접근
        //접근 & 이벤트 등록
        button.onClick.AddListener(Clicked);

        
    }

    void Update()
    {

        text1.text ="Recorded Length: "+recordlength.ToString();
        
    }

    private void Clicked0() {
        //여기서 재생시키자. 
      
            mic.StartPlaying();
           
           if(coroutine==null)
           {StartCoroutine(Recordtimer(false));}
               
           



    }
 

    private void Clicked2() {//recored중지.

        pauseclicked = true;
       mic.isrecoreded = false;
       StopAllCoroutines();
       coroutine = null;
    }
    
    
    private void Clicked() {//레코딩 시작 버튼
       
       
        mic.StartRecording();
        if (coroutine == null)
        {
            coroutine = StartCoroutine(Recordtimer(true));
        }

    }
    
    IEnumerator Recordtimer(bool startrecord)
    {
        int time = 0;
        

        while (true)
        {
            if (mic.isrecoreded == true && pauseclicked==false &&mic.recordfinish==true)
            {
                StopAllCoroutines();}

            yield return new WaitForSeconds(1f);
           
            time=time + 1;
            if (startrecord)
            {
                recordlength = time;
            }
           
                text.text = "Record/Play Timer: " + time.ToString();
            
            if(startrecord==false){
                
                if (time >= recordlength) break;
            }
            
            
        }

    }

}
