using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlushController : MonoBehaviour
{
    public float speed = 1.0f;  //点滅させるスピード
    public Level level;

    private Image image;        //Imageオブジェクト
    private float time;         //時間

    void Start()
    {
        //アタッチしてるオブジェクトを判別
        if (this.gameObject.GetComponent<Image>())
        {
            image = this.gameObject.GetComponent<Image>();
        }
    }

    void Update()
    {
        //オブジェクトのAlpha値を更新
        if (level.otetuki > 0)
        {
            image.enabled = true;
            image.color = GetAlphaColor(image.color);
        }
        else
        {
            image.enabled = false;
        }
       
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time) * 0.5f;

        return color;
    }
}