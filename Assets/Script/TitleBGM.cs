using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    public AudioClip BGM;
    AudioSource audio;

    bool BGMflg;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
        audio.loop = true;
        BGMflg = true;
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = SoundVolumu.BGMVol / 100;
        if (BGMflg == false)
        {
            audio.Stop();
        }

    }
}
