using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Phase3 : MonoBehaviour
{
    [SerializeField] private SEResuorce _se_resource;

    [SerializeField] private GameObject parent; //itemの親
    [SerializeField] private GameObject[] _item_obj;
    [SerializeField] private GameObject _selector_obj;
    //[SerializeField] private GameObject _next_phasej; //次のシーン
    private P3UI key_system;


    public SEResuorce se_resource { get { return _se_resource; } }
    public GameObject selector_obj { get { return _selector_obj; } }
   // public GameObject next_phasej { get { return _next_phasej; } }
    public GameObject[] item_obj { get { return _item_obj; } }

    [System.NonSerialized] public bool end_bool = false; //trueにして各種コルーチン終了、インスペクターから隠す
    public bool bool_start { get; set; } = false;//これがtrueになったタイミングではじめる

    // Start is called before the first frame update
    void Start()
    {
        parent.SetActive(false);

        key_system = new P3UI(this);
        StartCoroutine("WaitTask");
    }

    IEnumerator WaitTask() //前に実行していたPhaseからbool_startをtrueにする事で各種コルーチンを開始する
    {
        while (!bool_start) { yield return StartCoroutine("TimeStop"); }
        parent.SetActive(true);

        StartCoroutine("EasingOrigin");
        StartCoroutine("EndTask");
        StartCoroutine(key_system.SoundStart());
    }

    IEnumerator EndTask() //終了待機
    {
        while ( !end_bool ){ yield return StartCoroutine("TimeStop"); }
        if (key_system.select <= 0) { StartCoroutine("RestartCoroutine"); } //0ならリスタート
        else { StartCoroutine("BacktoTitle"); } //1なら戻る
    }

    IEnumerator TimeStop()
    {
        do { yield return null; } while (Time.timeScale == 0);
    }

    private IEnumerator EasingOrigin()
    {
        int time = 30;
        int count = 0;
        Vector3 edit = Vector3.zero;
        parent.transform.localScale = Vector3.zero;
        while (count < time)
        {
            float num = Mathf.Sin(count * (90 / time) * Mathf.Deg2Rad);
            edit.x = num;
            edit.y = num;
            edit.z = num;
            parent.transform.localScale = edit;
            count++;
            yield return StartCoroutine("TimeStop");
        }
        parent.transform.localScale = new Vector3(1, 1, 1);
    }

    private IEnumerator RestartCoroutine() //シーンチェンジ用
    {
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード

        SceneManager.LoadScene("Game");
        Time.timeScale = 1;
    }
    private IEnumerator BacktoTitle() //シーンチェンジ用
    {
        yield return new WaitForSecondsRealtime(1.5f);  //1.5秒待った後にシーンをロード

        SceneManager.LoadScene("Title");
        Time.timeScale = 1;
    }

    private class P3UI : KeyFunction
    {
        private Phase3 super;
        private Color32 dark;
        private Color32 white;
        public P3UI(Phase3 get_super) //初期化時にパラメーター設定
        {
            super = get_super;
            se = super.se_resource;
            item = super.item_obj; 
            selector = super.selector_obj;
            bool_decision = true;
            bool_udlr[0] = true;//上下移動許可
            bool_udlr[1] = true;
            dark = new Color32(0, 0, 0, 255);
            white = new Color32(255, 255, 255, 255);
            item[0].GetComponent<Text>().color = white; //一番最初に選択中の奴を白でハイライト
        }
        public override void Decision() 
        { 
            super.end_bool=true;
        }
        public override void Move() //選択された物を白く、外されたものを黒くする
        {
            item[select].GetComponent<Text>().color = white;
            item[select_old].GetComponent<Text>().color = dark;
        }
    }
}

