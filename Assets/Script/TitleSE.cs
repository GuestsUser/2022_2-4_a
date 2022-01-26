using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSE : MonoBehaviour
{
    public AudioClip Cursolmove; //カーソル移動
    public AudioClip Decision; //決定音
    public AudioClip Cancel; //キャンセル音

    //private static bool SEflag;
    bool SEflag; //効果音を鳴らせるフラグ
    bool SEflag2; //効果音を鳴らせるフラグ
    bool DecisionFlag;

    bool LoadFlg;

    AudioSource audio;//後々使う(音量調整など)

    Titlemenu Title; //Titlemenuの変数を持ってくるよう

    public GameObject OptionPanel;
    TitleOption _option;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        Title = GetComponent<Titlemenu>();
        SEflag = false;
        SEflag2 = false;
        DecisionFlag = false;

        LoadFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        audio.volume = SoundVolumu.SEVol / 100;

        if (Title.OptionFlg == false)
        {
            LoadObject();
            if(_option.CancelFlg == true)
            {
                Debug.Log("SEflag");
                if (SEflag == false)
                {
                    
                    audio.PlayOneShot(Cancel);
                    _option.CancelFlg = false;
                    SEflag = true;
                }
            }
        }

        if (!Input.anyKey)
        {
            DecisionFlag = false;
        }

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
                
                    if (SEflag == false)
                    {
                        audio.PlayOneShot(Decision);
                        SEflag = true;
                        DecisionFlag = true;
                    }
            }

            if (Title._MenuNumber == 0) //モード選択のところにカーソルがある時
            {
                if ((Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal2") == 1) || (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Horizontal2") == -1))
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

        
    }
    void LoadObject()
    {
        if(LoadFlg == false)
        {
            OptionPanel = GameObject.Find("Cursor");
            _option = OptionPanel.GetComponent<TitleOption>();
            LoadFlg = true;
        }
        
    }
}
