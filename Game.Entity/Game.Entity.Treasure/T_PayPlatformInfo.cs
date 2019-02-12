using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Entity.Treasure
{
    [Serializable]
    public class T_PayPlatformInfo
    {
        public const string Tablename = "T_PayPlatformInfo";

        public const string _ID = "ID";
        public const string _PlatformName = "PlatformName";
        public const string _PlatformCode = "PlatformCode";
        public const string _PlatformID = "PlatformID";
        public const string _QudaoID = "QudaoID";
        public const string _Nullity = "Nullity";
        public const string _SortID = "SortID";
        public const string _QudaoName = "QudaoName";
        public const string _QudaoCode = "QudaoCode";
        public const string _priKey = "priKey";
        public const string _pubKey = "pubKey";
        public const string _url = "url";
        public const string _findUrl = "findUrl";
        public const string _backName = "backName";
        public const string _backAcc = "backAcc";
        public const string _backAdd = "backAdd";
        public const string _PryType = "PryType";

        private int m_ID;

        public int ID
        {
            get { return m_ID; }
            set { m_ID = value; }
        }
        private string m_PlatformName;

        public string PlatformName
        {
            get { return m_PlatformName; }
            set { m_PlatformName = value; }
        }
        private string m_PlatformCode;

        public string PlatformCode
        {
            get { return m_PlatformCode; }
            set { m_PlatformCode = value; }
        }
        private int m_PlatformID;

        public int PlatformID
        {
            get { return m_PlatformID; }
            set { m_PlatformID = value; }
        }
        private int m_QudaoID;

        public int QudaoID
        {
            get { return m_QudaoID; }
            set { m_QudaoID = value; }
        }
        private int m_Nullity;

        public int Nullity
        {
            get { return m_Nullity; }
            set { m_Nullity = value; }
        }
        private int m_SortID;

        public int SortID
        {
            get { return m_SortID; }
            set { m_SortID = value; }
        }
        private string m_QudaoName;

        public string QudaoName
        {
            get { return m_QudaoName; }
            set { m_QudaoName = value; }
        }
        private string m_QudaoCode;

        public string QudaoCode
        {
            get { return m_QudaoCode; }
            set { m_QudaoCode = value; }
        }
        private string m_priKey;

        public string PriKey
        {
            get { return m_priKey; }
            set { m_priKey = value; }
        }
        private string m_pubKey;

        public string PubKey
        {
            get { return m_pubKey; }
            set { m_pubKey = value; }
        }
        private string m_url;

        public string Url
        {
            get { return m_url; }
            set { m_url = value; }
        }
        private string m_findUrl;

        public string FindUrl
        {
            get { return m_findUrl; }
            set { m_findUrl = value; }
        }
        private string m_backName;

        public string BackName
        {
            get { return m_backName; }
            set { m_backName = value; }
        }
        private string m_backAcc;

        public string BackAcc
        {
            get { return m_backAcc; }
            set { m_backAcc = value; }
        }
        private string m_backAdd;

        public string BackAdd
        {
            get { return m_backAdd; }
            set { m_backAdd = value; }
        }
        private int m_PryType;

        public int PryType
        {
            get { return m_PryType; }
            set { m_PryType = value; }
        }


        public T_PayPlatformInfo()
		{
            m_ID = 0;
            m_PlatformName = "";
            m_PlatformCode = "";
            m_PlatformID = 0;
            m_QudaoID = 0;
            m_Nullity = 0;
            m_SortID = 0;
            m_priKey = "";
            m_pubKey = "";
            m_url = "";
            m_findUrl = "";
            m_backName = "";
            m_backAcc = "";
            m_backAdd = "";
            m_PryType = 0;
		}
    }
}
