using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour
{  
    private float time;         //点滅時間
    private const float X_Speed = 0.04f;    //横スクロールスピード

    public bool Blinking;       //点滅フラグ
    public bool Scroll;         //Warning横スクロールフラグ
    public bool ReScroll;       //Warning逆横スクロールフラグ
    public float speed = 1.0f;  //点滅させるスピード

    //参照
    public Level level;         //レベル
    public Game_Over gameover;  //ゲームオーバー
    public RawImage warning;    //スクロールオブジェクト
    private RawImage image;     //点滅オブジェクト

    void Start()
    {
        //アタッチしてるオブジェクトを判別
        if (this.gameObject.GetComponent<RawImage>())
        {
            image = this.gameObject.GetComponent<RawImage>();
        }
    }

    void Update()
    {
        //お手付き1回したら
        if (level.otetuki == 1)
        {
            //Warning表示
            image.enabled = true;

            //点滅フラグがtrueなら
            if (Blinking == true)
            {
                image.color = GetAlphaColor(image.color);
            }

            //Warning画像スクロール
            if (Scroll == true)
            {
                var rect = warning.uvRect;
                rect.x = (rect.x + X_Speed * Time.unscaledDeltaTime) % 1.0f;
                warning.uvRect = rect;
            }
            //Warning画像逆スクロール
            else if (ReScroll == true)
            {
                var rect = warning.uvRect;
                rect.x = (rect.x - X_Speed * Time.unscaledDeltaTime) % 1.0f;
                warning.uvRect = rect;
            }
        }
        //お手付きが0か1より大きければ
        else
        {
            //Warning非表示
            image.enabled = false;
        }
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 1.0f * speed;
        color.a = 1f - Mathf.Sin(time) * 0.8f;

        return color;
    }
}