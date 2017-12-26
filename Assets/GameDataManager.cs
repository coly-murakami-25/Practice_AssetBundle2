using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataManager : MonoBehaviour {


	public static GameDataManager Instance;
	private void Awake(){
		Debug.Log("GameDataManager.Start()");
		if( Instance == null ) {
			Debug.Log("GameDataManager.Instanceにインスタンスを入れる");
			Instance = this;
		} else {
			Destroy( gameObject );
		}
		DontDestroyOnLoad( gameObject );
	}

	public AssetBundleDownloader bundleDownloader;

}
