using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))] //audiosource必須化

public class SEResuorce : MonoBehaviour //seの音を保持しておく為のコンポーネント
{
    [SerializeField] private AudioClip _d_se; //決定音
    [SerializeField] private AudioClip _c_se; //キャンセル音
    [SerializeField] private AudioClip _move_se; //十字キー音
    private AudioSource _audio_source; //音を鳴らすためのaudiosource

    public AudioClip d_se { get { return _d_se; } }
    public AudioClip c_se { get { return _c_se; } }
    public AudioClip move_se { get { return _move_se; } }
    public AudioSource audio_source { get { return _audio_source; } }

    // Start is called before the first frame update
    void Start()
    {
        _audio_source = GetComponent<AudioSource>(); //自身に付いてるaudiosourceを格納する
    }

    //Input.GetKey(KeyCode.Joystick1Button0)
}
