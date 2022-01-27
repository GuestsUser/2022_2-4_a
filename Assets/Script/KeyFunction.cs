using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KeyFunction : MonoBehaviour //seを使用する場合このクラスを継承したクラスを用意し、必要に応じてプロパティの変更、関数のoverride等を行い、そのクラスのオブジェクトをSoundStartコルーチンの引数に引き渡す
{
    //必須部分、継承クラスをインスタント化する際この変数に値を入れないと動かない、鳴らすseやら選択肢に使うオブジェクトやら、例え使わないとしても全ての変数に必ず1つは要素が必要
    public SEResuorce se { set; get; } //様々なseを詰めたリソース集
    public GameObject[] item { set; get; } //選択肢ゲームオブジェクト、配列をnewしてこの変数に入れたら各要素に選択肢として使いたいオブジェクトを入れる
    public GameObject selector { set; get; } //選択中の場所に置くカーソルオブジェクト



    //カスタマイズプロパティ、値を代入したりbool値を弄ればSoundStartコルーチンの挙動を弄れる
    public KeyCode key_decision { set; get; } = KeyCode.Joystick1Button0; //決定ボタン
    public KeyCode key_cancel { set; get; } = KeyCode.Joystick1Button1; //キャンセルボタン
    public float hit_tilt { set; get; } = 0.7f; //GetAxisのVertical、Horizontalを使って十字キー入力の判定を取る、その際、パッドがどこまで倒されたら入力と取るかの値

    public bool unscale_bool { set; get; } = true; //自動移動等の時間計測に用いるtimedeltatimeがtimescaleに影響されるかどうか、trueにする時時間を止めても時間計測してくれる
    public int auto_move_change { set; get; } = 16; //十字キー押しっぱなしで自動移動機能、この時間分押しっぱなしが続いたら自動移動を開始する、0以下にすると自動移動機能を封印する、秒数指定ではなくフレーム数指定
    public int auto_move_speed { set; get; } = 6; //自動移動の際毎フレーム移動すると超高速になってしまうので次の移動まで待つ時間、こちらもフレーム数指定



    //以下は対象ボタンを押されたときそのseを鳴らすかどうか、trueで鳴らす、序にtrueだとボタンが押された際対応した関数が呼び出され、キー入力受付を終了する(十字キーを押した場合を除く)
    //因みに、決定、キャンセルどちらかはtrueにしないと無限ループする
    public bool bool_decision { set; get; } = false; //決定音を鳴らすかどうか
    public bool bool_cancel { set; get; } = false; //キャンセル音を鳴らすかどうか
    public bool[] bool_udlr { set; get; } //十字キー入力時seを鳴らすかどうか、4つの要素を持つ配列で、[0]=上、[1]=下、[2]=左、[3]=右の順



    //機能用変数、書き換え禁止
    private int _select = 0; //選択中の選択肢ゲームオブジェクトの添え字を記録する変数、DecisionやCancel等が呼び出されたタイミングのここの値が選んだ項目という事になる
    private int _select_old = 0; //前回までの選択中ゲームオブジェクト添え字、選択中オブジェクトのマテリアルを変更する場合選択から外れたオブジェクトのマテリアルを戻す必要があるだろうからそれに使える変数
    public int select { get { return _select; } } //このプロパティを用いれば継承先でもどの項目が選ばれたか確認できる
    public int select_old { get { return _select_old; } } //このプロパティで前回までの選択中オブジェクトを確認できる


    //コンストラクタ、継承先のクラスでコンストラクタを用意してもこちらが先に呼び出されるのでbool_udlrが空などの心配無用、ゲームオブジェクトだけはそっちで配列を用意してほしい
    public KeyFunction() { 
        bool_udlr = new bool[4];
        for (int i = 0; i < 4; i++) { bool_udlr[i] = false; }
    }



    //ボタンが押された際の挙動設定、継承先のクラスにvirtualをoverrideに変えた同名関数を用意して動作をカスタム
    //入力受付終了処理はSoundStartコルーチンが勝手にやってくれるので不要
    public virtual void Decision() {; } //決定ボタンを押した時の動作、シーン遷移等を記入したい
    public virtual void Cancel() {; } //キャンセルボタンを押した時の動作
    public virtual void Move() {; } //選択肢移動があった場合の処理、カーソルの位置を微調整したり
    


    //動作用コルーチン、書き換え禁止
    public IEnumerator SoundStart()
    {
        float count = 0; //押しっぱなし時間記録用
        while (true)
        {
            //前にyield returnを持ってくる事で、決定ボタン1押しでどこまでもシーンを以降する現象を防ぐ
            if (unscale_bool) { yield return null; } //ウィンドウを非アクティブ化すると時間が消し飛ぶ現象発生
            else { yield return StartCoroutine("TimeStop"); }

            if (bool_decision) 
            { 
                if (Input.GetKeyDown(key_decision)) //押しっぱなしで到達してもいいように決定、キャンセルはKeyDownで行う
                {
                    se.audio_source.PlayOneShot(se.d_se);
                    Decision(); 
                    break; 
                } 
            }
            if (bool_cancel)
            {
                if (Input.GetKeyDown(key_cancel)) //押しっぱなしで到達してもいいように決定、キャンセルはKeyDownで行う
                {
                    se.audio_source.PlayOneShot(se.c_se);
                    Cancel();
                    break;
                }
            }

            bool up = bool_udlr[0] && Input.GetAxis("Vertical") >= hit_tilt;
            bool down = bool_udlr[1] && Input.GetAxis("Vertical") <= hit_tilt*-1;
            bool left = bool_udlr[2] && Input.GetAxis("Horizontal") <= hit_tilt*-1;
            bool right = bool_udlr[3] && Input.GetAxis("Horizontal") >= hit_tilt;
            //unityは上キーでvertical正の値だっけな?
            if (up||down||left||right) 
            {
                if(count == 0 || (count >= auto_move_change && (count - auto_move_change) % auto_move_speed == 0))
                {
                    int v_direction = (-1 * Convert.ToInt32(up)) + Convert.ToInt32(down); //上下方向
                    int h_direction = (-1 * Convert.ToInt32(left)) + Convert.ToInt32(right); //左右方向
                    
                    _select = (_select + v_direction + h_direction) % item.Length; //選択肢移動の+-操作から
                    if (_select < 0) { _select = item.Length + _select; }//マイナスになると余り計算が望み通りの値を返してくれなくなるので正の値に戻す処理
                    if (_select != select_old)//今までの演算の結果選択が動いた時だけ動作処理をする
                    {
                        se.audio_source.PlayOneShot(se.move_se);
                        selector.transform.position = item[_select].transform.position; //現在選択中の選択肢と位置を合わせる
                        Move();
                    }
                    
                }
                count++;
            }
            else { count = 0; }

            _select_old = _select; //前フレームの選択肢更新
        }
        
    }
    IEnumerator TimeStop()
    {
        do { yield return null; } while (Time.timeScale == 0);
    }
}
