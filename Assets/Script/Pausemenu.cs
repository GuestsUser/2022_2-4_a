using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    //イージング
    [SerializeField]
    private Easingtype type;
    private float time = 0.0f;
    private float InTime = 0.0f;
    private float easingTime = 0.8f;

    bool PanelClose;

    /*ポーズメニュー表示*/
    public GameObject _PausePanel;
    public GameObject ReStart;
    public GameObject Option;
    public GameObject ReTitle;
    bool pushFlag;
    public bool _pushFlag { get { return pushFlag; } }
    bool push_scene; //現在何のシーンなのか　ひとりモードor通信モード
    public bool _push_scene { get { return push_scene; } }
    bool GameOver; //ゲームが終わっているのか続いているのか

    RectTransform _pausepanel;
    /**/

    /*カーソルムーブ*/
    public static int MenuNumber = 0; //メニュー番号
    public int _MenuNumber { get { return MenuNumber; } }
    public GameObject Cursor; //カーソルのポジション取得に必要
    bool CursorFlg; //入力長押し対策
    public bool Decision; //決定を押したか押してないか

    RectTransform _Cursor; //Cursor動かすのに必要


    Text _ReStart; //最初から始める
    Text _Option; //オプション
    Text _ReTitle; //タイトルに戻る
    /**/

    /*フェードアウトに必要なもの*/
    public GameObject FadePanel;
    Image _FadePanel;
    float FadeOpacity;
    bool FadeFlg;
    float FadeMaxOpacity;

    bool ShowMenu;
    public bool _showMenu { get { return ShowMenu; } }
    /**/

    /*ロード処理*/
    bool LoadFlg;
    /**/

    /*オプションメニュ～に必要なもの*/
    //bool OptionFlg;
    public bool _OptionFlg;
    public int CancelCount;

    //ゲームオーバー時に必要
    public GameObject _Game_Over;
    Game_Over game_over;

    // Start is called before the first frame update
    void Start()
    {
        _PausePanel.SetActive(false);
        _pausepanel = _PausePanel.GetComponent<RectTransform>();
        GameOver = false;
        pushFlag = false;
        push_scene = false;
        Decision = false;
        CursorFlg = false; //19:42追加

        FadeOpacity = 0;
        FadeMaxOpacity = 100;
        FadeFlg = false;
        ShowMenu = false;

        LoadFlg = false;
        time = 0;
        InTime = 0;
        _pausepanel.localScale = new Vector3(0, 0, 1);
        PanelClose = false;

        _OptionFlg = false;
        _Game_Over = GameObject.Find("GameOver_flg");
        game_over = _Game_Over.GetComponent<Game_Over>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(game_over.game_over_flg);
        if (game_over.game_over_flg == false)
        {
            if (_OptionFlg == false)
            {
                //Debug.Log(time);
                EasOut();
                if (Time.timeScale == 0)
                {

                    LoadObject();



                    FadeIN();
                    if (push_scene == false)
                    {
                        CursorMove();
                    }

                }
                //Debug.Log("Updateは正常に動いています");
                Pause();
            }
        }
        
        
    }


    private void Pause() //ポーズメニュー表示/非表示
    {
        if (GameOver == false) //まだゲームオーバーじゃない
        {

            if (push_scene == false) //ポーズメニューからAボタンを押して決定していない
            {
                if (Time.timeScale == 0)//タイムスケールが0で
                {
                    if(CancelCount == 0) //オプションメニュー表示からBを押すとポーズも消えてしまう問題対策
                    {
                        if (!Input.GetButton("A") && Input.GetButton("B"))//Bボタンを押したら
                        {
                            if (pushFlag == false) //押されてないっていうステータスを
                            {
                                pushFlag = true; //押されたことにする
                                Time.timeScale = 1;
                                FadeOpacity = 0;
                                _FadePanel.color = new Color(0, 0, 0, 0);
                                ShowMenu = false;
                                FadeFlg = false;
                                PanelClose = true;


                                time = 0;
                            }
                        }
                    }
                    
                }
                else if (Time.timeScale == 1 && PanelClose == true)
                {
                    EasIn();

                }

                if (Input.GetButton("Start")) //スタートボタン入力があったら
                {

                    //Debug.Log("スタートボタンが押されました");
                    if (pushFlag == false) //押されてないっていうステータスを
                    {
                        pushFlag = true; //押したっていう事にしとくね
                        if (Time.timeScale == 1)
                        {
                            Time.timeScale = 0;
                            _PausePanel.SetActive(true);
                            InTime = 0;
                            ShowMenu = true;
                        }
                        else if(!Input.GetButton("A") && pushFlag == true && CancelCount == 0)
                        {
                            pushFlag = true;
                            Time.timeScale = 1;
                            FadeOpacity = 0;
                            _FadePanel.color = new Color(0, 0, 0, 0);
                            ShowMenu = false;
                            FadeFlg = false;
                            PanelClose = true;
                            time = 0;
                        }
                    }
                }
                else //ゲーム続行中は押してないってことにするね
                {
                    pushFlag = false;
                }
            }
        }
    }

    private void LoadObject()
    {
        if(LoadFlg == false) { 
            Cursor = GameObject.Find("cursor");
            _Cursor = Cursor.GetComponent<RectTransform>();
            ReStart = GameObject.Find("Restert");
            Option = GameObject.Find("Option");
            ReTitle = GameObject.Find("ReTitle");

            _ReStart = ReStart.GetComponent<Text>();
            _Option = Option.GetComponent<Text>();
            _ReTitle = ReTitle.GetComponent<Text>();

            FadePanel = GameObject.Find("FadePanel");
            _FadePanel = FadePanel.GetComponent<Image>();

            
            LoadFlg = true;
        }
    }
    private void CursorMove()
    {
        if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
        {
            if (CursorFlg == false)
            {
                CursorFlg = true;

                if (++MenuNumber > 2) MenuNumber = 0;   //カーソルが一番下から一番上にくるように
            }
        }
        else if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == 1))
        {
            if (CursorFlg == false)
            {
                CursorFlg = true;
                if (--MenuNumber < 0) MenuNumber = 2;    //カーソルが一番上から一番下にくるように

            }
        }
        else
        {
            CursorFlg = false;
        }
        
        switch (MenuNumber)
        {
            case 0:
                _ReStart.color = new Color(1,1,1,1);
                _Option.color = new Color(0, 0, 0, 1);
                _ReTitle.color = new Color(0,0,0,1);

                _Cursor.localPosition = new Vector3(0, 15, 0);

                if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    if(Decision == false)
                    {
                        StartCoroutine("RestartCoroutine");
                        Decision = true;
                    }
                }
                    break;

            case 1:
                _ReStart.color = new Color(0, 0, 0, 1);
                _Option.color = new Color(1, 1, 1, 1);
                _ReTitle.color = new Color(0, 0, 0, 1);

                _Cursor.localPosition = new Vector3(0, -45, 0);
                
                if (Input.GetAxis("Vertical")==0 && Input.GetAxis("Vertical2") == 0 && Input.GetButton("A"))
                {
                    if (Decision == false)
                    {
                        Decision = true;
                        _OptionFlg = true;
                        CancelCount = 1;
                    }
                }
                break;

            case 2: //タイトルに戻る
                _ReStart.color = new Color(0, 0, 0, 1);
                _Option.color = new Color(0, 0, 0, 1);
                _ReTitle.color = new Color(1, 1, 1, 1); 

                _Cursor.localPosition = new Vector3(0, -105, 0);

                if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    if (Decision == false)
                    {
                        StartCoroutine("BacktoTitle");
                        Decision = true;
                        
                    }
                }
                break;
        }
    }
    private void FadeIN()
    {
        if(ShowMenu == true)
        {

            if (FadeFlg == false)
            {
                FadeOpacity++;
            
            }
            if (FadeOpacity > FadeMaxOpacity / 4)
            {
                FadeFlg = true;
            }
        }

        _FadePanel.color = new Color(0, 0, 0, FadeOpacity / FadeMaxOpacity);
    }

    private void EasOut()
    {
        if(ShowMenu == true)
        {

            //_pausepanel.localScale = new Vector3(0, 0, 1);
            //イージング
            time += 0.333333f / 3;

            if (time < easingTime)
            {
                _pausepanel.localScale = new Vector3((Easing.ExpOut(time, easingTime, 0, 1)), ((Easing.ExpOut(time, easingTime, 0, 1))), 1);
                
            }
            else
            {
                _pausepanel.localScale = new Vector3(1, 1, 1);
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
        if (ShowMenu == false)
        {
            //Debug.Log(InTime);
            //Debug.Log(1 - (Easing.ExpOut(InTime, easingTime, 0, 1)));
            //イージング
            InTime += 0.333333f / 3;

            if (InTime < easingTime)
            {
                _pausepanel.localScale = new Vector3(1-(Easing.ExpOut(InTime, easingTime, 0, 1)), 1-(Easing.ExpOut(InTime, easingTime, 0, 1)), 1);
            }
            else
            {
                _pausepanel.localScale = new Vector3(0, 0, 1);
                PanelClose = true;
                MenuNumber = 0;
                _PausePanel.SetActive(false);
            }
        }
        else if(ShowMenu == true)
        {
            InTime = 0;
            Debug.Log("通りました");
        }
    }
    private IEnumerator RestartCoroutine() //シーンチェンジ用
    {
        push_scene = true;
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード
        
        SceneManager.LoadScene("Game");
        MenuNumber = 0;
        Time.timeScale = 1;
    }
    private IEnumerator BacktoTitle() //シーンチェンジ用
    {
        push_scene = true;
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード
        
        SceneManager.LoadScene("Title");
        MenuNumber = 0;
        Time.timeScale = 1;
    }
}
