using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Titlemenu : MonoBehaviour
{
    

    public GameObject Mode1; //ひとりモード
    public GameObject Mode2; //通信対戦
    public GameObject Arrow; //モード選択の矢印
    public GameObject _Start; //はじめる(UI)
    public GameObject _End;   //おわる(UI)

    

    public Animator ArrowAnim;
    Animation _ArrowAnim;

    public static int MenuNumber = 0;
    public int _MenuNumber { get{ return MenuNumber; } }
    //例:public Vector3 Center { get { return _center; } }
    public static int ModeNumber = 0;
    RectTransform rect;
    RectTransform Mode1rect; //ひとりモードテキスト
    public RectTransform _Mode1rect { get { return Mode1rect; } }
    RectTransform Mode2rect; //通信モードテキスト
    public RectTransform _Mode2rect { get { return Mode2rect; } }

    //float X1 = 0; //ひとりモード用
    //float X2 = 375; //通信モード用

    private Vector3[] Goal = new Vector3[3];

    bool pushFlag = false; //縦用(押した判定)
    bool pushFlag2 = false; //横用(押した判定)
    public bool _pushFlag2 { get { return pushFlag2; } } //他スクリプトで参照する用
    bool plusFlag = false; //右横スクロール用
    bool minusFlag = false; //左横スクロール用

    bool Decision; //決定を押した
    public bool _Decision {get { return Decision; } }
    public bool OptionFlg;

    /*色変える時に必要*/
    Text _Mode1;
    Text _Mode2;
    Text _start;
    Text _end;
    /*色変える時に必要*/

    TitleOption _option;

    // Start is called before the first frame update
    void Start()
    {

        _Mode1 = Mode1.GetComponent<Text>();
        _Mode2 = Mode2.GetComponent<Text>();
        _start = _Start.GetComponent<Text>();
        _end = _End.GetComponent<Text>();



        ArrowAnim = Arrow.GetComponent<Animator>();
        _ArrowAnim = Arrow.GetComponent<Animation>();
        rect = GetComponent<RectTransform>();

        Mode1rect = Mode1.GetComponent<RectTransform>();
        Mode2rect = Mode2.GetComponent<RectTransform>();
        Goal[0] = new Vector3(-375,0 ,0);
        Goal[1] = new Vector3(0, 0, 0);
        Goal[2] = new Vector3(375, 0, 0);

        Mode1rect.localPosition = new Vector3(0, 0, 0);
        Mode2rect.localPosition = new Vector3(-375, 0, 0);

        Decision = false;
        OptionFlg = false;


        _option = GetComponent<TitleOption>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ModeNumber);
        if (Decision == false)
        {
            if(OptionFlg == false)
            {
                CursorMove();
            }
            
        }
        
    }

    private void CursorMove()
    {
        if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
        {
            if (pushFlag == false)
            {
                pushFlag = true;

                if (++MenuNumber > 2) MenuNumber = 0;   //カーソルが一番下から一番上にくるように
            }
        }
        else if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == 1))
        {
            if (pushFlag == false)
            {
                pushFlag = true;
                if (--MenuNumber < 0) MenuNumber = 2;    //カーソルが一番上から一番下にくるように

            }
        }
        else
        {
            pushFlag = false;
        }

        MenuNumberManager();

        switch (ModeNumber)
        {
            case 0: //ひとりモード
                if (plusFlag == true) //1が右、2が左→1が左、2が右
                {
                    Mode2rect.localPosition = Vector3.Lerp(Mode2rect.localPosition, Goal[2], 8f * Time.deltaTime);
                    Mode1rect.localPosition = Vector3.Lerp(Mode1rect.localPosition, Goal[1], 8f * Time.deltaTime);

                }
                if (minusFlag == true) //1が右、2が左→1が左、2が右
                {
                    Mode2rect.localPosition = Vector3.Lerp(Mode2rect.localPosition, Goal[0], 8f * Time.deltaTime);
                    Mode1rect.localPosition = Vector3.Lerp(Mode1rect.localPosition, Goal[1], 8f * Time.deltaTime);

                }

                break;
            case 1: //通信対戦モード
                if (plusFlag == true)
                {

                    Mode1rect.localPosition = Vector3.Lerp(Mode1rect.localPosition, Goal[2], 8f * Time.deltaTime);
                    Mode2rect.localPosition = Vector3.Lerp(Mode2rect.localPosition, Goal[1], 8f * Time.deltaTime);
                }
                if (minusFlag == true)
                {

                    Mode1rect.localPosition = Vector3.Lerp(Mode1rect.localPosition, Goal[0], 8f * Time.deltaTime);
                    Mode2rect.localPosition = Vector3.Lerp(Mode2rect.localPosition, Goal[1], 8f * Time.deltaTime);
                }
                break;
        }
    }
    private void Side_Scroll()
    {
        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal") == 1 || Input.GetAxis("Horizontal2") == 1) //右
        {
            if (pushFlag2 == false)
            {
                pushFlag2 = true;
                plusFlag = true;
                if (minusFlag == true)
                {
                    minusFlag = false;
                }
                Debug.Log("右に入力されました");
                if (++ModeNumber > 1) ModeNumber = 0;
                if (ModeNumber == 1)
                {
                    Mode2rect.localPosition = new Vector3(-375, 0, 0);//左から登場させるため左に移動させる
                }
                else if (ModeNumber == 0)
                {
                    Mode1rect.localPosition = new Vector3(-375, 0, 0);//左から登場させるため左に移動させる
                }
            }

        }
        else if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetAxis("Horizontal") == -1 || Input.GetAxis("Horizontal2") == -1) //左
        {
            if (pushFlag2 == false)
            {
                pushFlag2 = true;
                minusFlag = true;
                if (plusFlag == true)
                {
                    plusFlag = false;
                }
                Debug.Log("左に入力されました");
                if (--ModeNumber < 0) ModeNumber = 1;
                if (ModeNumber == 1)
                {
                    Mode2rect.localPosition = new Vector3(375, 0, 0);//左から登場させるため左に移動させる
                }
                else if (ModeNumber == 0)
                {
                    Mode1rect.localPosition = new Vector3(375, 0, 0);//左から登場させるため左に移動させる
                }
            }

        }
        else
        {
            pushFlag2 = false;
        }
    }
    private void MenuNumberManager()
    {
        switch (MenuNumber)
        {
            case 0: //モード選択の位置
                _start.color = new Color(0, 0, 0, 1);
                _end.color = new Color(0, 0, 0, 1);

                _Mode1.color = new Color(1, 1, 1, 1);
                _Mode2.color = new Color(1, 1, 1, 1);

                rect.localPosition = new Vector3(0, -80, 0);
                arrowanim();

                /*モード選択*/

                //横スクロール
                Side_Scroll(); //モード選択の横スクロール処理

                /*モード選択*/

                //memo 左xPos:-375 真ん中:xPos0 右xPos:375
                if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    //pushScene = true;
                    StartCoroutine(ChangeCoroutine());

                }
                break;
            case 1: //スタート
                _start.color = new Color(1, 1, 1, 1);
                _end.color = new Color(0, 0, 0, 1);

                _Mode1.color = new Color(0, 0, 0, 1);
                _Mode2.color = new Color(0, 0, 0, 1);

                rect.localPosition = new Vector3(0, -150, 0);
                if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Vertical2") == 0 && Input.GetButton("A"))
                {
                    if (pushFlag == false)
                    {
                        OptionFlg = true;
                        _option.ShowMenu = true;

                        pushFlag = true;
                    }
                        
                }
                break;
            case 2: //ゲームを終了
                _end.color = new Color(1, 1, 1, 1);
                _start.color = new Color(0, 0, 0, 1);

                _Mode1.color = new Color(0, 0, 0, 1);
                _Mode2.color = new Color(0, 0, 0, 1);

                rect.localPosition = new Vector3(0, -220, 0);
                if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    StartCoroutine(EndCoroutine());
                }
                break;

        }
    }

    private IEnumerator ChangeCoroutine() //シーンチェンジ用
    {
        Decision = true;
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード

        switch (ModeNumber)
        {
            case 0:
                SceneManager.LoadScene("Game");
                break;
            case 1:
                SceneManager.LoadScene("Game");//とりあえず同じシーンにしているが別モードが実装出来たら変える
                break;
        }
        SceneManager.LoadScene("Game");
        MenuNumber = 0;
        ModeNumber = 0;
    }
    private IEnumerator EndCoroutine()
    {
        Decision = true;
        yield return new WaitForSecondsRealtime(1.5f);
        Application.Quit();
    }
    private void arrowanim()
    {
        ArrowAnim.SetTrigger("ArrowMove");
    }
}
