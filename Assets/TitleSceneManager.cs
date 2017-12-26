using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour {

	private UpdateFileTableDownLoader updateFileTableDownLoader;
	void Start () {
		updateFileTableDownLoader = GetComponent<UpdateFileTableDownLoader>();
		StartCoroutine( TitleAssetBundleDL());
	}
	
	IEnumerator TitleAssetBundleDL(){

		// 5秒待ってから、fileTableの内容をDL。
		yield return new WaitForSeconds( 5.0f );
		for (int i = 0; i < updateFileTableDownLoader.tableDatas.Count; i++) {
			for (int j = 0; j < updateFileTableDownLoader.tableDatas[i].Length; j++) {

				Debug.Log( updateFileTableDownLoader );
				Debug.Log( updateFileTableDownLoader.tableDatas );
				Debug.Log( updateFileTableDownLoader.tableDatas[i] );
				Debug.Log( updateFileTableDownLoader.tableDatas[i][j] );

				Debug.Log( GameDataManager.Instance );
				Debug.Log( GameDataManager.Instance.bundleDownloader );

				var filePath = updateFileTableDownLoader.tableDatas[i][j];
				StartCoroutine( GameDataManager.Instance.bundleDownloader.DLAssetBundle( filePath ));
			}
	　　}
	}
}
