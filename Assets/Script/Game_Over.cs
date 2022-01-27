using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    public int miss_count;
    public bool miss;
    public bool game_over;  //ゲームオーバーフラグ
    
    void Start()
    {
        miss_count = 0;

        miss = false;
        game_over = false;  //フラグの初期化
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Joystick1Button0))
        {
            miss = true;
            if (miss)
            {
                if (miss_count >= 1)
                {
                    game_over = true;
                }
                miss_count++;
                miss = false;
            }
        }

            if (game_over)      //ゲームオーバーになったら↓
            {
            Debug.Log("リザルトに遷移");
                //SceneManager.LoadScene("Result");
            }
    }
}
