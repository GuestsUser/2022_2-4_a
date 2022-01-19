using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    public int miss_count;
    public bool miss;
    public bool game_over_flg;  //ゲームオーバーフラグ
    
    void Start()
    {
        miss_count = 0;

        miss = false;
        game_over_flg = false;  //フラグの初期化
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            miss = true;
            if (miss)
            {
                miss = false;
                if (miss_count >= 1)
                {
                    game_over_flg = true;
                }
                miss_count++;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {
            miss_count = 0;
        }

        if (game_over_flg)      //ゲームオーバーになったら↓
        {
        Debug.Log("リザルトに遷移");
            game_over_flg = false;
            //SceneManager.LoadScene("Result");
        }
    }
}
