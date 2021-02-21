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
                streamWriter.WriteLine("Швабра Тамары\tWeapon\t3\t0");
                streamWriter.WriteLine("Кухонный нож Валерии\tWeapon\t7\t0");
                streamWriter.WriteLine("Баскетбольное кольцо\tRing\t2\t10");
                streamWriter.WriteLine("Кольцо парашютное\tRing\t1\t3");
                streamWriter.WriteLine("Перчатки Валерии\tGloves\t2\t3");
                streamWriter.WriteLine("Ладони Иванова\tGloves\t5\t3");
                streamWriter.WriteLine("Прическа Котова\tHeaddress\t4\t2");
                streamWriter.WriteLine("Шляпа Верина\tHeaddress\t9\t3");
                streamWriter.WriteLine("Трусы Саломоныча\tLeggings\t2\t5");
                streamWriter.WriteLine("Шаровары Вячеслава\tLeggings\t6\t1");
                streamWriter.WriteLine("Жилетка колесова\tBodyArmor\t2\t5");
                streamWriter.WriteLine("Бюстгалтер Аллочки\tBodyArmor\t10\t10");
                streamWriter.WriteLine("Сапоги ухода домой\tBoots\t10\t0");
                streamWriter.WriteLine("Профит Хвостиченко\tBoots\t0\t0");
                streamWriter.Close();
            }
        }
        public static void LoadCharToFileFirstStart()
        {
            if (!File.Exists(Constants.CHARS_FILE_NAME))
            {
                StreamWriter streamWriter = new StreamWriter(Constants.CHARS_FILE_NAME, false);
                streamWriter.WriteLine("Юдин И.А.\tHuman");
                streamWriter.WriteLine("Азовцев А.Ю.\tHuman");
                streamWriter.Close();
            }
        }
    }
}
