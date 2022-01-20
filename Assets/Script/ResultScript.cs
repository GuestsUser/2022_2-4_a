using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultScript : MonoBehaviour
{
    /*リトライメニューに必要なもの*/
    public GameObject Retrymenu;
    public GameObject Retry;
    public GameObject BacktoTitle;

    bool pushFlg;
    public bool _pushFlg { get{ return pushFlg; } }

    bool PushScene;
    bool ShowMenu; //メニュー表示フラグ
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

    /*フェードインに必要なもの*/
    public GameObject FadePanel;
    Image _FadePanel;
    float FadeOpacity;
    bool FadeFlg;
    float FadeMaxOpacity;
    /**/

    bool push_scene;

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
    }

    // Update is called once per frame
    void Update()
    {
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
        else
        {
            
        }
        _FadePanel.color = new Color(0, 0, 0, FadeOpacity / FadeMaxOpacity);
    }
    private void _ShowMenu()
    {
        if (ShowMenu == true)
        {
            CursorMove();

            /*↓リザルト画面をみたいと思う人が多ければ採用*/
            //if (Input.GetButton("B"))
            //{
            //    ShowMenu = false;
            //    Retrymenu.SetActive(false);
            //}
            
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
        //Cursor = GameObject.Find("Cursor");
        _Cursor = Cursor.GetComponent<RectTransform>();

        //Retry = GameObject.Find("Retry");
        //BacktoTitle = GameObject.Find("BacktoTitle");

        _Retry = Retry.GetComponent<Text>();
        _BacktoTitle = BacktoTitle.GetComponent<Text>();

        
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
    //private void Finalize()
    //{
    //    FadeFlg = false;
    //    Opacity = 0;
    //}
    private IEnumerator RetryCoroutine()
    {
        push_scene = true;
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード
        SceneManager.LoadScene("Game");
        push_scene = false;
        //Decision = false;
    }

    private IEnumerator BacktoTitleCoroutine()
    {
        push_scene = true;
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード
        SceneManager.LoadScene("Title");
        push_scene = false;
        //Decision = false;
    }
}
