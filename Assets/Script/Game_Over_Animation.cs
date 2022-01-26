using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //パネルのイメージを操作するのに必要

public class Game_Over_Animation : MonoBehaviour
{

	float fadeSpeed = 0.02f;        //透明度が変わるスピードを管理
	public float red, green, blue, alfa;   //パネルの色、不透明度を管理

	public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ
	public bool isFadeIn = false;   //フェードイン処理の開始、完了を管理するフラグ

	public bool next_scene = false;


	Image fadeImage;                //透明度を変更するパネルのイメージ

	void Start()
	{
		fadeImage = GetComponent<Image>();
		red = fadeImage.color.r;
		green = fadeImage.color.g;
		blue = fadeImage.color.b;
		alfa = fadeImage.color.a;
	}

	void Update()
	{
		if (isFadeIn)
		{
			StartFadeIn();
			Invoke("StartFadeOut", 3f);
			Invoke("NextScene", 4f);
		}

		//if (isFadeOut)
		//{
		//	StartFadeOut();
		//}
	}

	void StartFadeOut()
	{
		alfa -= fadeSpeed;                //a)不透明度を徐々に下げる
		SetAlpha();                      //b)変更した不透明度パネルに反映する
		if (alfa <= 0)
		{                    //c)完全に透明になったら処理を抜ける
			next_scene = true;
			isFadeOut = false;
			fadeImage.enabled = false;    //d)パネルの表示をオフにする
		}
	}

	void StartFadeIn()
	{
		fadeImage.enabled = true;  // a)パネルの表示をオンにする
		alfa += fadeSpeed;         // b)不透明度を徐々にあげる
		SetAlpha();               // c)変更した透明度をパネルに反映する
		if (alfa >= 1)
		{             // d)完全に不透明になったら処理を抜ける
			isFadeIn = false;
			//isFadeOut = true;
		}
	}

	void SetAlpha()
	{
		fadeImage.color = new Color(red, green, blue, alfa);
	}
	void NextScene()
    {
		next_scene = true;
	}
}