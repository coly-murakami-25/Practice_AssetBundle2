using UnityEngine;
using StandMyHeroes;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// すべてのResourceの情報を持つ
/// </summary>
public class ResourceDataManager : ScriptableObject {

    public string ResourceListName;
    public List<ResourceInfo> ResourceList = new List<ResourceInfo>();
    
    public Dictionary<string, ResourceInfo> ResourceDic = new Dictionary<string, ResourceInfo>();

    /// <summary>
    /// UpdateFileからコンバートしたデータを持つResourceListから、
    /// アプリで使いやすいようにデータを構築
    /// </summary>
    public void ConstructResourceData() {
        var ResourceCount = ResourceList.Count;
        ResourceDic.Clear();
        for ( int i = 0; i < ResourceCount; i++ ) {
            var list = ResourceList[i];
            try {
                // if ( !ResourceDic.ContainsKey( list.Name ) )
                // list.ServerPath = Path.Combine( ResourceManager.ResourceServerURL, list.Name );
                // list.LocalPath  = Path.Combine( ResourceManager.LocalResourceCachePath, list.Name );
                list.Exist      = File.Exists( list.LocalPath );

                ResourceDic.Add( list.Name, list );
            } catch {
                Debug.LogError( "ResourceDataに同じ名前のファイルが含まれています： " + list.Name );
            }
        }
    }

    /// <summary>
    /// エラー処理込みのリソースデータ検索
    /// </summary>
    public ResourceInfo GetResourceInfo( string fileName ) {
        if( string.IsNullOrEmpty( fileName ) )
            return null;
            
        if ( ResourceDic.ContainsKey( fileName ) ) {
            return ResourceDic[fileName];
        } else {
            return null;
        }
    }

}