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

    Text BGM; //音楽
    Text SE; //効果音
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
    }

    // Update is called once per frame
    void Update()
    {
        ShowOption();
    }

    void ShowOption()
    {
        if (pause._showMenu == true)
        {
            LoadObject();
        }
        if (pause._OptionFlg == true)
        {
            EasOut();
            InTime = 0;

            if (!Input.GetButton("A") && Input.GetButton("Start") || Input.GetButton("B"))//Bボタンを押したら
            {
                OnCancelSE = true;
                pause._OptionFlg = false;

            }

        } 
        else
        {
            EasIN();
            Outtime = 0;
        }
        void CursorMove()
        {
            CursorFlg = true;
            if (CursorFlg)
            {

            }
            
        }

        void LoadObject()
        {
            if(LoadFlg == false)
            {
                OptionPanel = GameObject.Find("OptionPanel");
                _OptionPanel = OptionPanel.GetComponent<RectTransform>();

                menu = GameObject.Find("menu"); //ポーズメニューを移動させるのに使う
                _menu = menu.GetComponent<RectTransform>();

                Cursor = GameObject.Find("cursor2");
                _Cursor = Cursor.GetComponent<RectTransform>();

                
                LoadFlg = true;
            }
            
        }

        void EasOut() //登場時
        {
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
                }
            }
        }
    }
}
