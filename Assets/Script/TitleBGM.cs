using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    public AudioClip BGM;
    AudioSource audio;

    bool BGMflg;

    public GameObject Title;
    Titlemenu Flg;

    float FadeVolume;
    float MinusVolume;
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
        audio.loop = true;
        BGMflg = true;
        Title = GameObject.Find("Cursor");
        Flg = Title.GetComponent<Titlemenu>();
        FadeVolume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(MinusVolume);
        if(Flg._Decision == false)
        {
            audio.volume = SoundVolumu.BGMVol / 100;
            MinusVolume = SoundVolumu.BGMVol / 50;
        }
        else
        {
            FadeVolume -= MinusVolume;
            audio.volume = ((SoundVolumu.BGMVol + FadeVolume) / 100);
            if (audio.volume < 0) BGMflg = false;
        }
        
        if (BGMflg == false)
        {
            audio.Stop();
        }

    }
}
