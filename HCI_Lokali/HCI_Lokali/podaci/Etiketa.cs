using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.ComponentModel;

namespace HCI_Lokali
{
    [Serializable]
    public class Etiketa
    {
        public string oznaka
        {
            get;
            set;
        }

        public string boje
        {
            get;
            set;
        }

        public string opis
        {
            get;
            set;
        }

        public string imeBoje
        {
            get;
            set;
        }
    }


    //serijalizacija etiketa
    class BazaEtiketa
    {
        private BindingList<Etiketa> etik_list = new BindingList<Etiketa>();

        private readonly string datoteka;

        //konstruktor - kreiramo ime datoteke i zadajemo putanju etiketa i pozivamo metodu za ucitavanje!!!
        public BazaEtiketa()
        {
            datoteka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BazaEtiketa.data");
            UcitajDatoteku();
        }

        //ucitavamo datoteku ako postoji!!!
        private void UcitajDatoteku()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            if (File.Exists(datoteka))
            {
                try
                {
                    stream = File.Open(datoteka, FileMode.Open);
                    etik_list = (BindingList<Etiketa>)formatter.Deserialize(stream);
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
            //ako ne postoji datoteka, napravi novu listu etiketa
            else
                etik_list = new BindingList<Etiketa>();
        }

        //memorisemo datoteku!!!
        public void MemorisiDatoteku()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(datoteka, FileMode.OpenOrCreate);
                formatter.Serialize(stream, etik_list);
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

        //dodaj neku etiketu u listu etiketa i memorisi izmene u datoteci
        public void Dodaj(BindingList<Etiketa> el)
        {
            etik_list = el;
            MemorisiDatoteku();
        }

        //vracamo listu svih dosada unetih etiketa
        public BindingList<Etiketa> getAll()
        {
            return etik_list;
        }
    }
}
