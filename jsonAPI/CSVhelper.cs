using FileHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jsonAPI
{
    public class CSVhelper
    {
        private FileHelperEngine<JokeFromApi> engine = new FileHelperEngine<JokeFromApi>();
        private string path = "jokes.csv";

        public List<JokeFromApi> ReadFile()
        {
            JokeFromApi[] res;
            List<JokeFromApi> list = new List<JokeFromApi>();

            if (File.Exists(path))
            {
                res = engine.ReadFile(path);

                foreach (var record in res)
                {
                    list.Add(record);
                }
            }
            return list;
        }
        public void WriteFile(List<JokeFromApi> listToWrite)
        {
            if (!File.Exists(path))
            {
                FileStream x = File.Create(path);
                x.Close();
            }
            engine.WriteFile(path, listToWrite);
        }
        public void WriteAddOne(JokeFromApi oneToWrite)
        {
            List<JokeFromApi> list = ReadFile();
            list.Add(oneToWrite);
            WriteFile(list);
        }
    }
}
