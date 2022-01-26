using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour
{
    public float speed = 1.0f;  //点滅させるスピード
    public Level level;

    private RawImage image;     //RawImageオブジェクト
    private float time;         //時間

    public bool Blinking;       //点滅フラグ
    public bool Scroll;         //Warning横スクロールフラグ

    private const float X_Speed = 0.04f;
    public RawImage warning;

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
        }
        else
        {
            image.enabled = false;
        }

        //Warning画像スクロール
        if (Scroll == true)
        {
            var rect = warning.uvRect;
            rect.x = (rect.x + X_Speed * Time.unscaledDeltaTime) % 1.0f;
            warning.uvRect = rect;
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