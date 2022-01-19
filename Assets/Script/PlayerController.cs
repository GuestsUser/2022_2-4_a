using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpeedGage SG;    //スピードゲージ
    public Level level;     //レベル
    public StaminaGage STG; //スタミナゲージ

    public int Move = 0;    //速度調整

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    void FixedUpdate()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbodyを取得

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
}