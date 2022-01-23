using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour
{
    //イージング
    [SerializeField]
    private Easingtype type;
    private float time = 0.0f;
    private float easingTime = 1.0f;


    //

    /*リトライメニューに必要なもの*/
    public GameObject Retrymenu;
    public GameObject Retry;
    public GameObject BacktoTitle;

    RectTransform _Retrymenu;

    bool pushFlg;
    public bool _pushFlg { get{ return pushFlg; } }

    bool PushScene;
    bool ShowMenu; //メニュー表示フラグ
    public bool _showMenu {get { return ShowMenu; } }
    
    /**/

    /*カーソル移動に必要なもの*/
    public static int MenuNumber = 0; //メニュー番号
    public int _MenuNumber { get { return MenuNumber; } }
    public GameObject Cursor; //カーソルのポジション取得に必要
    bool CursorFlg; //
    bool Decision; //決定押したかどうか

    RectTransform _Cursor;

    Text _Retry;
    Text _BacktoTitle;
    /**/

    /*テキスト点滅に必要なもの*/
    public GameObject Operation; //操作ボタンを教えてくれるテキスト
    Text _Operation;
    float Opacity;
    bool MaxFlg; //透明度がmaxに達したかどうか
    float MaxOpacity; //透明度の分割数
    /**/

    /*フェードアウトに必要なもの*/
    public GameObject FadePanel;
    Image _FadePanel;
    float FadeOpacity;
    bool FadeFlg;
    float FadeMaxOpacity;
    /**/

    bool push_scene;
    public bool _push_scene {get{ return push_scene; } }

    bool LoadFlg;

    // Start is called before the first frame update
    void Start()
    {
        Operation = GameObject.Find("Operation");
        _Operation = Operation.GetComponent<Text>();

        FadePanel = GameObject.Find("FadePanel");
        _FadePanel = FadePanel.GetComponent<Image>();
        FadeFlg = false;

        Retrymenu.SetActive(false);
        pushFlg = false;
        PushScene = false;
        CursorFlg = false;
        Decision = false;

        MaxFlg = false;
        Opacity = 0;
        MaxOpacity = 100;

        FadeOpacity = 0;
        FadeMaxOpacity = 100;

        push_scene = false;
        LoadFlg = false;

        time = 0;
    }

    // Update is called once per frame
    void Update()
    {

        
        //time += (1 / 3);

        //Debug.Log(MenuScaleX);
        TextBlinking(); //テキストの点滅
        FadeIN();
        _ShowMenu(); //リトライメニューの表示
    }
    private void TextBlinking()
    {
        if (ShowMenu == false)
        {
            if (MaxFlg == false)
            {
                Opacity++;
                if (Opacity > MaxOpacity) MaxFlg = true;
            }
            else
            {
                Opacity--;
                if (Opacity < 0) MaxFlg = false;
            }
        }
        else
        {
            Opacity = 0;
        }
        //Debug.Log(Opacity);

        _Operation.color = new Color(0,0,0,Opacity/ MaxOpacity); 
    }
    private void FadeIN()
    {
        
        if (ShowMenu == true)
        {
            if (FadeFlg == false)
            {
                FadeOpacity++;
            }
            if(FadeOpacity > FadeMaxOpacity / 4)
            {
                FadeFlg = true;
            }
        }
        
        _FadePanel.color = new Color(0, 0, 0, FadeOpacity / FadeMaxOpacity);
    }
    private void _ShowMenu()
    {
        
        if (ShowMenu == true)
        {
            time += Time.deltaTime;
            CursorMove();

            /*↓リザルト画面をみたいと思う人が多ければ採用*/
            //if (Input.GetButton("B"))
            //{
            //    ShowMenu = false;
            //    time = 0;
            //    Retrymenu.SetActive(false);
            //}



            if (time < easingTime)
            {
                _Retrymenu.localScale = new Vector3((Easing.ExpOut(time, easingTime, 0, 1)), ((Easing.ExpOut(time, easingTime, 0, 1))), 1);
            }
            else
            {
                _Retrymenu.localScale = new Vector3(1, 1, 1);
            }
                
        }

        if (Input.GetButton("A"))
        {
            if (Decision == false)
            {
                Decision = true;
                if (ShowMenu == false)
                {
                    ShowMenu = true;
                    LoadObject();
                    Retrymenu.SetActive(true);
                    _Retrymenu.localScale = new Vector3(0, 0, 1);
                }
            }
        }
        else
        {
            Decision = false;
        }
    }
    private void LoadObject()
    {
        if(LoadFlg == false)
        {
            _Cursor = Cursor.GetComponent<RectTransform>();
            _Retry = Retry.GetComponent<Text>();
            _BacktoTitle = BacktoTitle.GetComponent<Text>();
            _Retrymenu = Retrymenu.GetComponent<RectTransform>();
            LoadFlg = true;
        }
    }

    private void CursorMove()
    {
        if (push_scene == false)
        {
            if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
            {
                if (CursorFlg == false)
                {
                    CursorFlg = true;

                    if (++MenuNumber > 1) MenuNumber = 0;   //カーソルが一番下から一番上にくるように
                }
            }
            else if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == 1))
            {
                if (CursorFlg == false)
                {
                    CursorFlg = true;
                    if (--MenuNumber < 0) MenuNumber = 1;    //カーソルが一番上から一番下にくるように

                }
            }
            else
            {
                CursorFlg = false;
            }

            switch (MenuNumber)
            {
                case 0:
                    _Retry.color = new Color(1, 1, 1, 1);
                    _BacktoTitle.color = new Color(0, 0, 0, 1);

                    _Cursor.localPosition = new Vector3(0, 15, 0);

                    if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                    {
                        if (Decision == false)
                        {
                            StartCoroutine("RetryCoroutine");
                            Decision = true;
                        }
                    }
                    break;

                case 1:
                    _Retry.color = new Color(0, 0, 0, 1);
                    _BacktoTitle.color = new Color(1, 1, 1, 1);

                    _Cursor.localPosition = new Vector3(0, -45, 0);

                    if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                    {
                        if (Decision == false)
                        {
                            StartCoroutine("BacktoTitleCoroutine");
                            Decision = true;
                        }
                    }
                    break;
            }
        }
    }
    private IEnumerator RetryCoroutine()
    {
        push_scene = true;
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード
        SceneManager.LoadScene("Game");
        push_scene = false;
        ShowMenu = false;

        time = 0;
    }

    private IEnumerator BacktoTitleCoroutine()
    {
        push_scene = true;
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード
        SceneManager.LoadScene("Title");
        push_scene = false;
        ShowMenu = false;
        time = 0;
    }


    //public static float QuartOut(float t, float totaltime, float min, float max)
    //{
    //    max -= min;
    //    t = t / totaltime - 1;
    //    return -max * (t * t * t * t - 1) + min;
    //}
}
