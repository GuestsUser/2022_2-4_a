﻿using System.Collections;
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
        //オブジェクトのAlpha値を更新
        if (level.otetuki > 0)
        {
            image.enabled = true;
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
            else if (ReScroll == true)
            {
                var rect = warning.uvRect;
                rect.x = (rect.x - X_Speed * Time.unscaledDeltaTime) % 1.0f;
                warning.uvRect = rect;
            }
        }
        else if(gameover.game_over_flg == true)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = false;
        }

        ////Warning画像スクロール
        //if (Scroll == true)
        //{
        //    var rect = warning.uvRect;
        //    rect.x = (rect.x + X_Speed * Time.unscaledDeltaTime) % 1.0f;
        //    warning.uvRect = rect;
        //}
        //else if(ReScroll == true)
        //{
        //    var rect = warning.uvRect;
        //    rect.x = (rect.x - X_Speed * Time.unscaledDeltaTime) % 1.0f;
        //    warning.uvRect = rect;
        //}
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 1.0f * speed;
        color.a = 1f - Mathf.Sin(time) * 0.8f;

        return color;
    }
}