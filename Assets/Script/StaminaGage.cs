using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaGage : MonoBehaviour
{
    [SerializeField] private float MaxGage;//ゲージ最大量、これを増やしてもゲージは伸びない、減るのが遅くなる事で大容量化する
    [SerializeField] private float PinchTiming;//ゲージ残量がこの値をゲージ最大に掛けた値以下になったらピンチ演出開始
    [SerializeField] private float pinch_pitch;//ピンチ演出の点灯間隔
    float _point; //ゲージ現在量、setをpublic化するのは少し気が引ける……

    public float max_gage { get { return MaxGage; } }//プロパティ名と変数名が逆転してる気もするが変数として外部から使うならこっちのが自然な気がする……
    public float pinch_timing { get { return PinchTiming; } }
    
    public float point //ゲージ現在量、setをpublic化するのは少し気が引ける……
    { 
        set //max_gageより大きかったり0より小さくならないよう調整する
        { 
            if (value >= max_gage) { _point = max_gage; return; }
            if (value <= 0) { _point = 0; return; }
            _point = value;//問題ない場合だけここにたどり着く
        }
        get { return _point; }
    }
    [SerializeField] private Camera cam;//常にカメラの方向を向くシステム用

    //扱う画像集
    [SerializeField] private GameObject normal;//ゲージ残量
    [SerializeField] private GameObject pinch;//残量低下時演出用、点滅するのだがこの時マテリアルの透明度を変更するためdefaultmaterialだとそれを使う全てのオブジェクトに変更が適用されてしまうので専用マテリアルを用意した
    [SerializeField] private GameObject damage;//ゲージ減少用、残量画像に模様やグラデーションが付いてるとサイズ変更で減少させる方法だと模様が一緒に縮小されるので逆方向からこの画像で残量を隠す方法にする
    // Start is called before the first frame update
    void Start()
    {
        point = max_gage;
        StartCoroutine("Damage");
        StartCoroutine("Pinch");
    }

    // Update is called once per frame
    void Update()
    {

        this.gameObject.transform.rotation= cam.gameObject.transform.rotation;//カメラの方を向く
        //デバッグ用、最終的に削除する
        //if (Input.GetKey(KeyCode.Joystick1Button0)) { point += 2; }
        //if (Input.GetKey(KeyCode.Joystick1Button1)) { point -= 2; }
    }
    IEnumerator Damage() //ゲージ減少システム
    {
        while (true)
        {
            float ratio = point / max_gage;//現在ポイントの最大値に対する割合がそのままダメージゲージ拡大率になる
            Vector3 scale = damage.gameObject.transform.localScale;
            scale.x = 1 - ratio;//ダメージはポイント減少で伸びる
            damage.gameObject.transform.localScale = scale;

            yield return StartCoroutine("TimeStop");
        }
    }



    IEnumerator Pinch() //ピンチ演出
    {
        ulong count=0;
        Color32 clear = new Color32(255, 255, 255, 156);
        Color32 origin = pinch.gameObject.GetComponent<Renderer>().material.color;
        while (true)
        {
            while (point > max_gage * pinch_timing) { yield return StartCoroutine("TimeStop"); }//演出開始まで待機

            normal.gameObject.SetActive(false);//通常ゲージ不可視化
            count = 0;

            while (point <= max_gage * pinch_timing)//回復なんかで演出タイミングを超えるまで続ける
            {
                if (count%pinch_pitch==0)//pitchのタイミングで透明度交換
                {
                    if (Mathf.Floor(count/pinch_pitch)%2==0) { pinch.gameObject.GetComponent<Renderer>().material.color = origin; }
                    else { pinch.gameObject.GetComponent<Renderer>().material.color = clear; }
                }
                count++;
                yield return StartCoroutine("TimeStop"); 
            }
            normal.gameObject.SetActive(true);//通常ゲージ可視化

        }
        
    }


    IEnumerator TimeStop()
    {
        do { yield return null; } while (Time.timeScale == 0);
    }
}
