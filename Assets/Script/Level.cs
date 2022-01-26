using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    /*シャトルランのルール上の秒数：参照元：https://newbal.wiki.fc2.com/wiki/20m%E3%82%B7%E3%83%A3%E3%83%88%E3%83%AB%E3%83%A9%E3%83%B3%E3%81%AE%E9%80%9F%E3%81%95 */
    public float[] level_time =
    {

        9.00f,/* Level_1  Count1   ～ 7 */
        8.00f,/* Level_2  Count8   ～ 15 */
        7.58f,/* Level_3  Count16  ～ 23 */
        7.20f,/* Level_4  Count24  ～ 32 */
        6.86f,/* Level_5  Count33  ～ 41 */
        6.55f,/* Level_6  Count42  ～ 51 */
        6.26f,/* Level_7  Count52  ～ 61 */
        6.00f,/* Level_8  Count62  ～ 72 */
        5.76f,/* Level_9  Count73  ～ 83 */
        5.54f,/* Level_10 Count84  ～ 94 */
        5.33f,/* Level_11 Count95  ～ 106 */
        5.14f,/* Level_12 Count107 ～ 118 */
        4.97f,/* Level_13 Count119 ～ 131 */
        4.80f,/* Level_14 Count132 ～ 144 */
        4.65f,/* Level_15 Count145 ～ 157 */
        4.50f,/* Level_16 Count158 ～ 171 */
        4.36f,/* Level_17 Count172 ～ 185 */
        4.24f,/* Level_18 Count186 ～ 200 */
        4.11f,/* Level_19 Count201 ～ 215 */
        4.00f,/* Level_20 Count216 ～ 231 */
        3.89f,/* Level_21 Count232 ～ 247 */
        3.79f /* Level_22 Count248 ～ 263 */
        
    };
    /*シャトルランのルール上の秒数：参照元：https://newbal.wiki.fc2.com/wiki/20m%E3%82%B7%E3%83%A3%E3%83%88%E3%83%AB%E3%83%A9%E3%83%B3%E3%81%AE%E9%80%9F%E3%81%95 */

    /*往復回数との比較変数*/
    public int[] level_count =
    {
        8,   /* Level_2 */
        16,  /* Level_3 */
        24,  /* Level_4 */
        33,  /* Level_5 */
        42,  /* Level_6 */
        52,  /* Level_7 */
        62,  /* Level_8 */
        73,  /* Level_9 */
        84,  /* Level_10 */
        95,  /* Level_11 */
        107, /* Level_12 */
        119, /* Level_13 */
        132, /* Level_14 */
        145, /* Level_15 */
        158, /* Level_16 */
        172, /* Level_17 */
        186, /* Level_18 */
        201, /* Level_19 */
        216, /* Level_20 */
        232, /* Level_21 */
        248, /* Level_22 */
        260  /* 数合わせ */   
    };
    /*往復回数との比較変数*/

    ///*次の音階がなるまでのインターバル*/
    //public float[] level_interval =
    //{

    //    0f,        /*Level1*/
    //    0f,     /*Level2*/
    //    0f,      /*Level3*/
    //    0f,      /*Level4*/
    //    0f,      /*Level5*/
    //    0f,      /*Level6*/
    //    0f,      /*Level7*/
    //    0f,      /*Level8*/
    //    0f,      /*Level9*/
    //    0f,      /*Level10*/
    //    0f,      /*Level11*/
    //    0f,      /*Level12*/
    //    0f,      /*Level13*/
    //    0f,      /*Level14*/
    //    0f,      /*Level15*/
    //    0f,      /*Level16*/
    //    0f,      /*Level17*/
    //    0f,      /*Level18*/
    //    0f,      /*Level19*/
    //    0f,      /*Level20*/
    //    0f,      /*Level21*/
    //    0f,      /*Level22*/

    //};
    ///*次の音階がなるまでのインターバル*/

    /*シャトルランのシステム変数*/
    public int level = 0;               /*シャトルランのレベルアップ変数*/
    public int runcount = 0;            /*ゴール回数を記録する変数*/
    public int otetuki = 0;             /*失敗したときの変数(上限は２)*/
    public int otetuki_interval = 0;    /*往復したときにお手付きをリセットする変数*/
    public float time = 0f;             /*制限時間の変数*/
    public float timemax = 5f;          /*5秒後にスタートさせる変数*/
    public float time_interval = 0f;    /*次の音階が鳴るまでのインターバル変数*/
    public bool StartGame = true;       /*ゲーム中かどうかを入れる(プレーヤー担当の人よろしく💛)*/
    /*シャトルランのシステム変数*/

    /*audioはそのままの名前だとコンポーネントのaudioと被って隠されるとかなんとかだったから念の為変える*/
    public AudioSource audio_player;            /*シャトルラン音源を使用するオーディオソースを入れる*/
    [SerializeField] AudioClip clip;     /*シャトルラン音源*/

    /*プレーヤー情報を入れる変数*/
    public GameObject run;          /*プレーヤのオブジェクトを入れる*/
    public float playerx = 0f;      /*スタート時の位置*/
    public float maxposition = 0f;  /*ゴール時の位置*/
    public float position = 0f;     /*プレーヤが進んだ距離*/
    public float max = 20f;         /*ゴールまでの距離*/
    Vector3 playerposition;         /*プレーヤのPositionを入れる*/
    /*プレーヤー情報を入れる変数*/

    /*ゲームオーバー用変数*/
    public GameObject GameOver_flg;
    Game_Over var;
    /*ゲームオーバー用変数*/

    // Start is called before the first frame update
    void Start()
    {
        audio_player.clip = clip;
        /*動作テスト用*/
        audio_player.Play();/*再生_test用*/
        StartGame = true;
        /*動作テスト用*/

        /*スタート時のプレーヤポジションを取得*/
        playerposition = run.transform.position;
        playerx = playerposition.x;
        maxposition = playerposition.x;
        /*スタート時のプレーヤポジションを取得*/


    }

    // Update is called once per frame
    void Update()
    {
        audio_player.volume = SoundVolumu.BGMVol / 100;

        if (otetuki > 1 || Time.timeScale <= 0)
        {
            StartGame = false;
            audio_player.Pause();/*fixedupdateはtimescaleが0になると実行されないのでfixedupdateに停止処理が入ってると恐らく止まらないからこちらに移動した*/
        }
        else { StartGame = true; }/*プレーヤー担当の人ここにゲーム中のフラグを入れて*/

    }

    void FixedUpdate()
    {
        /*動作テスト*/
        //if ((int)time % 3 == 0f && otetuki < 2)
        //{
        //    runcount++;

        //}
        //time += Time.deltaTime;
        /*動作テスト*/

        /*音源が鳴っていてゲーム中のみ動作*/
        if (audio_player.isPlaying && StartGame)
        {

            /*5秒後に数える処理*/
            if (timemax < 0f)
            {

                time += Time.deltaTime;

                ///*インターバルが終わってから数える*/
                //time_interval += Time.deltaTime;
                //if (time_interval >= level_interval[level])
                //{

                //    time += Time.deltaTime;

                //}
                ///*インターバルが終わってから数える*/

            }
            else
            {
                timemax -= Time.deltaTime;
            }
            /*5秒後に数える処理*/

        }

        /*ゲーム中のみ音源が再生される*/
        if (StartGame)
        {

            audio_player.UnPause();/*ゲーム中は音源が停止されない*/
 
            /*制限時間を過ぎたら*/
            if (time >= level_time[level])
            {

                /*制限時間がすぎるまでにゴールできなかったら*/
                if (position < max && time > level_time[level])
                {

                    /*お手付きと往復回数が一緒だったら*/
                    if (otetuki_interval == otetuki)
                    {

                        otetuki++;/*お手付きプラス1*/

                    }
                    else if (otetuki_interval < otetuki)/*インターバルよりお手付きのほうが大きかったら*/
                    {

                        otetuki++;

                    }

                }

                time = 0;/*制限時間をリセット*/
                time_interval = 0;/*インターバルタイムをリセット*/
                otetuki_interval = 0;/*お手付きインターバルをリセット*/

            }
            /*制限時間を過ぎたら*/

            /*ゴール回数が一定数を超えたら*/
            if (runcount >= level_count[level] && time < level_time[level] && level < 22 && otetuki < 2)
            {

                level++;/*シャトルランのレベルを上げる*/

            }
            else if (otetuki >= 2)/*お手付きを連続で2回やったら*/
            {


                audio_player.Stop();/*音源を停止*/

                StartGame = false;/*ゲームフラグをファルスに*/

                /*ゲームオーバースクリプトを編集*/
                var = GameOver_flg.GetComponent<Game_Over>();   /*ゲームオーバースクリプトを空箱に代入*/
                var.game_over_flg = true;   /*ゲームオーバーフラグを真に変更*/

            }

        }
        else/*ゲーム途中でポーズが入ると*/
        {

            //audio_player.Pause();/*音源を一時停止*/

        }


        /*座標取得*/
        if (StartGame)/*ゲーム中のみ動作する*/
        {

            /*プレーヤのx軸を取得*/
            playerposition = run.transform.position;
            playerx = playerposition.x;

            /*プレーヤーのスタート位置と進んだ位置を比較*/
            if (maxposition > playerx)/*スタート位置よりも進んだ位置のほうが大きかったら*/
            {

                position = maxposition - playerx;/*進んだ距離に引いた値を入れる*/

            }
            else if (maxposition < playerx)/*スタート位置よりも進んだ位置のほうが小さかったら*/
            {

                position = playerx - maxposition;/*進んだ距離に引いた値を入れる*/

            }
            /*プレーヤーのスタート位置と進んだ位置を比較*/

            /*プレーヤが制限時間までにゴールしたら*/
            if (position >= max && time <= level_time[level])
            {
                position = 0;/*距離をリセット*/
                maxposition = playerposition.x;/*スタート位置にプレーヤのx軸を入れる*/
                runcount++;/*ゴールカウントプラス1*/
                otetuki_interval++;/*往復回数をプラス1*/

                /*往復回数がお手付きより大きくなったら*/
                if (otetuki_interval > otetuki)
                {

                    otetuki = 0;/*お手付きをリセット*/

                }

            }
            /*プレーヤが制限時間までにゴールしたら*/

        }
        /*座標取得*/

    }



}
