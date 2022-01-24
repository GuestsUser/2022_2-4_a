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
    public GameObject Cursor;
    RectTransform _Cursor;

    public static int MenuNumber = 0;
    bool CursorFlg; //入力長押し対策

    Text BGM; //音楽
    Text SE; //効果音
    /**/

    /*イージング*/
    private float Outtime = 0.0f;
    private float InTime = 0.0f;
    private float easingTime = 20.0f;

    //オプションメニューの隠れている位置 X(490)
    //出てくるときのイメージ オプションを押したら右から登場

    // Start is called before the first frame update
    void Start()
    {
        ShowFlg = false;

        OptionPanel = GameObject.Find("OptionPanel");
        _OptionPanel = OptionPanel.GetComponent<RectTransform>();

        menu = GameObject.Find("menu"); //ポーズメニューを移動させるのに使う
        _menu = menu.GetComponent<RectTransform>();

        Cursor = GameObject.Find("cursor2");
        _Cursor = Cursor.GetComponent<RectTransform>();

        pause = GetComponent<Pausemenu>();
    }

    // Update is called once per frame
    void Update()
    {
        ShowOption();
    }

    void ShowOption()
    {
        if (pause._OptionFlg == true)
        {
            EasOut();
            InTime = 0;

            if (Input.GetButton("Start") || Input.GetButton("B"))//Bボタンを押したら
            {
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

        }

        void EasOut() //登場時
        {
            Outtime += 0.33333f;
            if (Outtime < easingTime)
            {
                _OptionPanel.localPosition = new Vector3((Easing.ExpOut(Outtime, easingTime, 480, 0)), 0, 0);
                _menu.localPosition = new Vector3((Easing.ExpOut(Outtime, easingTime, 0, -480)), 39.8f, 0);
            }
            else
            {
                _OptionPanel.localPosition = new Vector3(0, 0, 0);
                _menu.localPosition = new Vector3(-480, 39.8f, 0);
            }
        }
        void EasIN() //退場時
        {
            InTime += 0.33333f;
            if (InTime < easingTime)
            {
                _OptionPanel.localPosition = new Vector3((Easing.ExpOut(InTime, easingTime, 0, 480)), 0, 0);
                _menu.localPosition = new Vector3((Easing.ExpOut(InTime, easingTime, -480, 0)), 39.8f, 0);
            }
            else
            {
                _OptionPanel.localPosition = new Vector3(480, 39.8f, 0);
                _menu.localPosition = new Vector3(0, 39.8f, 0);
            }
        }
    }
}
