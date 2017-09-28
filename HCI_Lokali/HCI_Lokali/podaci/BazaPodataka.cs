

namespace HCI_Lokali
{
    class BazaPodataka
    {

        public BazaLokal bLokal
        {
            get;
            set;
        }

        public BazaTip bTip
        {
            get;
            set;
        }

        public BazaEtiketa bEtiketa
        {
            get;
            set;
        }

        public BazaIkonica bIkonice
        {
            get;
            set;
        }


        private BazaPodataka()
        {
            bLokal = new BazaLokal();
            bTip = new BazaTip();
            bEtiketa = new BazaEtiketa();
            bIkonice = new BazaIkonica();
        }

        private static BazaPodataka instance;

        public static BazaPodataka getInstance()
        {
            if (instance == null)
                instance = new BazaPodataka();
            return instance;
        }
    }
}

