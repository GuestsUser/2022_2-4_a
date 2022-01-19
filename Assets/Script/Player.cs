using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject stamina;
    [SerializeField] private GameObject speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("StaminaControl");
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator StaminaControl()//未完成
    {
        float no_hit_count=0;//連打が切れてからの秒数を記録する
        float healing_time=3.0f;//回復が始まる秒数
        float healing_rate=stamina.GetComponent<StaminaGage>().max_gage/7;//1秒間で回復する値、この例なら7秒で最大値に達する

        float repeat_count=0f;//連打状態持続時間
        float stamina_sub_limit_time = 48.0f;//連打維持状態がこの時間に達する事でスタミナ減少値が最大となる
        float stamina_sub = stamina.GetComponent<StaminaGage>().max_gage;//スタミナ減少値マックス

        while (true)
        {
            int count=speed.GetComponent<SpeedGage>().RendaCount;
            if (count > 0) 
            {
                if (repeat_count > stamina_sub_limit_time) { stamina.GetComponent<StaminaGage>().point -= stamina_sub; }
                else { stamina.GetComponent<StaminaGage>().point -= stamina_sub * repeat_count ; }
                no_hit_count = 0;
                repeat_count += Time.deltaTime;
            }
            else
            {//連打されてないと回復するシステム、どうするかは後々決定
                if (no_hit_count > healing_time) { stamina.GetComponent<StaminaGage>().point += healing_rate * Time.deltaTime; }//非連打時間が基準を超えた場合回復開始
                repeat_count = 0;
                no_hit_count += Time.deltaTime;
            }
            yield return StartCoroutine("TimeStop");//時間停止中待機の命令で停止中はスタミナを操作しない
        }
    }
    IEnumerator TimeStop()
    {
        do { yield return null; } while (Time.timeScale == 0);
    }
}
