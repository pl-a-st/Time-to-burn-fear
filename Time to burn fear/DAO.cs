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
       public enum cocorrectness
        {
            Correctly,
            Incorrect
        }
        public cocorrectness Cocorrectness
        { get; private set; } = cocorrectness.Correctly;
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
                if (numberString < 0)
                    return string.Empty;
                stringFromFile = allStringsFromFile[numberString];
            }
            else
            {
                MessageBox.Show("Не удалось зачитать файл " + fileName);
            }
            return stringFromFile;
        }
        public static void WriteLog(string message)
        {
            DateTime fixedTime = DateTime.Now;
            string logName = GetLogName(fixedTime);
            message = fixedTime + "\t" + message;
            StreamWriter sr = new StreamWriter(logName, true);
            sr.WriteLine(message);
            sr.Flush();
            sr.Close();
        }
        public static string GetLogName(DateTime fixedTime)
        {
            string fileName = string.Empty;
            fileName = fixedTime.Year + "_" + fixedTime.Month + "_" + fixedTime.Day +
                " " + fixedTime.Hour +  ".txt";
            return fileName;
        }

        public static void LoadCutToFile()
        {
            if (!File.Exists(Constants.THING_FILE_NAME))
            {
                StreamWriter streamWriter = new StreamWriter(Constants.THING_FILE_NAME, false);
                streamWriter.WriteLine(Constants.CUT_DRESS_NAME);
                streamWriter.Close();
            }
        }
    }
}
