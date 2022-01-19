using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpeedGage SG;
    public int Move = 0;
    
    void FixedUpdate()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbodyを取得
        Vector3 force = new Vector3(Move* SG.GageAmount, 0.0f, 0.0f);    // 力を設定
        rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
    }
}