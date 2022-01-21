using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpeedGage SG;    //スピードゲージ
    public Level level;     //レベル
    public StaminaGage STG; //スタミナゲージ

    public int Move = 0;    //速度調整

    private bool stop_flg=false;//速くゴールした時のフラグ保持
    private int old_run_count=0;//前フレームのゴールカウント、これが増えた瞬間をゴールとみなす

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbodyを取得

        if (stop_flg)
        {
            if( level.time==0) { stop_flg = false; }//時間が更新されたら再開
        }
        else
        {
            if (old_run_count != level.runcount && level.otetuki<1) { stop_flg = true; old_run_count = level.runcount; return; }//ランカウントが増えた&お手付きしてないならゴール待機
            if (level.timemax > 0) { return; }// カウントダウン中は移動しない
            if (level.runcount == 0 || level.runcount % 2 == 0)
            {
                if (STG.point == 0)
                {
                    Vector3 force = new Vector3(Move / 2.0f * SG.GageAmount, 0.0f, 0.0f);
                    rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
                    Debug.Log(force);
                }
                else
                {
                    Vector3 force = new Vector3(Move * SG.GageAmount, 0.0f, 0.0f);    // 力を設定
                    rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
                }
            }
            else
            {
                if (STG.point == 0)
                {
                    Vector3 force = new Vector3(-Move / 2.0f * SG.GageAmount, 0.0f, 0.0f);
                    rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
                    Debug.Log(force);
                }
                else
                {
                    Vector3 force = new Vector3(-Move * SG.GageAmount, 0.0f, 0.0f);    // 力を設定
                    rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
                }
            }
        }

        
        old_run_count = level.runcount;
    }
}