using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    /*オプションメニューに必要なもの*/
    Pausemenu pause;

    public GameObject OptionPanel;
    RectTransform _OptionPanel;

    public GameObject menu;
    RectTransform _menu;

    

    bool ShowFlg; //メニューが出ているのか
    /**/

    /*カーソルムーブ*/
    public GameObject Cursor; //Y35,-45
    RectTransform _Cursor;

    public static int MenuNumber = 0;
    bool CursorFlg; //入力長押し対策

    public GameObject BGM;
    public GameObject SE;

    Text _BGM; //音楽
    Text _SE; //効果音
    /**/

    /*スライダームーブ*/
    public GameObject Slider1; 
    RectTransform _Slider1;
    public GameObject Slider2;
    RectTransform _Slider2;

    //SoundVolumu Volume;
    /**/

    /*イージング*/
    private float Outtime = 0.0f;
    private float InTime = 0.0f;
    private float easingTime = 5.0f;

    bool INFlg; //EasInしても良いよフラグ

    /*ロード用*/
    bool LoadFlg;

    /*SE用*/
    public bool OnCancelSE;
    public bool PauseCancelSE;

    float SliderX1;
    float SliderX2;

    //オプションメニューの隠れている位置 X(490)
    //出てくるときのイメージ オプションを押したら右から登場

    // Start is called before the first frame update
    void Start()
    {
        pause = GetComponent<Pausemenu>();
        ShowFlg = false;
        LoadFlg = false;

        INFlg = false;

        CursorFlg = false;
        OnCancelSE = false;
        PauseCancelSE = false;

        

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SoundVolumu.BGMVol);
        Debug.Log(SoundVolumu.SEVol);
        ShowOption();
    }

    void ShowOption()
    {
        if (pause._showMenu == true)
        {
            LoadObject();

        }
        else
        {
            PauseCancelSE = false;
        }
        if (pause._OptionFlg == true)
        {
            EasOut();
            CursorMove();
            SliderMove();
            InTime = 0;

            if (!Input.GetButton("A") && Input.GetButton("Start") || Input.GetButton("B"))//Bボタンを押したら
            {
                OnCancelSE = true;
                pause._OptionFlg = false;

            }

        } 
        else if(pause._showMenu == true && pause._OptionFlg == false)
        {
            EasIN();
            Outtime = 0;
            MenuNumber = 0;
        }
    }
    void CursorMove()
    {
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Horizontal2") == 0 && Input.GetAxis("Vertical") == -1 || Input.GetAxis("Vertical2") == -1) //下に入力された時
        {
            if (CursorFlg == false)
            {
                CursorFlg = true;
                if (++MenuNumber > 1) MenuNumber = 0;
            }
        }
        else if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Horizontal2") == 0 && Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical2") == 1)//上に入力された時
        {
            if (CursorFlg == false)
            {
                CursorFlg = true;
                if (--MenuNumber < 0) MenuNumber = 1;
               // Debug.Log("上入力");
            }

        }
        else
        {
            CursorFlg = false;
        }

        switch (MenuNumber)//カーソル移動とテキストカラー変更
        {
            case 0:
                _BGM.color = new Color(1,1,1,1);
                _SE.color = new Color(0, 0, 0, 1);
                _Cursor.localPosition = new Vector3(0, 35, 0);
                break;

            case 1:
                _SE.color = new Color(1, 1, 1, 1);
                _BGM.color = new Color(0, 0, 0, 1);
                _Cursor.localPosition = new Vector3(0, -45, 0);
                break;
        }
    }
    
    void SliderMove() //音量調整
    {
        switch (MenuNumber)//スライダーの移動
        {
            case 0:
                if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal2") == 1) //右
                {
                    if (++SoundVolumu.BGMVol > 100) SoundVolumu.BGMVol = 100;
                }
                else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Horizontal2") == -1)
                {
                    if (--SoundVolumu.BGMVol < 0) SoundVolumu.BGMVol = 0;
                }
                _Slider1.localPosition = new Vector3(SliderX1 + (SoundVolumu.BGMVol*3), _Slider1.localPosition.y, _Slider1.localPosition.z);
                _Slider2.localPosition = new Vector3(SliderX2 + (SoundVolumu.SEVol * 3), _Slider2.localPosition.y, _Slider2.localPosition.z);
                break;

            case 1:
                if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal2") == 1) //右
                {
                    if (++SoundVolumu.SEVol > 100) SoundVolumu.SEVol = 100;
                }
                else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Horizontal2") == -1)
                {
                    if (--SoundVolumu.SEVol < 0) SoundVolumu.SEVol = 0;
                }
                _Slider2.localPosition = new Vector3(SliderX2 + (SoundVolumu.SEVol * 3), _Slider2.localPosition.y, _Slider2.localPosition.z);
                _Slider1.localPosition = new Vector3(SliderX1 + (SoundVolumu.BGMVol * 3), _Slider1.localPosition.y, _Slider1.localPosition.z);
                break;
        }
        

    }
    void LoadObject()
    {
        if (LoadFlg == false)
        {
            OptionPanel = GameObject.Find("OptionPanel");
            _OptionPanel = OptionPanel.GetComponent<RectTransform>();

            menu = GameObject.Find("menu"); //ポーズメニューを移動させるのに使う
            _menu = menu.GetComponent<RectTransform>();

            Cursor = GameObject.Find("cursor2");
            _Cursor = Cursor.GetComponent<RectTransform>();

            BGM = GameObject.Find("BGM");

            SE = GameObject.Find("SE");
            _BGM = BGM.GetComponent<Text>();
            _SE = SE.GetComponent<Text>();

            Slider1 = GameObject.Find("BGMHandle");
            _Slider1 = Slider1.GetComponent<RectTransform>();
            Slider2 = GameObject.Find("SEHandle");
            _Slider2 = Slider2.GetComponent<RectTransform>();

            SliderX1 = _Slider1.localPosition.x;
            SliderX2 = _Slider2.localPosition.x;

            LoadFlg = true;
        }

    }

    void EasOut() //登場時
    {
        PauseCancelSE = false;
        INFlg = true;
        Outtime += 0.33333f;
        if (Outtime < easingTime)
        {
            _OptionPanel.localPosition = new Vector3((Easing.ExpOut(Outtime, easingTime, 490, 0)), 0, 0);
            _menu.localPosition = new Vector3((Easing.ExpOut(Outtime, easingTime, 0, -490)), 39.8f, 0);
        }
        else
        {
            _OptionPanel.localPosition = new Vector3(0, 0, 0);
            _menu.localPosition = new Vector3(-490, 39.8f, 0);

        }
    }
    void EasIN() //退場時
    {
        PauseCancelSE = true;
        OnCancelSE = false;
        if (INFlg == true)
        {
            InTime += 0.33333f;
            if (InTime < easingTime)
            {
                _OptionPanel.localPosition = new Vector3((Easing.ExpOut(InTime, easingTime, 0, 490)), 0, 0);
                _menu.localPosition = new Vector3((Easing.ExpOut(InTime, easingTime, -490, 0)), 39.8f, 0);
            }
            else
            {
                _OptionPanel.localPosition = new Vector3(490, 39.8f, 0);
                _menu.localPosition = new Vector3(0, 39.8f, 0);
                INFlg = false;
                pause.CancelCount = 0;
                pause.Decision = false;
                PauseCancelSE = false;
            }
        }
    }
}
