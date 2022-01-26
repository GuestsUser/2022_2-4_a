using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleOption : MonoBehaviour
{
    /*イージング*/
    private float Outtime = 0.0f;
    private float InTime = 0.0f;
    private float easingTime = 0.8f;

    /*オプションの表示*/
    public GameObject OptionPanel;
    RectTransform _OptionPanel;

    private static int MenuNumber;

    public bool ShowMenu;
    public bool CancelFlg;
    private bool LoadFlg;

    private bool CursorFlg;
    private bool EasINFlg;

    Titlemenu Title;

    /*カーソルムーブ*/
    public GameObject Cursor;
    RectTransform _Cursor;

    public GameObject BGM;
    public GameObject SE;

    Text _BGM; //音楽
    Text _SE; //効果音

    /*スライダームーブ*/
    public GameObject Slider1;
    RectTransform _Slider1;
    public GameObject Slider2;
    RectTransform _Slider2;

    /*SE用*/

    float SliderX1;
    float SliderX2;

    // Start is called before the first frame update
    void Start()
    {
        
        Title = GetComponent<Titlemenu>();
        MenuNumber = 0;
        

        ShowMenu = false;
        //OptionPanel.SetActive(false);
        LoadFlg = false;


        OptionPanel = GameObject.Find("OptionPanel");
        _OptionPanel = OptionPanel.GetComponent<RectTransform>();
        _OptionPanel.localScale = new Vector3(0, 0, 1);

        EasINFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Title.OptionFlg == true)
        {
            LoadObject();
            
            if (ShowMenu == true)
            {
                InTime = 0;
                OptionPanel.SetActive(true);
                EasOut();
                CursorMove();
                SliderMove();
            }

            if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetButton("Start") || Input.GetButton("B"))
            {
                ShowMenu = false;
                EasINFlg = true;
                CancelFlg = true;
            }

        }
        if (EasINFlg == true && ShowMenu == false)
        {
            EasIn();
        }
        
    }

    private void CursorMove()
    {
        if (!Input.GetButton("A") && Input.GetAxis("Vertical") == -1 || Input.GetAxis("Vertical2") == -1) //下に入力された時
        {
            if (CursorFlg == false)
            {
                CursorFlg = true;
                if (++MenuNumber > 1) MenuNumber = 0;
            }
        }
        else if (!Input.GetButton("A") && Input.GetAxis("Vertical") == 1 || Input.GetAxis("Vertical2") == 1)//上に入力された時
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
                _BGM.color = new Color(1, 1, 1, 1);
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
                _Slider1.localPosition = new Vector3(SliderX1 + (SoundVolumu.BGMVol * 3), _Slider1.localPosition.y, _Slider1.localPosition.z);
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
    private void EasOut()
    {
        
        if (ShowMenu == true)
        {
            InTime = 0;
            //_pausepanel.localScale = new Vector3(0, 0, 1);
            //イージング
            Outtime += 0.333333f / 3;

            if (Outtime < easingTime)
            {
                _OptionPanel.localScale = new Vector3((Easing.ExpOut(Outtime, easingTime, 0, 1)), ((Easing.ExpOut(Outtime, easingTime, 0, 1))), 1);

            }
            else
            {
                _OptionPanel.localScale = new Vector3(1, 1, 1);
            }
        }
        else
        {
            //閉じるアニメーションが要らない場合これでok
            //_pausepanel.localScale = new Vector3(0, 0, 1);
        }
    }
    private void EasIn()
    {
        Title.OptionFlg = false;
        Outtime = 0;
        
            InTime += 0.333333f / 3;

            if (InTime < easingTime)
            {
                _OptionPanel.localScale = new Vector3(1 - (Easing.ExpOut(InTime, easingTime, 0, 1)), 1 - (Easing.ExpOut(InTime, easingTime, 0, 1)), 1);
            }
            else
            {
                _OptionPanel.localScale = new Vector3(0, 0, 1);
                
                MenuNumber = 0;
                OptionPanel.SetActive(false);
                EasINFlg = false;
            }
        //}
        //else if (ShowMenu == true)
        //{
        //    InTime = 0;
        //}
    }
    private void LoadObject()
    {
        if(LoadFlg == false)
        {
            Cursor = GameObject.Find("cursor2");
            _Cursor = Cursor.GetComponent<RectTransform>();
            BGM = GameObject.Find("BGM");
            _BGM = BGM.GetComponent<Text>();
            SE = GameObject.Find("SE");
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
}
