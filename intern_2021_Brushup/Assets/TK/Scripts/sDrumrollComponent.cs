using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class sDrumrollComponent : MonoBehaviour
{
    //仕様
    //リストの中身をドラムロール的な感じに表示するクラス。
    //注目indexは最大の大きさで、離れるほど透明度が低く、大きさが小さくなる
    //範囲は５にする。変えれてもいいけど。
    //注目indexが中心になる様に表示される。
    //操作は注目indexのみ?
    //
    //操作
    //小目標
    //テキスト列挙してindexのところまでスクロール
    //使いやすいように改変する。
    //　
    //①リストを受け取る関数
    //②リストを生成する(すでにリストを持っていたら消す)
    //③数値入力でスクロールする関数


    private float startTime;			//　開始時間
    private Vector3 moveVelocity;		//　現在の移動の速度
    [SerializeField] private float moveSpeed;	//　カメラの移動速度
    [SerializeField] List<GameObject> textList = new List<GameObject>();
    [SerializeField] GameObject _resource;
    [SerializeField] float _width = 100;
    [SerializeField] int attensionIndex = 0;
    [SerializeField] Vector3 targetpos;
    [SerializeField] Vector3 originpos;
    [SerializeField] AnimationCurve _alphaCurve;
    [SerializeField] AnimationCurve _scaleCurve;

    // Start is called before the first frame update
    public void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        //テキストの大きさとか透明度調整する。
        for (int i = 0; i < textList.Count; i++)
        {
            float tikasa = Mathf.Abs(textList[i].transform.position.y - originpos.y);

            float value = 1.0f - (tikasa / _width / 3);
            Debug.Log(value);
            var avalue = _alphaCurve.Evaluate(value);
            var svalue = _scaleCurve.Evaluate(value);
            var color = textList[i].GetComponent<Text>().color;
            textList[i].GetComponent<Text>().color = new Vector4(color.r, color.g, color.b, avalue);
            textList[i].GetComponent<Text>().transform.localScale=new Vector3(1+ svalue * 0.1f, 1 + svalue * 0.1f, 1 + svalue * 0.1f);
            textList[i].transform.GetChild(0).GetComponent<Text>().color = new Vector4(color.r, color.g, color.b, value);
            

        }

    }

    public void InputList(List<GameObject> inlist)
    {
        //リストを外部から取得して使う
        textList.Clear();

        textList = inlist;
        originpos = textList[2].transform.position;

    }
    //ランキングをいい感じに見るための関数。
    public void LookThrough(int endindex, int targetindex)
    {
        float endscrolltime = endindex / 20;
        float targetscrolltime = (endindex - targetindex) / 30+0.1f;
        var targety1 = textList[endindex].transform.position.y - originpos.y;//現在の位置からのターゲット１の相対位置
        var targety2 = textList[targetindex].transform.position.y - originpos.y;//現在の位置からのターゲット２の相対位置
        for (int i = 0; i < textList.Count; i++)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.AppendInterval(0.5f);
            sequence.Append(textList[i].transform.DOMoveY(textList[i].transform.position.y - targety1, endscrolltime).SetEase(Ease.InOutCubic));

            sequence.Append(textList[i].transform.DOMoveY(textList[i].transform.position.y - targety2, targetscrolltime).SetEase(Ease.InOutCubic));

            //textList[i].transform.DOMoveY(textList[i].transform.position.y - targetpos.y, 2).SetEase(Ease.InOutCubic);
        }
    }
}
