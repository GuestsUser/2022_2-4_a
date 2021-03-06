using UnityEngine;
using UnityEngine.UI;

public class SpeedGage : MonoBehaviour
{
    [SerializeField] Image Gage = null;    //ゲージ
    public float GageAmount;   //ゲージの量

    //連打中の連打間隔
    [SerializeField] float Interval = 0.3f;

    //何連打目から連打中に切り替えるか
    [SerializeField] int StartCount = 3;
    public int RendaCount;         //連打数
    int RendaCountRecord;   //連打数の記録
    bool isCounting;        //連打を数えているかどうか
    bool isMashing;         //連打しているかどうか
    float second;           //連打間の秒数

    [SerializeField] private float gage_down_interval = 0.25f; //この秒数だけ無入力状態が続けばゲージ減少が始まる
    private float no_input_count = 0; //無入力状態時間記録

    [SerializeField] Text[] texts = null;

    //参照先
    public PlayerController playercontroller;
    public Level level;

    [SerializeField] float Speed_up_vol = 45;//スピードゲージ最大を90とした時、1秒のゲージ加算量
    public float speed_up_vol { get { return Speed_up_vol; } }//Speed_up_volはこれを使うと他スクリプトから変数のように引っ張って来る事ができる
    private float sin_val = 0;//ゲージ量を決定する為の値

    private void Start()
    {
        //Imageコンポーネントの設定
        Gage.type = Image.Type.Filled;
        Gage.fillMethod = Image.FillMethod.Horizontal;
        Gage.fillOrigin = 0;
        Gage.fillAmount = 0f;
    }

    private void Update()
    {
        texts[0].text = "RendaCount" + RendaCount;
        texts[1].text = "RendaCountRecord" + RendaCountRecord;
        texts[2].text = "isCounting" + isCounting;
        texts[3].text = "isMashing" + isMashing;
        texts[4].text = "second: " + second;

        //カウントダウン中,待機中は連打できないようにする
        if (playercontroller.stop_flg == false && level.timemax < 0f && level.otetuki <= 1)
        {
            //Aボタンを連打されたとき
            if (Input.GetKeyDown("joystick button 0"))
            //左クリックで連打されたとき
            //if (Input.GetMouseButtonDown(0))
            {
                //数えている状態にする
                isCounting = true;

                //秒数をリセット
                second = 0f;
                no_input_count = 0; //未入力状態もリセットする

                //連打数を1増加
                RendaCount++;
            }
        }

        //数えているとき
        if (isCounting)
        {
            texts[5].text = "数えている";

            //秒数をカウント
            second += Time.deltaTime;

            //時間切れの時
            if(second > Interval)
            {
                //数えていない状態にする
                isCounting = false;

                //連打していない状態にする
                isMashing = false;

                //連打数を記録
                RendaCountRecord = RendaCount;

                //連打数をリセット
                RendaCount = 0;
            }
            //数えていて連打中でない時
            else if (!isMashing)
            {
                no_input_count += Time.deltaTime;

                if (no_input_count > gage_down_interval) { DecreaseGage(); } //ゲージを減らす

                //連打数が指定の数以上になった時
                if (RendaCount >= StartCount)
                {
                    //連打状態にする
                    isMashing = true;
                }
            }
            //数えていて連打しているとき
            else
            {
                texts[5].text = "連打している";

                if (sin_val + speed_up_vol * Time.deltaTime >= 90) { sin_val = 90; }//ゲージを増やす
                else { sin_val += speed_up_vol * Time.deltaTime; }//ゲージをマックスのままにする
                GageAmount = Mathf.Sin(sin_val * Mathf.Deg2Rad);
                Gage.fillAmount = GageAmount;
            }
        }
        //数えていない時
        else
        {
            texts[5].text = "数えていない";
            no_input_count += Time.deltaTime;

            if (no_input_count > gage_down_interval ) { DecreaseGage(); } //ゲージを減らす
        }
    }

    //ゲージを減らす関数
    void DecreaseGage()
    {
        if (sin_val - speed_up_vol * Time.deltaTime <= 0) { sin_val = 0; }
        else { sin_val -= speed_up_vol * Time.deltaTime; }
        GageAmount = Mathf.Sin(sin_val * Mathf.Deg2Rad);

        Gage.fillAmount = GageAmount;
    }
}