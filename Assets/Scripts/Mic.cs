using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mic : MonoBehaviour
{
    private AudioSource audiosource;
    // Start is called before the first frame update
    void Start()
    {
        audiosource = this.gameObject.GetComponent<AudioSource>();
    }

    public void StartPlaying()//재생을 위한
    {
        audiosource.Play();//오디오 소스 클래스의 멤버 메소드인 Play()함수를 통해,
                           //재생한다. 
    }

    public void StartRecording()//녹음을 위한 
    {
        audiosource.clip = Microphone.Start(Microphone.devices[0].ToString(),false,3,44100);
        //devices: 현재 장치에 연결된 마이크들의 이름이 있는 리스트이다. 
        //false를 통해, 연속 녹음이 아닌, 한번, 레코드 버튼 눌렀을 시, 한번 녹음.
        //clip은 default 재생되는 오디오 클립입니다.
        //default오디오 클립에다, 마이크로폰의 인풋을 받아 녹음한 것을 저장하는 것입니다.
        //녹음은 마이크로 폰의 Start 메소드를 통해, 실행한다.
        //Microphone 클래스를 이용해서, 연결된 마이크를 사용해서, AudioClip에 녹음할 수 있도록 합니다.
        //Microphone의 Start메소드는, AudioClip타입을 반환하며 clip에 오디오 클립을 저장합니다. 
        // '시작'을 하는 기능입니다.
        //Use this class to record to an AudioClip using a connected microphone.
        //Start함수는 파라미터로, 디바이스명(string),계속해서 녹음할것인지 여부(bool), 늑음할 클립의 길이(int),
        //녹음할 클립의 샘플 rate
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
