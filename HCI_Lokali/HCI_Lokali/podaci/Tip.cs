using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HCI_Lokali
{
    [Serializable]
    public class Tip
    {

        public string oznaka
        {
            get;
            set;
        }

        public string ime
        {
            get;
            set;
        }

        public string opis
        {
            get;
            set;
        }

        public string slika
        {
            get;
            set;
        }
    }

    class BazaTip
    {
        private BindingList<Tip> tip_list = new BindingList<Tip>();
        private readonly string datoteka;

        public BazaTip()
        {
            datoteka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BazaTip.data");
            UcitajDatoteku();
        }

        private void UcitajDatoteku()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            if (File.Exists(datoteka))
            {
                try
                {
                    stream = File.Open(datoteka, FileMode.Open);
                    tip_list = (BindingList<Tip>)formatter.Deserialize(stream);
                }
                catch
                {
                    //
                }
                finally
                {
                    if (stream != null)
                        stream.Dispose();
                }
            }
            else
                tip_list = new BindingList<Tip>();
        }

        public void MemorisiDatoteku()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(datoteka, FileMode.OpenOrCreate);
                formatter.Serialize(stream, tip_list);
            }
            catch
            {
                //
            }
            finally
            {
                if (stream != null)
                    stream.Dispose();
            }
        }

        public void Dodaj(BindingList<Tip> tl)
        {
            tip_list = tl;
            MemorisiDatoteku();
        }

        public BindingList<Tip> getAll()
        {
            return tip_list;
        }
    }
}
