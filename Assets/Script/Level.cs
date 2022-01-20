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

    public int level = 0;           /*レベルアップ変数*/
    public int runcount = 0;        /*往復回数を記録する変数*/
    public int otetuki = 0;         /*失敗したときの変数(上限は２)*/
    public float time = 0f;         /*音源を再生してから数える変数*/
    public float timemax = 5f;
    public bool StartGame = true;  /*ゲーム中かどうかを入れる(プレーヤー担当の人よろしく💛)*/

    public AudioSource audio;            /*シャトルラン音源を使用するオーディオソースを入れる*/
    [SerializeField] AudioClip clip;     /*シャトルラン音源*/

    /*プレーヤー情報を入れる変数*/
    public GameObject run = GameObject.Find("player");
    public float playerx = 0f;
    public float maxposition = 0f;
    public float position = 0f;
    public float max = 19f;
    Vector3 playerposition;
    /*プレーヤー情報を入れる変数*/

    // Start is called before the first frame update
    void Start()
    {
        audio.clip = clip;
        /*動作テスト用*/
        audio.Play();/*再生_test用*/
        StartGame = true;
        /*動作テスト用*/

        playerposition = run.transform.position;
        playerx = playerposition.x;
        maxposition = playerposition.x;
        max = 19f;

    }

    // Update is called once per frame
    void Update()
    {
        //if (!StartGame)
        //{
        //    StartGame = true;
        //}
        //StartGame = true;/*プレーヤー担当の人ここにゲーム中のフラグを入れて*/

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

        if (audio.isPlaying && StartGame)
        {

            if (timemax < 0f)
            {

                time += Time.deltaTime;

            }
            else
            {
                timemax -= Time.deltaTime;
            }

        }


        /*ゲーム中のみ音源が再生される*/
        if (StartGame)
        {

            audio.UnPause();
            if (time >= level_time[level])
            {

                if (position < max && time > level_time[level])
                {


                    otetuki++;

                }

                time = 0;


            }
            if (runcount >= level_count[level] && time < level_time[level] && level < 22 && otetuki < 2)
            {

                level++;

            }
            else if (otetuki >= 2)
            {


                audio.Stop();
                StartGame = false;

            }

        }
        else
        {

            audio.Pause();

        }


        /*座標取得*/
        if (StartGame)
        {

            playerposition = run.transform.position;
            playerx = playerposition.x;
            if (maxposition > playerx)
            {

                position = maxposition - playerx;

            }
            else if (maxposition < playerx)
            {

                position = playerx - maxposition;

            }

            if (position >= max && time <= level_time[level])
            {
                position = 0;
                maxposition = playerposition.x;
                runcount++;
                otetuki = 0;

            }

        }
        /*座標取得*/

    }



}
