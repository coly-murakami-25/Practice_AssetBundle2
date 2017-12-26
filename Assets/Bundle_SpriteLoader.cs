using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[RequireComponent(typeof(Image))]
public class Bundle_SpriteLoader : MonoBehaviour {

	[SerializeField] string fileFullPath;
	private string cachePath;

	void Start(){
		StartCoroutine( BundleLoadCo());
	}

	IEnumerator BundleLoadCo(){
		/// 1、DL?。
		// var DownLoadCo = DLAssetBundle( fileFullPath );
		// yield return StartCoroutine( DownLoadCo );

		yield return new WaitForSeconds( 10f );
		cachePath = "Application.persistentDataPath" + System.IO.Path.GetFileName( fileFullPath );


		/// 2.解凍。
		var unzipCo = Bundle_LoadFromFile( cachePath );
		yield return StartCoroutine( unzipCo );
		var assetbundle = (AssetBundle)unzipCo.Current;

		/// 3.Assetの取り出し
		var loadAssetCo = Bundle_LoadAsset( assetbundle, ".png" );
		yield return StartCoroutine( loadAssetCo );
		var bundleSprite = (Sprite)loadAssetCo.Current;		

		/// 4.Imageにアタッチ
		if( GetComponent<Image>() != null ){
			GetComponent<Image>().sprite = bundleSprite;
		}
	}

	// /// 1.AssetBundleのDL。
	// IEnumerator DLAssetBundle( string _fileFullPath ){
	// 	using(WWW www = new WWW(_fileFullPath)){
	// 		// DL完了待ち
	// 		while( !www.isDone ){
	// 			Debug.Log("DL待ち");
	// 			yield return null;
	// 		}
	// 		// エラーがあれば表示
	// 		if( !string.IsNullOrEmpty( www.error )){
	// 			Debug.Log("DLエラー");
	// 		} else {
	// 			try{				
	// 				// キャッシュに保存
	// 				cachePath = "Application.persistentDataPath" + _fileFullPath;
	// 				File.WriteAllBytes(cachePath ,www.bytes);
	// 			}catch (System.Exception ex){
	// 				Debug.LogError("DLエラー発生：" + ex);
	// 			}
	// 		}
	// 	}
	// }


	/// 2、Bundle解凍？（Unity的にはこれ一択らしい）
	IEnumerator Bundle_LoadFromFile( string _cachePath ){
		var assetbundle = AssetBundle.LoadFromFile( _cachePath );
		yield return assetbundle;
	}

	/// 3.Assetの取り出し？（公式だけど最善の選択か確認はしてない）
	IEnumerator Bundle_LoadAsset( AssetBundle _asset, string suffix ){
		var sprite = _asset.LoadAsset<Sprite>( System.IO.Path.GetFileName( fileFullPath ) + suffix );
		yield return sprite;
	}





}
