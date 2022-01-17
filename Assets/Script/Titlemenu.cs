using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Titlemenu : MonoBehaviour
{
    GameObject Image;
    public static int MenuNumber = 0;
    RectTransform rect;

    bool pushFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
        {
            if (pushFlag == false)
            {
                pushFlag = true;
                if (++MenuNumber > 2) MenuNumber = 0;   //ポーズメニューのカーソルが一番下から一番上にくるように
            }
        }
        else if ((!Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (!Input.GetButton("A") && Input.GetAxis("Vertical2") == 1))
        {
            if (pushFlag == false)
            {
                pushFlag = true;
                if (--MenuNumber < 0) MenuNumber = 2;    //ポーズメニューのカーソルが一番上から一番下にくるように

            }
        }
        else
        {
            pushFlag = false;
        }

        switch (MenuNumber)
        {
            case 0: //モード選択の位置
                rect.localPosition = new Vector3(0, -80, 0);
                if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    //pushScene = true;
                    //StartCoroutine(RetryCoroutine());
                    
                }
                //Debug.Log("0");
                break;
            case 1: //スタート
                rect.localPosition = new Vector3(0, -150, 0);
                if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    //pushScene = true;
                    //StartCoroutine(TitleCoroutine());
                    StartCoroutine(ChangeCoroutine());

                }
                //Debug.Log("1");
                break;
            case 2: //ゲームを終了
                rect.localPosition = new Vector3(0, -220, 0);
                if (Input.GetButton("A") || (Input.GetButton("A") && Input.GetAxis("Vertical") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == 1) || (Input.GetButton("A") && Input.GetAxis("Vertical") == -1) || (Input.GetButton("A") && Input.GetAxis("Vertical2") == -1))
                {
                    //pushScene = true;
                    StartCoroutine(EndCoroutine());
                }
                //Debug.Log("2");
                break;

        }
    }

    private IEnumerator ChangeCoroutine() //シーンチェンジ用
    {
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード

        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
    private IEnumerator EndCoroutine()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Application.Quit();

    }
}
