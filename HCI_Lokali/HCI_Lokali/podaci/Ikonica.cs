using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.ComponentModel;


namespace HCI_Lokali
{
    [Serializable]
    public class Ikonica
    {
        //pozicija
        public Dictionary<Point, Lokal> poz
        {
            get;
            set;
        }

    }
    class BazaIkonica
    {
        public BindingList<Ikonica> icon_list = new BindingList<Ikonica>();
        private readonly string datoteka;

        public BazaIkonica()
        {
            datoteka = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BazaIkonica.data");
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
                    icon_list = (BindingList<Ikonica>)formatter.Deserialize(stream);
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
                icon_list = new BindingList<Ikonica>();
        }

        public void MemorisiDatoteku()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = null;

            try
            {
                stream = File.Open(datoteka, FileMode.OpenOrCreate);
                formatter.Serialize(stream, icon_list);
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

        public void Dodaj(BindingList<Ikonica> tl)
        {
            icon_list = tl;
            MemorisiDatoteku();
        }

        public BindingList<Ikonica> getAll()
        {
            return icon_list;
        }
    }
}
