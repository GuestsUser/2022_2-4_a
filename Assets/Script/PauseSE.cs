using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSE : MonoBehaviour
{
    public AudioClip Cursolmove;
    public AudioClip Decision; //決定音
    public AudioClip Cancel;

    bool SEflag; //効果音を鳴らせるフラグ
    bool SEflag2; //効果音を鳴らせるフラグ
    bool DecisionFlag;
    bool CancelFlg;
    AudioSource audio; //後々使う(音量調整など)

    public GameObject pause;
    Pausemenu _pause; //Resultメニューで扱う変数を持ってくるため

    OptionMenu _option;

    int Count; 
    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        pause = GameObject.Find("PauseScript");
        _pause = pause.GetComponent<Pausemenu>();
        SEflag = false;
        SEflag2 = false;
        DecisionFlag = false;
        CancelFlg = false;
        _option = GetComponent<OptionMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SEflag);

        if (Count == 2)
        {
            if (_option.OnCancelSE == true) //キャンセル時
            {
                SEflag = true;
                audio.PlayOneShot(Cancel);
                Count -= 1;
            }

            //↓オプションメニューで上入力時
            if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == -1) ||Input.GetAxis("Vertical2") == -1)
            {
                if (SEflag == true)
                {
                    Debug.Log("aaa");
                    audio.PlayOneShot(Cursolmove);
                    SEflag = false;
                }
            }//↓オプションメニューで下入力時
            else if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == 1) ||Input.GetAxis("Vertical2") == 1)
            {
                if (SEflag == true)
                {
                    audio.PlayOneShot(Cursolmove);
                    SEflag = false;
                }
            }
            else if(!Input.anyKey)
            {
                SEflag = true;
            }
        }

        if (_pause._OptionFlg == true)
        {
            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetButton("A"))
            {
                if (SEflag == false) //Aボタンを押した時の処理
                {
                    if (Count < 2)
                    {
                        Count++;
                    }

                    audio.PlayOneShot(Decision);
                    SEflag = true;
                }
            }
        }

        //Debug.Log(DecisionFlag);

        if (_pause._OptionFlg == false)
        {
            if (_pause._showMenu == true)
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
                    else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && !Input.GetButton("B") && Input.GetButton("A"))
                    {
                        if (SEflag == false) //Aボタンを押した時の処理
                        {
                            
                            Count++;
                            Debug.Log("入りました");
                            audio.PlayOneShot(Decision);
                            SEflag = true;
                            if (_pause._MenuNumber != 1)
                            {
                                DecisionFlag = true;
                            }
                        }
                    }
                    else if (!Input.anyKey)
                    {
                        SEflag = false;
                    }



                }



            }

            switch (Count)
            {
                case 0: //ポーズメニュー開く前
                    if (_pause._showMenu == true)
                    {
                        if (SEflag == false)
                        {
                            audio.PlayOneShot(Decision);
                            SEflag = true;
                            CancelFlg = true;
                            Count++;
                        }
                    }
                    break;
                case 1: //ポーズメニュー開いた状態
                    if (Input.GetButton("Start") || Input.GetButton("B"))
                    {
                        if(_option.PauseCancelSE == false)
                        {
                            if (SEflag == false)
                            {
                                CancelFlg = false;
                                SEflag = true;
                                audio.PlayOneShot(Cancel);
                                Debug.Log("通りました");
                                Count = 0;
                            }
                        }
                        
                    }
                    if(_pause._OptionFlg == true)
                    {
                        if(Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetButton("A"))
                        if (SEflag == false)
                        {
                            SEflag = true;
                            audio.PlayOneShot(Decision);
                        }
                    }
                    

                    break;
                //case 2:
                //    if(_option.OnCancelSE == false)
                //    {
                //        if (Input.GetButton("Start") || Input.GetButton("B"))
                //        {
                //            if (SEflag == false)
                //            {
                //                CancelFlg = false;
                //                SEflag = true;
                //                audio.PlayOneShot(Cancel);
                //                Count--;
                //            }
                //        }
                //    }
                    //else
                    //{
                    //    _option.OnCancelSE = false;
                    //}

                    //break;
            }
        }
        //
    }
}

