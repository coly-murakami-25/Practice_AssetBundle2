using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

///---------------------------------------------------------
/// csv データのコンテナ
///---------------------------------------------------------
public class CsvDataContainer : MonoBehaviour {

	public List<string[]> csvDatas = new List<string[]>();
	public CsvDataContainer ( TextAsset textAsset ) {
		csvDatas = CsvToListConverter.Convert( textAsset );
    }
}

///---------------------------------------------------------
/// csv を string[] 型のリストにするコンバータ
///---------------------------------------------------------
public static class CsvToListConverter {
	public static List<string[]> Convert ( TextAsset textAsset ) {
		// csvをロード
		StringReader reader = new StringReader ( textAsset.text );
		var csvDatas = new List<string[]>();
		while ( reader.Peek () > -1 ) {
			// ','ごとに区切って配列へ格納
			string line = reader.ReadLine ();
			csvDatas.Add (line.Split (','));
		}
		return csvDatas;
    }
}
