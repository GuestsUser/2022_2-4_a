using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausemenu : MonoBehaviour
{
    /*ポーズメニュー表示*/
    public GameObject _PausePanel;
    public GameObject ReStart;
    public GameObject Option;
    public GameObject ReTitle;
    bool pushFlag;
    public bool _pushFlag { get { return pushFlag; } }
    bool pushscene; //現在何のシーンなのか　ひとりモードor通信モード
    bool GameOver; //ゲームが終わっているのか続いているのか
    /**/

    /*カーソルムーブ*/
    public static int MenuNumber = 0; //メニュー番号
    public GameObject Cursor; //カーソルのポジション取得に必要
    bool CursorFlg;
    bool Decision; //決定を押したか押してないか

    RectTransform _Cursor; //Cursor動かすのに必要


    Text _ReStart; //最初から始める
    Text _Option; //オプション
    Text _ReTitle; //タイトルに戻る
    /**/

    // Start is called before the first frame update
    void Start()
    {
        _PausePanel.SetActive(false);
        GameOver = false;
        pushFlag = false;
        pushscene = false;
        Decision = false;
        CursorFlg = false; //19:42追加
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {
            LoadObject();
            CursorMove();
        }
        //Debug.Log("Updateは正常に動いています");
        Pause();
    }


    private void Pause() //ポーズメニュー表示/非表示
    {
        if (GameOver == false) //まだゲームオーバーじゃない
        {

            if (pushscene == false) //ポーズメニューからAボタンを押して決定していない
            {
                if (Time.timeScale == 0)//タイムスケールが0で
                {
                    if (Input.GetButton("B"))//Bボタンを押したら
                    {
                        if (pushFlag == false) //押されてないっていうステータスを
                        {
                            pushFlag = true; //押されたことにする
                            Time.timeScale = 1;
                            _PausePanel.SetActive(false);
                        }
                    }
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
                            
                        }
                        else
                        {
                            pushFlag = true;
                            Time.timeScale = 1;
                            _PausePanel.SetActive(false);
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
        Cursor = GameObject.Find("cursor");
        _Cursor = Cursor.GetComponent<RectTransform>();
        ReStart = GameObject.Find("Restert");
        Option = GameObject.Find("Option");
        ReTitle = GameObject.Find("ReTitle");

        _ReStart = ReStart.GetComponent<Text>();
        _Option = Option.GetComponent<Text>();
        _ReTitle = ReTitle.GetComponent<Text>();
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

                _Cursor.localPosition = new Vector3(0, 55, 0);

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

                _Cursor.localPosition = new Vector3(0, -5, 0);
                
                if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    if (Decision == false)
                    {
                        Decision = true;
                    }
                }
                break;

            case 2: //タイトルに戻る
                _ReStart.color = new Color(0, 0, 0, 1);
                _Option.color = new Color(0, 0, 0, 1);
                _ReTitle.color = new Color(1, 1, 1, 1); 

                _Cursor.localPosition = new Vector3(0, -65, 0);

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

    private IEnumerator RestartCoroutine() //シーンチェンジ用
    {
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード
        
        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
    private IEnumerator BacktoTitle() //シーンチェンジ用
    {
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード
        
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }
}
