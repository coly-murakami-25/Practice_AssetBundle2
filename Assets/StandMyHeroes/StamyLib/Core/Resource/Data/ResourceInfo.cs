using UnityEngine;

namespace StandMyHeroes {

    [System.Serializable]
    public class ResourceInfo {

        [SerializeField]
        private string fileName;
        [SerializeField]
        private string serverPath;
        [SerializeField]
        private string localPath;
        [SerializeField]
        private int version;
        [SerializeField]
        private ResourceRate rate;
        [SerializeField]
        private int deleteFlag;
        [SerializeField]
        private string type;
        [SerializeField]
        private string extension;
        [SerializeField]
        private double size;
        [SerializeField]
        private bool isExist;

        public ResourceInfo( string n, int v, string ex, double si, ResourceRate r, string t = "", int d = 0 ) {
            fileName    = n;
            version     = v;
            extension   = ex;
            size        = si;
            rate        = r;
            type        = t;
            deleteFlag  = d;
        }

        public ResourceInfo( ResourceInfo copy ) {
            fileName    = copy.Name;
            version     = copy.Version;
            serverPath  = copy.ServerPath;
            localPath   = copy.LocalPath;
            extension   = copy.Extension;
            size        = copy.Size;
            rate        = copy.Rate;
            isExist     = copy.Exist;
            type        = copy.Type;
            deleteFlag  = copy.DeleteFlag;
        }

        /// <summary>
        /// AssetBundleの名前
        /// </summary>
        public string Name {
            get {
                return fileName;
            }
        }

        public string ServerPath {
            get {
                return serverPath;
            }
            set {
                serverPath = value;
            }
        }

        /// <summary>
        /// 保存先
        /// </summary>
        public string LocalPath {
            get {
                return localPath;
            }
            set {
                localPath = value;
            }
        }

        /// <summary>
        /// リソースのバージョン
        /// </summary>
        public int Version {
            get {
                return version;
            }
        }

        /// <summary>
        /// AssetBundle元の拡張子
        /// </summary>
        public string Extension {
            get {
                return extension;
            }
        }

        /// <summary>
        /// Resourceのレート
        /// </summary>
        public ResourceRate Rate {
            get {
                return rate;
            }
        }

        /// <summary>
        /// AssetBundleのサイズ
        /// </summary>
        public double Size {
            get {
                return size;
            }
            set {
                size = value;
            }
        }

        /// <summary>
        /// 端末内に存在するかどうか
        /// </summary>
        public bool Exist {
            get {
                return isExist;
            }
            set {
                isExist = value;
            }
        }

        /// <summary>
        /// なんらかのフラグ
        /// </summary>
        public int DeleteFlag {
            get {
                return deleteFlag;
            }
            set {
                deleteFlag = value;
            }
        }

        /// <summary>
        /// なんらかのデータ
        /// </summary>
        public string Type {
            get {
                return type;
            }
            set {
                type = value;
            }
        }


        //==============================================
        // 処理
        //==============================================
        public bool RateEquals( ResourceRate r ) {
            return rate == r;
        }

        public bool IsDownloadResource {
            get {
                return RateEquals( ResourceRate.S ) || RateEquals( ResourceRate.E );
            }
        }

        public bool IsPreDownload {
            get {
                return RateEquals( ResourceRate.L );
            }
        }

        public bool IsDelete {
            get {
                return RateEquals( ResourceRate.D ) && Exist;
            }
        }

    }

    public enum ResourceRate {
        L, //DL前になくてはならない
        S, //必ず端末内に存在しなくてはならない
        A, //
        B, //
        C, //無くてもエラー吐かない
        D, //ほとんど使用されない
        E, //イベント時のみ
    }

}