using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpeedGage SG;

    void FixedUpdate()
    {
        Rigidbody rb = this.GetComponent<Rigidbody>();  // rigidbodyを取得
        Vector3 force = new Vector3(1.5f * SG.GageAmount, 0.0f, 0.0f);    // 力を設定
        rb.AddForce(force - rb.velocity, ForceMode.Impulse);  // 力を加える
    }
}