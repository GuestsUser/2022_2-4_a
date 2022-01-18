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
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }

    IEnumerator StaminaControl()
    {
        ulong hit_count=0;
        float limit = 0.3f ;

        while(true)
        {
            if (Input.GetKey(KeyCode.Joystick1Button0)) { ; }

            yield return StartCoroutine("TimeStop");//時間停止中待機の命令で停止中はスタミナを操作しない
        }
    }
    IEnumerator TimeStop()
    {
        do { yield return null; } while (Time.timeScale == 0);
    }
}
