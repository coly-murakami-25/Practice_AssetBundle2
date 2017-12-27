using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public class CSV_Impoter : AssetPostprocessor {

  static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths) {

    // 「一番左の行はint型！」「その右の行はfloat型！」「その右の行はstring型！」
    // でなければならない様子！


    string targetFile = "Assets/CSVtoScriptableObj/Files/Data.csv";
    string exportFile = "Assets/CSVtoScriptableObj/Files/Data.asset";

    foreach (string asset in importedAssets) {

      // 合致しないものはスルー
      if (!targetFile.Equals(asset)) continue;

      // 既存のマスタを取得
      Data data = AssetDatabase.LoadAssetAtPath<Data>(exportFile);

      // 見つからなければ作成する
      if (data == null) {
        data = ScriptableObject.CreateInstance<Data>();
        AssetDatabase.CreateAsset((ScriptableObject)data, exportFile);
      }
      // 中身を削除
      data.param.Clear();

      // CSVファイルをオブジェクトへ保存
      using (StreamReader sr = new StreamReader(targetFile)) {

        // ヘッダをやり過ごす
        sr.ReadLine();

        // ファイルの終端まで繰り返す
        while (!sr.EndOfStream) {
          string   line     = sr.ReadLine();
          string[] dataStrs = line.Split(',');

          // 追加するパラメータを生成
          Data.Param p = new Data.Param();
          // 値を設定する
          p.intValue    = int.Parse(dataStrs[0]);
          p.floatValue  = float.Parse(dataStrs[1]);
          p.stringValue = dataStrs[2];
          // 追加
          data.param.Add(p);
        }
      }

      // 保存
      AssetDatabase.SaveAssets();

      Debug.Log("Data updated.");
    }
  }
}