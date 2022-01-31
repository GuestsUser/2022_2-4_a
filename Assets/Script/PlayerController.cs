using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpeedGage SG;    //スピードゲージ
    public Level level;     //レベル
    public StaminaGage STG; //スタミナゲージ

    public int Move = 0;    //速度調整

    public bool stop_flg=false;//速くゴールした時のフラグ保持
    private int old_run_count=0;//前フレームのゴールカウント、これが増えた瞬間をゴールとみなす

    //アニメーション
    private Animation anim;
    public AnimationClip Idle;  //その場足踏み
    public AnimationClip Dizzy; //疲れたときの歩き
    public AnimationClip Run;   //走り
    //アニメーションフラグ
    private bool anim_idle_flg = false;
    private bool anim_run_flg = false;
    private bool anim_dizzy_flg = false;

    //回転
    private bool rotate_flg = false;


    private void Start()
    {
        anim = GetComponent<Animation>();
        StartCoroutine("Anim_Idle");
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
            //ランカウントが増えた&お手付きしてないならゴール待機
            if (old_run_count != level.runcount && level.otetuki<1)
            {
                if (level.runcount == 0 || level.runcount % 2 == 0)
                {
                    if (!rotate_flg)
                    {
                        rotate_flg = true;
                        StartCoroutine("LeftMove");
                    }
                }
                else
                {
                    if (!rotate_flg)
                    {
                       rotate_flg = true;
                       StartCoroutine("RightMove");
                    }
                }
                if (!anim_idle_flg)
                {
                    anim_idle_flg = true;
                    anim_dizzy_flg = false;
                    anim_run_flg = false;
                    StartCoroutine("Anim_Idle");
                }
                stop_flg = true;
                old_run_count = level.runcount;
                return;
            }

            //お手付きしているとき&欄カウントが増えたとき
            if(level.otetuki == 1 && old_run_count != level.runcount)
            {
                if (level.runcount == 0 || level.runcount % 2 == 0)
                {
                    if (!rotate_flg)
                    {
                        rotate_flg = true;
                        StartCoroutine("LeftMove");
                    }
                }
                else
                {
                    if (!rotate_flg)
                    {
                        rotate_flg = true;
                        StartCoroutine("RightMove");
                    }
                }
            }

                if (level.timemax > 0) { return; }// カウントダウン中は移動しない

            if (level.runcount == 0 || level.runcount % 2 == 0)
            {
                if (STG.point == 0)
                {
                    if (!anim_dizzy_flg)
                    {
                        anim_dizzy_flg = true;
                        anim_idle_flg = false;
                        anim_run_flg = false;
                        StartCoroutine("Anim_Dizzy");
                    }
                    Vector3 force = new Vector3(Move / 2.0f * SG.GageAmount, 0.0f, 0.0f);
                    rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
                    Debug.Log(force);
                }
                else
                {
                    if (!anim_run_flg)
                    {
                        anim_run_flg = true;
                        anim_idle_flg = false;
                        anim_dizzy_flg = false;
                        StartCoroutine("Anim_Run");
                    }
                    Vector3 force = new Vector3(Move * SG.GageAmount + 1.0f, 0.0f, 0.0f);    // 力を設定
                    rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
                }
            }
            else
            {
                if (STG.point == 0)
                {
                    if (!anim_dizzy_flg)
                    {
                        anim_dizzy_flg = true;
                        anim_idle_flg = false;
                        anim_run_flg = false;
                        StartCoroutine("Anim_Dizzy");
                    }
                    Vector3 force = new Vector3(-Move / 2.0f * SG.GageAmount, 0.0f, 0.0f);
                    rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
                    Debug.Log(force);
                }
                else
                {
                    if (!anim_run_flg)
                    {
                        anim_run_flg = true;
                        anim_idle_flg = false;
                        anim_dizzy_flg = false;
                        StartCoroutine("Anim_Run");
                    }
                    Vector3 force = new Vector3(-Move * SG.GageAmount - 1.0f, 0.0f, 0.0f);    // 力を設定
                    rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
                }
            }
        }
        old_run_count = level.runcount;
    }





    //止まっているとき
    IEnumerator Anim_Idle()
    {
        anim.Stop();

        yield return null;

        anim.clip = Idle;

        yield return null;

        anim.Play("Idle");

        yield return null;
    }

   //走っているとき
    IEnumerator Anim_Run()
    {
        anim.Stop();

        yield return null;

        anim.clip = Run;

        yield return null;

        anim.Play("Run");

        yield return null;
    }

    //疲れているとき
    IEnumerator Anim_Dizzy()
    {
        anim.Stop();

        yield return null;

        anim.clip = Dizzy;

        yield return null;

        anim.Play("Dizzy");

        yield return null;
    }

    //右にゆっくり回転して90度でストップ
    IEnumerator RightMove()
    {
        for(int turn = 0; turn < 60; turn++)
        {
            transform.Rotate(0, 3, 0);
            yield return new WaitForSeconds(0.001f);
        }
        rotate_flg = false;
    }

    //左にゆっくり回転して90度でストップ
    IEnumerator LeftMove()
    {
        for (int turn = 0; turn < 60; turn++)
        {
            transform.Rotate(0,-3, 0);
            yield return new WaitForSeconds(0.001f);
        }
        rotate_flg = false;
    }
}