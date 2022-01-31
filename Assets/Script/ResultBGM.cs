using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultBGM : MonoBehaviour
{
    public AudioClip BGM;
    AudioSource audio;

    bool BGMflg;

    //public GameObject Result;
    //ResultScript Flg;

    public GameObject Result;
    Phase3 Flg;

    float FadeVolume;
    // Start is called before the first frame update
    void Start()
    {
        //Flg = GameObject.Find("ResultScript");
        audio = GetComponent<AudioSource>();
        audio.Play();
        audio.loop = true;
        BGMflg = true;
        FadeVolume = 0;
        Result = GameObject.Find("UISystem");
        Flg = Result.GetComponent<Phase3>();
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = SoundVolumu.BGMVol / 100;
        if (Flg.end_bool == false)
        {
            audio.volume = SoundVolumu.BGMVol / 100;
        }
        else
        {
            FadeVolume--;
            audio.volume = ((SoundVolumu.BGMVol + FadeVolume) / 100);
            if (audio.volume < 0) BGMflg = false;
        }

        if (BGMflg == false)
        {
            audio.Stop();
        }

    }
}
