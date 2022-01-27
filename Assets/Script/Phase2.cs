using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase2 : MonoBehaviour
{
    [SerializeField] private SEResuorce _se_resource;
    //[SerializeField] private GameObject[] _item_obj;
    //[SerializeField] private GameObject _selector_obj;
    [SerializeField] private GameObject _next_phasej; //次のシーン
    private P2UI key_system;

    [SerializeField] private Image fade_panel;
    [SerializeField] private GameObject parent; //下記のUIオブジェクトを持つ親のオブジェクト
    [SerializeField] private Text point; //反復回数
    [SerializeField] private Text level; //到達レベル
    [SerializeField] private Text rank; //評価

    public SEResuorce se_resource { get { return _se_resource; } }
    //public GameObject selector_obj { get { return _selector_obj; } }
    public GameObject next_phasej { get { return _next_phasej; } }

    [System.NonSerialized] public bool end_bool = false; //trueにして各種コルーチン終了、インスペクターから隠す

    public bool bool_start { get; set; } = false;//これがtrueになったタイミングではじめる

    [SerializeField] private string[] rank_name = { "E", "D", "C", "B", "A", "S" }; //評価、要素番号が0に近い程低評価
    [SerializeField] private int level_max = 22; //レベル最大値、レベル最大値と評価の数に応じて評価区分を変化させる

    // Start is called before the first frame update
    void Start()
    {
        Vector3 ini_pos = parent.transform.position;
        ini_pos.y -= 720;
        parent.transform.position = ini_pos; //0,0,0の位置を目指して移動させるため初期位置を変更
        //ゲームシーンから持ってきた値の代入
        point.text = PlayerPrefs.GetInt("runcount", 0).ToString();
        level.text = PlayerPrefs.GetInt("level", 0).ToString();
        rank.text = rank_name[(int)Mathf.Floor(PlayerPrefs.GetInt("level", 0) / (level_max / rank_name.Length))];
        key_system = new P2UI(this);
        StartCoroutine("WaitTask");
    }
    IEnumerator WaitTask() //前に実行していたPhaseからbool_startをtrueにする事で各種コルーチンを開始する
    {
        while (!bool_start) { yield return StartCoroutine("TimeStop"); }
        StartCoroutine("Fade");
        StartCoroutine(key_system.SoundStart());
    }

    IEnumerator Fade()
    {
        
        int time = 30; //初期位置から0,0,0まで掛ける時間、フレーム数指定
        int count = 0;
        Vector3 edit = parent.transform.position; //編集用Vector3
        Vector3 ini_pos = edit; //初期位置pos
        Vector3 dest_pos = edit; //目的値Vector3
        dest_pos.y += 720; 
        while ( count < time )
        {
            float code = Mathf.Sin(count * (90 / time) * Mathf.Deg2Rad); //今回の掛ける値
            float reciprocal = 1 - code; //掛ける値がどれ程1に満ていないか
            edit.y = ini_pos.y * reciprocal + dest_pos.y * code; //0に近ければ初期位置、1に近ければ目的位置に寄せる
            parent.transform.position = edit;
            count++;
            yield return StartCoroutine("TimeStop");
        }
        parent.transform.position = dest_pos; //決定ボタをすぐ押しても到達は必ず所定時間掛ける
    }

    public IEnumerator ColorOut() //基本構造が一緒なので引数で取り扱えばよかったなー
    {
        int time = 60;
        int count = 0;

        Color32 edit = fade_panel.color;
        Color32 dest_color = edit;
        Color32 ini_color = edit;
        dest_color.a = 96; //目的のアルファ値
        while (count<time)
        {
            float code = Mathf.Sin(count * (90 / time) * Mathf.Deg2Rad); //今回の掛ける値
            float reciprocal = 1 - code; //掛ける値がどれ程1に満ていないか
            edit.a = (byte)(ini_color.a * reciprocal + dest_color.a * code ) ; //0に近ければ初期位置、1に近ければ目的位置に寄せる
            fade_panel.color = edit;
            count++;
            yield return StartCoroutine("TimeStop");
        }
        fade_panel.color = dest_color ;
    }

    IEnumerator TimeStop()
    {
        do { yield return null; } while (Time.timeScale == 0);
    }

    private class P2UI : KeyFunction
    {
        private Phase2 super;
        public P2UI(Phase2 get_super) //初期化時にパラメーター設定
        {
            super = get_super;
            se = super.se_resource;
            item = new GameObject[0]; //選択肢は使用しないので空オブジェクトを挿入
            selector = new GameObject(); //カーソルも使わないとは……
            bool_decision = true;
        }
        public override void Decision() 
        { 
            super.end_bool=true;
            //StartCoroutine(super.ColorOut());
            super.StartCoroutine(super.ColorOut()); //上の書き方ではnullreferenceを起こすなんで
            super.next_phasej.GetComponent<Phase3>().bool_start = true;
        }
    }
}

