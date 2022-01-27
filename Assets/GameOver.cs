using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject Animation_flg;
    Game_Over_Animation var;

    public bool game_over_flg;  //ゲームオーバーフラグ
    
    void Start()
    {
        game_over_flg = false;  //フラグの初期化
    }

    void Update()
    {
        var = Animation_flg.GetComponent<Game_Over_Animation>();   /*ゲームオーバースクリプトを空箱に代入*/
        if (game_over_flg)      //ゲームオーバーになったら↓
        {
            //Debug.Log("リザルトに遷移"); // デバッグ用
            var.isFadeIn = true;
            game_over_flg = false;
        }

        if (var.next_scene == true)
        {
            SceneManager.LoadScene("Result");
        }
    }
}
