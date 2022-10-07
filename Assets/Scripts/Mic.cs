using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Mic : MonoBehaviour
{
    //static private int num
    private AudioSource audiosource;
    private AudioClip mic;
    private float[] samples = null;
    private List<float> readSamples = null;
    public int lengthsec;
    public bool isloop;
    private int samplerate = 44100;//frequency 
    private int lastSamplePos = 0;
    public bool isrecoreded=false;
    private int channels ;
    
    public Text text1;
   
    public Text text2;
    
    
    private int currentPos;
    public Transform buffervisualization;
    

    
    

    // Start is called before the first frame update
    void Start()
    {
        readSamples = new List<float>();
        audiosource = this.gameObject.GetComponent<AudioSource>();

        
    }

    public void StartPlaying()//재생을 위한
    {
        print("start playing");
        
        print(readSamples.Count);

        audiosource.clip = AudioClip.Create("Real_time", readSamples.Count, channels, samplerate, false);
                audiosource.clip.SetData(readSamples.ToArray(), 0);
                
                if (audiosource.isPlaying==false)
                {
                    print("play!!");
                    audiosource.Play();
                    
               }

                readSamples.Clear();
               
              // readSamples.Clear();

    }
    
    

    public void StartRecording()//녹음을 위한 
    {

        
        print("start recording");
        mic = Microphone.Start(Microphone.devices[0].ToString(),isloop,lengthsec,samplerate);
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
        channels = mic.channels;
        isrecoreded = true;

    }

   

    // Update is called once per frame
    void Update()
    {
        if (isrecoreded == true) 
        {
            
            ReadMic();
           
        }
        text1.text = "lengthSec: "+lengthsec+" sampleRate: "+samplerate+" loop: "+isloop.ToString();
        text2.text = "samples: "+samples+" lastSamplePos: "+lastSamplePos+" currentPos-GetPosition(): "+currentPos.ToString();
        buffervisualization.localScale = new Vector3(currentPos*0.00003f,currentPos*0.00003f,currentPos*0.00003f);
        buffervisualization.position = new Vector3(0,0,20);





    }

    public void ReadMic()
    {
        print("read mic");
        
        currentPos = Microphone.GetPosition(Microphone.devices[0].ToString());
        //가장 최근에 기록한 오디오 샘플의 위치를 저장하는 변수이다.
        if (currentPos - lastSamplePos > 0)
        {
            samples = new float[(currentPos - lastSamplePos)];
            
            mic.GetData(samples, lastSamplePos);
            
            readSamples.AddRange(samples);
        }

        lastSamplePos = currentPos;

       


    

}
    
    


}
