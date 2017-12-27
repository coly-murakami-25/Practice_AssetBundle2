using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResouceDataUnziper : MonoBehaviour {

	[SerializeField] string resourceDataPath;
	[SerializeField] string resourceDataName;
	
	void Start(){
		StartCoroutine( BundleLoadCo());
	}

	IEnumerator BundleLoadCo(){

		/// 2.解凍。
		var unzipCo = Bundle_LoadFromFile( resourceDataPath + resourceDataName );
		yield return StartCoroutine( unzipCo );
		var assetbundle = (AssetBundle)unzipCo.Current;

		/// 3.Assetの取り出し
		var loadAssetCo = Bundle_LoadAsset( assetbundle, ".txt" );
		yield return StartCoroutine( loadAssetCo );
		var textAsset = (TextAsset)loadAssetCo.Current;		

		var tableDatas = new CsvDataContainer( textAsset ).csvDatas;
	　　for (int i = 0; i < tableDatas.Count; i++) {
			for (int j = 0; j < tableDatas[i].Length; j++) {
			　　Debug.Log("csvDatas[" + i + "][" + j + "] = " + tableDatas[i][j]);
			}
	　　}


	}

	/// 2、Bundle解凍？（Unity的にはこれ一択らしい）
	IEnumerator Bundle_LoadFromFile( string _cachePath ){
		var assetbundle = AssetBundle.LoadFromFile( _cachePath );
		yield return assetbundle;
	}

	/// 3.Assetの取り出し？（公式だけど最善の選択か確認はしてない）
	IEnumerator Bundle_LoadAsset( AssetBundle _asset, string suffix ){
		var textAsset = _asset.LoadAsset<TextAsset>( System.IO.Path.GetFileName( resourceDataPath + resourceDataName ) + suffix );
		yield return textAsset;
	}
	
}
