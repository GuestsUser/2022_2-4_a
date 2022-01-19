using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpeedGage SG;
    public Level level;
    public int Move = 0;

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
            Vector3 force = new Vector3(Move * SG.GageAmount, 0.0f, 0.0f);    // 力を設定
            rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
        }
        else
        {
            Vector3 force = new Vector3(-Move * SG.GageAmount, 0.0f, 0.0f);    // 力を設定
            rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
        }
    }
}