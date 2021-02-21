using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GmapsProject.Model
{
    class DataLoader
    {  
        private const string PATH = "C:\\Users\\User\\source\\repos\\GmapsProject\\Data\\data.csv";

        List<Ciudad> lista;

        public DataLoader()
        {
            lista = new List<Ciudad>();
            readInfo();
        }

        private void readInfo()
        {

            var reader = new StreamReader(File.OpenRead(PATH));
            int count = 0;
            while (!reader.EndOfStream && count < 100)
            {
                var line = reader.ReadLine();
                var arreglo = line.Split(',');

                lista.Add(new Ciudad(arreglo[4],arreglo[2],"Colombia"));
                count++;
            }

        }

        public List<Ciudad> getLista()
        {

            return lista;

        }

    }
}
