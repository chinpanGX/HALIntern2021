﻿using NCMB; //mobile backendのSDKを読み込む
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MiniJSON;

public class Bg_ghost : MonoBehaviour {
	public static NCMBObject posObj;
	public static bool readyGhost = false;
	// Use this for initialization
	void Start () {
		StartCoroutine(Ghost());
	}

	IEnumerator Ghost()
	{
        yield return new WaitForSeconds(1);
		NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("GameScore");
		query.OrderByDescending ("score");
		query.Limit = 1;
		query.FindAsync ((List<NCMBObject> objList,NCMBException e)=>{

			if(e !=null){
				//検索失敗時の処理
			}else{
				//検索成功時の処理
				//取得したレコードをscoreクラスとして保存
				if(objList.Count > 0){
					Debug.Log("GhostData");
					readyGhost = true;
					foreach(NCMBObject obj in objList){
						posObj = obj;
					}
				}
			}
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
