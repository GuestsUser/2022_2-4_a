using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaGage : MonoBehaviour
{
    [SerializeField] private float max_gage=100;//ゲージ最大量、これを増やしてもゲージは伸びない、減るのが遅くなる事で大容量化する
    [SerializeField] private float pinch_timing;//ピンチ演出に入るタイミング
    [SerializeField] private float pinch_pitch;//ピンチ演出の点灯間隔
    private ulong count;
    public float point { set; get; }//ゲージ現在量、setをpublic化するのは少し気が引ける……

    //扱う画像集
    [SerializeField] private GameObject normal;//ゲージ残量
    [SerializeField] private GameObject pinch;//残量低下時演出用、点滅するのだがこの時マテリアルの透明度を変更するためdefaultmaterialだとそれを使う全てのオブジェクトに変更が適用されてしまうので専用マテリアルを用意した
    [SerializeField] private GameObject damage;//ゲージ減少用、残量画像に模様やグラデーションが付いてるとサイズ変更で減少させる方法だと模様が一緒に縮小されるので逆方向からこの画像で残量を隠す方法にする
    // Start is called before the first frame update
    void Start()
    {
        point = max_gage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        
    }

    IEnumerator Damage() 
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



    IEnumerator Pinch() 
    {
        float ratio = point / max_gage;//現在ポイントの最大値に対する割合がそのままダメージゲージ拡大率になる
        if (ratio <= pinch_timing)
        {
            normal.gameObject.SetActive(false);
            //pinch.gameObject.GetComponent<Material>().
        }
        yield return null;
    }


    IEnumerator TimeStop()
    {
        do { yield return null; } while (Time.timeScale == 0);
    }
}
