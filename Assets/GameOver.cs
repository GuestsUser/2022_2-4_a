using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_Over : MonoBehaviour
{
    public bool game_over_flg;  //ゲームオーバーフラグ
    
    void Start()
    {
        game_over_flg = false;  //フラグの初期化
    }

    void Update()
    {
        if (game_over_flg)      //ゲームオーバーになったら↓
        {
            //Debug.Log("リザルトに遷移"); // デバッグ用
            game_over_flg = false;
            SceneManager.LoadScene("Result");
        }
    }
}
