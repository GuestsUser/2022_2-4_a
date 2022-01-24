using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject stamina;
    [SerializeField] private GameObject speed;


    private Vector3 old_pos;
    // Start is called before the first frame update
    void Start()
    {
        old_pos = gameObject.transform.position;
        StartCoroutine("StaminaControl");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator StaminaControl()
    {
        float healing_time=1.0f;//回復が始まる秒数
        //float healing_rate=stamina.GetComponent<StaminaGage>().max_gage/3.0f;//1秒間で回復する値、この例なら3秒で最大値に達する
        float del_limit = 10.0f;//連打維持状態がこの時間に達する事でスタミナがすべて減る、秒指定
        float count = 0;//時間経過共通カウント

        float healing_wait = 0f;
        float stamina_max = stamina.GetComponent<StaminaGage>().max_gage;
        float point = 90 / del_limit * count;
        while (true)
        {
            //float ini_point = stamina.GetComponent<StaminaGage>().point;//連打開始前のスタミナ
            //while (speed.GetComponent<SpeedGage>().RendaCount > 0)
            //{
            //    float mul = Mathf.Cos(point * Mathf.Deg2Rad);//cosカーブを使う事により時間が経過するほど加速度的に減少してゆく
            //    if (point >= 90) { stamina.GetComponent<StaminaGage>().point = 0; }//90より大きい状態で続けてしまうとやがて回復に転じるので強制0
            //    else { stamina.GetComponent<StaminaGage>().point = ini_point * mul; }//このループ突入前に取ったスタミナ値にcosカーブを掛けて減少後の値を出した
            //    count += Time.deltaTime;
            //    yield return StartCoroutine("TimeStop");//時間停止中待機の命令で停止中はスタミナを操作しない
            //}
            //count = 0;//時間リセット
            //while (speed.GetComponent<SpeedGage>().RendaCount <= 0)//連打されてないと回復するシステム、どうするかは後々決定
            //{
            //    if (count > healing_time) { stamina.GetComponent<StaminaGage>().point += healing_rate * Time.deltaTime; }//非連打時間が基準を超えた場合回復開始
            //    count += Time.deltaTime;
            //    yield return StartCoroutine("TimeStop");//時間停止中待機の命令で停止中はスタミナを操作しない
            //}
            //count = 0;//時間リセット


            if (speed.GetComponent<SpeedGage>().RendaCount > 0 && old_pos != gameObject.transform.position)//位置を止められているとスタミナも減らない
            {
                healing_wait = 0;
                if (count < del_limit) { count += Time.deltaTime; } 
                else { count = del_limit; }
            }
            else
            {
                if (count > 0)
                {
                    if (healing_wait > healing_time) { count -= Time.deltaTime; }//cosなので0に近づけると回復
                    else { healing_wait += Time.deltaTime; }
                }
                else { count = 0; }
            }

            point = 90 / del_limit * count;//del_limitにcountが達する事で90となりcos(90)=0となり全てのポイントが破棄されるまでを滑らかに表現できる
            float mul = Mathf.Cos(point * Mathf.Deg2Rad);//cosカーブを使う事により時間が経過するほど加速度的に減少してゆく
            stamina.GetComponent<StaminaGage>().point = stamina_max * mul;
            old_pos = gameObject.transform.position;
            yield return StartCoroutine("TimeStop");//時間停止中待機の命令で停止中はスタミナを操作しない
        }
    }
    IEnumerator TimeStop()
    {
        do { yield return null; } while (Time.timeScale == 0);
    }
}
