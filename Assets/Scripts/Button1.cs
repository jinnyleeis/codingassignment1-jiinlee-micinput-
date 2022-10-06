using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    Mic mic;
    public GameObject micobject;
    private void Start()
    {

        mic = micobject.GetComponent<Mic>();
        Button button = this.gameObject.GetComponent<Button>();//접근
        //접근 & 이벤트 등록
        button.onClick.AddListener(Clicked);
        
    }

    private void Clicked() {
        //여기서 재생시키자. 
        mic.StartPlaying();
    }
}
