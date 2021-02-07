using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Time_to_burn_fear
{
    public class DAO
    {
        /// <summary>
        /// Записать строку в файл
        /// </summary>
        /// <param name="addingString">хаписываемая строк</param>
        /// <param name="fileName"> имя файла</param>
        public static void AddStringToFile(string addingString,string fileName)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(fileName, true);
                streamWriter.WriteLine(addingString);
                streamWriter.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось произвести запись в файл: " + fileName);
            }
        }
        public static void AddListToFile(List<string> list, string fileName)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter(fileName, false);
                foreach(string addingString in list)
                {
                    streamWriter.WriteLine(addingString);
                }
                streamWriter.Close();
            }
            catch
            {
                MessageBox.Show("Не удалось произвести запись в файл: " + fileName);
            }
        }
        /// <summary>
        /// Возвращает лист строк созданный из файла
        /// </summary>
        /// <param name="fileName"> путь к файалу</param>
        /// <returns> лист строк</returns>
        public static List<string> GetListStringsFromFile (string fileName)
        {
            List<string> listString = new List<string>();
            if (File.Exists(fileName))
            {
                StreamReader streamReader = new StreamReader(fileName);

                while (!streamReader.EndOfStream)
                {
                    listString.Add(streamReader.ReadLine());
                }
                streamReader.Close();
            }
            else
            {
                MessageBox.Show("Не удалось зачитать файл " + fileName);
            }
            return listString;
        }
        public static string GetStringsFromFile (string fileName,int numberString)
        {
            string stringFromFile = "";
            List<string> allStringsFromFile = new List<string>();
            if (File.Exists(fileName))
            {
                StreamReader streamReader = new StreamReader(fileName);
                
                while (!streamReader.EndOfStream)
                {
                    allStringsFromFile.Add(streamReader.ReadLine());
                }
                streamReader.Close();
                stringFromFile = allStringsFromFile[numberString];
            }
            else
            {
                MessageBox.Show("Не удалось зачитать файл " + fileName);
            }
            return stringFromFile;
        }
    }
}
