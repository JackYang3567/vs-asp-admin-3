using System;


namespace Game.Entity.Treasure
{
    [Serializable]
    public class OffLineQrCode
    {
        public const string Tablename = "OffLinePayQrCode";

        public const string _ID = "ID";

        public const string _PaymentName = "PaymentName";

        public const string _IconPath = "IconPath";

        public const string _PaymentTypeID = "PaymentTypeID";

        public const string _OwnerID = "OwnerID";

        private int o_id;

        private string o_paymentName;

        private string o_iconPath;

        private int o_paymentTypeID;

        private int o_ownerID;


        public int ID
        {
            get
            {
                return o_id;
            }
            set
            {
                o_id = value;
            }
        }

        public string PaymentName
        {
            get
            {
                return o_paymentName;
            }
            set
            {
                o_paymentName = value;
            }
        }


        public string IconPath
        {
            get
            {
                return o_iconPath;
            }
            set
            {
                o_iconPath = value;
            }
        }

        public int PaymentTypeID
        {
            get
            {
                return o_paymentTypeID;
            }
            set
            {
                o_paymentTypeID = value;
            }
        }

        public int OwnerID
        {
            get
            {
                return o_ownerID;
            }
            set
            {
                o_ownerID = value;
            }
        }
    }
}
