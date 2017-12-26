using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

///---------------------------------------------------------
/// 最新のUpdateFileTableを入手・所持しているクラス
///---------------------------------------------------------
public class UpdateFileTableDownLoader : MonoBehaviour {

	/// 最新のUpdateFileTableの情報が入っているリスト!!
	public List<string[]> tableDatas = new List<string[]>();
	private const string CurrentUFTNameFile     = "updateFileTableNameFile";
	private const string PlayerPrefs_UFTFileKey = "updateFileName";
	private void Start() {
		CheckUpdate();
	}

	/// 更新があるか確認。
	private void CheckUpdate(){
		var isUpdated = ( GetFileTableName_SaveData() != GetFileTableName_Server() );
		if( isUpdated ){
			Debug.Log("FileTableに更新があったので、DLしてきます" );
			Update_UpdateFileTableDatas();
			SetFileTableName_SaveData();
		} else {
			Debug.Log("FileTableに更新はないので、DL処理はありません") ;
		}
	}

	/// PlayerPrefsに保存しているupdateFileTable名
	private string GetFileTableName_SaveData(){
		return PlayerPrefs.GetString( PlayerPrefs_UFTFileKey, "" );
	}

	/// PlayerPrefsに保存する
	private void SetFileTableName_SaveData(){
		PlayerPrefs.SetString( PlayerPrefs_UFTFileKey, GetFileTableName_Server() );
		PlayerPrefs.Save();
		Debug.Log( "セーブしました" );
	}

	/// Resources/updateFileTable_NameFile に記載されているファイル名
	private string GetFileTableName_Server(){
		var textAsset = Resources.Load ( CurrentUFTNameFile ) as TextAsset;
		Debug.Log ( "updateFileTable_NameFile の 指定ファイル名： " + textAsset.text );
		return textAsset.text;
	}

	/// 名前を受け取り、その名前のUpdateFileTableを返す。
	private TextAsset GetNewUpdateFileTable( string fileTableName_Server ){
		return Resources.Load( fileTableName_Server ) as TextAsset;
	}

	/// updateFileTableDatas の内容を更新。Debug.Log()する。
	private void Update_UpdateFileTableDatas(){
		var tableText = GetNewUpdateFileTable( GetFileTableName_Server() );
		tableDatas    = new CsvDataContainer( tableText ).csvDatas;
	　　for (int i = 0; i < tableDatas.Count; i++) {
			for (int j = 0; j < tableDatas[i].Length; j++) {
			　　Debug.Log("csvDatas[" + i + "][" + j + "] = " + tableDatas[i][j]);
			}
	　　}
	}
}


