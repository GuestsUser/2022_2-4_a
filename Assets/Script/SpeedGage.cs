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

    [SerializeField] Text[] texts = null;

    //参照先
    public PlayerController playercontroller;
    public Level level;

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
        if (playercontroller.stop_flg == false && level.timemax < 0f)
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
                //ゲージを減らす
                DecreaseGage();

                //連打数が指定の数以上になった時
                if(RendaCount >= StartCount)
                {
                    //連打状態にする
                    isMashing = true;
                }
            }
            //数えていて連打しているとき
            else
            {
                texts[5].text = "連打している";

                //ゲージを増やす
                GageAmount += 0.2f * Time.deltaTime;
                //ゲージが満タンになったら
                if (GageAmount >= 1f) 
                    //ゲージをマックスのままにする
                    GageAmount = 1f;

                Gage.fillAmount = GageAmount;
            }
        }
        //数えていない時
        else
        {
            texts[5].text = "数えていない";

            //ゲージを減らす
            DecreaseGage();
        }
    }

    //ゲージを減らす関数
    void DecreaseGage()
    {
        GageAmount -= 0.4f * Time.deltaTime;
        //ゲージが0以下になったら
        if (GageAmount <= 0f)
            //ゲージを0にする
            GageAmount = 0f;

        Gage.fillAmount = GageAmount;
    }
}