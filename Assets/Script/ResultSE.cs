using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSE : MonoBehaviour
{
    public AudioClip Cursolmove; //カーソル移動
    public AudioClip Decision; //決定音
    //private static bool SEflag;
    bool SEflag; //効果音を鳴らせるフラグ
    bool SEflag2; //効果音を鳴らせるフラグ
    bool DecisionFlag;
    AudioSource audio; //後々使う(音量調整など)

    public GameObject Retry;
    ResultScript Result; //Resultメニューで扱う変数を持ってくるため

    int Count;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Retry = GameObject.Find("RetryScript");
        Result = Retry.GetComponent<ResultScript>();
        SEflag = false;
        SEflag2 = false;
        DecisionFlag = false;
        Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Result._showMenu);

        audio.volume = SoundVolumu.SEVol / 100;

        if (Result._showMenu == true) //決定ボタンを押していない間
        {
            if (DecisionFlag == false)
            {
                //Debug.Log("第一関門突破");
                if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    if(Result._push_scene == false)
                    {
                        if (SEflag == false)
                        {
                            audio.PlayOneShot(Cursolmove);
                            SEflag = true;
                        }
                    }
                }
                else if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == 1))
                {
                    if (Result._push_scene == false)
                    {
                        if (SEflag == false)
                        {
                            audio.PlayOneShot(Cursolmove);
                            SEflag = true;
                        }
                    }
                }
                else if ((Input.GetAxis("Vertical") != 1 && Input.GetAxis("Vertical") != -1 && Input.GetAxis("Vertical2") != 1 && Input.GetAxis("Vertical2") != -1 && Input.GetButton("A")) || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    switch (Count)
                    {
                        case 0:
                            if (SEflag == false)
                            {
                                Count++;
                                audio.PlayOneShot(Decision);
                                SEflag = true;
                            }
                            break;
                        case 1:
                            if (SEflag == false)
                            {
                                DecisionFlag = true;

                                audio.PlayOneShot(Decision);
                                SEflag = true;
                            }
                            break;
                    }
                }
                else
                {
                    SEflag = false;
                }
            }


        }
        else
        {
            DecisionFlag = false;
        }
    }
    
}


