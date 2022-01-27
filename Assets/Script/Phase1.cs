using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Phase1 : MonoBehaviour
{
    [SerializeField] private SEResuorce _se_resource;
    //[SerializeField] private GameObject[] _item_obj;
    [SerializeField] private GameObject _selector_obj;
    [SerializeField] private GameObject _next_phasej; //次のシーン

    public SEResuorce se_resource { get { return _se_resource; } }
    public GameObject selector_obj { get { return _selector_obj; } }
    public GameObject next_phasej { get { return _next_phasej; } }

    [System.NonSerialized] public bool end_bool = false; //trueにして各種コルーチン終了、インスペクターから隠す


    // Start is called before the first frame update
    void Start()
    {
        P1UI obj = new P1UI(this);
        StartCoroutine("Fade");
        StartCoroutine(obj.SoundStart());
    }


    IEnumerator Fade()
    {
        int time = 30; //透明から不透明までどれ位かかるか、フレーム数指定
        int count = 0;
        Color32 set_color = selector_obj.GetComponent<Text>().color;
        while ( !end_bool )
        {
            set_color.a = (byte)( 255 * ( ( Mathf.Cos(count * (90 / time) * Mathf.Deg2Rad) + 1 ) / 2 ) ); //点滅する
            selector_obj.GetComponent<Text>().color = set_color; //透明度設定
            count++;
            yield return StartCoroutine("TimeStop");
        }
    }

    IEnumerator TimeStop()
    {
        do { yield return null; } while (Time.timeScale == 0);
    }

    private class P1UI : KeyFunction
    {
        private Phase1 super;
        public P1UI(Phase1 get_super) //初期化時にパラメーター設定
        {
            super = get_super;
            se = super.se_resource;
            item = new GameObject[0]; //選択肢は使用しないので空オブジェクトを挿入
            selector = super.selector_obj;
            bool_decision = true;
        }
        public override void Decision() 
        { 
            super.end_bool=true;
            super.selector_obj.SetActive(false);
            super.next_phasej.GetComponent<Phase2>().bool_start=true;
        }
    }
}

