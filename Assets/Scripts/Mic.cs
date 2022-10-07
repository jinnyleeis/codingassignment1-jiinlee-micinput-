using System.Collections;
using System.Collections.Generic;
using Unity.Profiling.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Mic : MonoBehaviour
{
    static private int num=1;
    private AudioSource audiosource;
    private AudioClip mic;
    AudioClip[] audioclips = new AudioClip[num];
    private float[] samples = null;
    private List<float> readSamples = null;
    public int lengthsec;
    public bool isloop;
    private int samplerate = 4410;//frequency 
    private int lastSamplePos = 0;
    public bool isrecoreded=false;
    private int channels = 0;
    
    public float READ_FLUSH_TIME = 0.5f;
    private float writeFlushTimer = 0.0f;
    private float readFlushTimer = 0.0f;

    // Start is called before the first frame update
    //private out;
    void Start()
    {
        readSamples = new List<float>();
        audiosource = this.gameObject.GetComponent<AudioSource>();

        
    }

    public void StartPlaying()//재생을 위한
    {
        readFlushTimer += Time.deltaTime;
        if (readFlushTimer > READ_FLUSH_TIME && readSamples.Count > 0)
        {
            //if (readUpdateId != previousReadUpdateId && readSamples != null && readSamples.Count > 0)
          //  {

              //  previousReadUpdateId = readUpdateId;

                audiosource.clip = AudioClip.Create("Real_time", readSamples.Count, channels, 44100, false);
                audiosource.clip.SetData(readSamples.ToArray(), 0);
                if (!audiosource.isPlaying)
                {
                    audiosource.Play();
                }

                SavetoAudioClipsStorage();
                readSamples.Clear();
                //readUpdateId++;
           // }

            readFlushTimer = 0.0f;
        }

        isrecoreded = false;

        //

    }

    public void StartRecording()//녹음을 위한 
    {
       
        
        
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
        
    }

    public void SavetoAudioClipsStorage()
    {
        
        audioclips[num - 1] = audiosource.clip;
        print(audioclips[num - 1]+"saved");
        num += 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (isrecoreded == true)
        {
            ReadMic();

            StartPlaying();
        }




    }

    private void ReadMic()
    {
        writeFlushTimer += Time.deltaTime;
        int currentPos = Microphone.GetPosition(Microphone.devices[0].ToString());
        //가장 최근에 기록한 오디오 샘플의 위치를 저장하는 변수이다.
        if (currentPos - lastSamplePos > 0)
        {
            samples = new float[currentPos - lastSamplePos];
            
            mic.GetData(samples, lastSamplePos);
            
            readSamples.AddRange(samples);
        }

        lastSamplePos = currentPos;


    }

    public void PlayBackStorage()
    {
        int playnum = num - 1;
        if (playnum > num - 1)
        {
            audiosource.clip = audioclips[playnum];
            audiosource.Play();
        }

}
    public void PlayFrontStorage()
    {
        int playnum = num + 1;
        if (playnum < num - 1)
        {
            audiosource.clip = audioclips[playnum];
            audiosource.Play();
        }

    }


}
