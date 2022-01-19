﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSE : MonoBehaviour
{
    public AudioClip Cursolmove; //カーソル移動
    public AudioClip Decision; //決定音
    //private static bool SEflag;
    bool SEflag; //効果音を鳴らせるフラグ
    bool SEflag2; //効果音を鳴らせるフラグ
    bool DecisionFlag;
    AudioSource audio;

    Titlemenu Title; //Titlemenuの変数を持ってくるよう

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Title = GetComponent<Titlemenu>();
        SEflag = false;
        SEflag2 = false;
        DecisionFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(DecisionFlag == false)
        {

            //Debug.Log(Title._MenuNumber);
            if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
            {
                if (SEflag == false)
                {
                    audio.PlayOneShot(Cursolmove);
                    SEflag = true;
                }
            }
            else if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == 1))
            {
                if (SEflag == false)
                {
                    audio.PlayOneShot(Cursolmove);
                    SEflag = true;
                }
            }
            else
            {
                SEflag = false;
            }

            if ((Input.GetAxis("Vertical") != 1 && Input.GetAxis("Vertical") != -1 && Input.GetAxis("Vertical2") != 1 && Input.GetAxis("Vertical2") != -1 && Input.GetButton("A")) || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
            {
                if (Title._MenuNumber != 0)
                {
                    if (SEflag == false)
                    {
                        audio.PlayOneShot(Decision);
                        SEflag = true;

                    
                        DecisionFlag = true;
                    }
                    
                }
            }

            if (Title._MenuNumber == 0) //モード選択のところにカーソルがある時
            {
                if ((!Input.GetButton("A") && Input.GetAxis("Horizontal") == 1) || (!Input.GetButton("A") && Input.GetAxis("Horizontal2") == 1) || (!Input.GetButton("A") && Input.GetAxis("Horizontal") == -1) || (!Input.GetButton("A") && Input.GetAxis("Horizontal2") == -1))
                {
                    if (SEflag2 == false)
                    {
                        audio.PlayOneShot(Cursolmove);
                        SEflag2 = true;
                    }
                }
                else
                {
                    SEflag2 = false;
                }

            }

        }

        //switch (Title._MenuNumber)
        //{
        //    case 0:
        //        if ((!Input.GetButton("A") && Input.GetAxis("Horizontal") == -1) || (!Input.GetButton("A") && Input.GetAxis("Horizontal2") == -1))
        //        {
        //            if (SEflag == false)
        //            {
        //                audio.PlayOneShot(Cursolmove);
        //                SEflag = true;
        //            }
        //        }
        //        break;
        //}
    }
}
