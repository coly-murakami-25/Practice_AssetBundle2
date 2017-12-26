using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// [System.Serializable]
public class AssetBundleDownloader : MonoBehaviour{

	/// 1.AssetBundleのDL。
	public IEnumerator DLAssetBundle( string _fileFullPath ){
		using(WWW www = new WWW(_fileFullPath)){
			// DL完了待ち
			while( !www.isDone ){
				Debug.Log("DL待ち");
				yield return null;
			}
			// エラーがあれば表示
			if( !string.IsNullOrEmpty( www.error )){
				Debug.Log("DLエラー:" + www.error );
			} else {
				try{				
					// キャッシュに保存
					var cachePath = Application.persistentDataPath + System.IO.Path.GetFileName( _fileFullPath );
					System.IO.File.WriteAllBytes( cachePath ,www.bytes );
				}catch (System.Exception ex){
					Debug.LogError("DLエラー発生：" + ex);
				}
			}
		}
	}
}
