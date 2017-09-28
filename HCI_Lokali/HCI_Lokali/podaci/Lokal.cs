using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace HCI_Lokali
{
    [Serializable]
    public class Lokal
    {
        public BindingList<Etiketa> etikete
        {
            get;
            set;
        }

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

        public Tip tip
        {
            get;
            set;
        }

        public string statusalkohola
        {
            get;
            set;
        }

        public bool rezervacije
        {
            get;
            set;
        }

        public string opis
        {
            get;
            set;
        }

        public bool pusenje
        {
            get;
            set;
        }

        public int kapacitet
        {
            get;
            set;
        }

        public DateTime datum
        {
            get;
            set;
        }

        public bool hendikepirani
        {
            get;
            set;
        }

        public string kategorijaCena
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

    class BazaLokal
    {
        private BindingList<Lokal> lokal_list = new BindingList<Lokal>();
        private readonly string datoteka;

        public BazaLokal()
        {
            datoteka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BazaLokal.data");
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
                    lokal_list = (BindingList<Lokal>)formatter.Deserialize(stream);
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
                lokal_list = new BindingList<Lokal>();
        }

        public void MemorisiDatoteku()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(datoteka, FileMode.OpenOrCreate);
                formatter.Serialize(stream, lokal_list);
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

        public void Dodaj(BindingList<Lokal> lo)
        {
            lokal_list = lo;
            MemorisiDatoteku();
        }

        public BindingList<Lokal> getAll()
        {
            return lokal_list;
        }
    }
}


