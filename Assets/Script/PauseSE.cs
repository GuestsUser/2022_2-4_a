using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSE : MonoBehaviour
{
    public AudioClip Cursolmove;
    public AudioClip Decision; //決定音

    bool SEflag; //効果音を鳴らせるフラグ
    bool SEflag2; //効果音を鳴らせるフラグ
    bool DecisionFlag;
    AudioSource audio; //後々使う(音量調整など)

    public GameObject pause;
    Pausemenu _pause; //Resultメニューで扱う変数を持ってくるため
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        pause = GameObject.Find("PauseScript");
        _pause = pause.GetComponent<Pausemenu>();
        SEflag = false;
        SEflag2 = false;
        DecisionFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_pause._push_scene == true)//他のシーンに遷移した時
        {
            DecisionFlag = false;
        }
        else
        {
            if (Time.timeScale == 0)//タイムスケールが0で
            {
                if (DecisionFlag == false) //決定ボタンを押していない間
                {
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
                    else if ((Input.GetAxis("Vertical") != 1 && Input.GetAxis("Vertical") != -1 && Input.GetAxis("Vertical2") != 1 && Input.GetAxis("Vertical2") != -1 && Input.GetButton("A")) || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                    {
                        if (SEflag == false) //Aボタンを押した時の処理
                        {
                            if (_pause._MenuNumber != 1) //真ん中のオプションは未実装
                            {
                                DecisionFlag = true;
                                audio.PlayOneShot(Decision);
                                SEflag = true;
                            }
                            
                        }
                    }
                    else
                    {
                        SEflag = false;
                    }


                }



            }

        }
    }
}
