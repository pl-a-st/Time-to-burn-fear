using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Time_to_burn_fear
{
    public enum ValueType
    {
        Int,
        String,
        Bool
    }
    public class DAO
    {
       public enum cocorrectness
        {
            Correctly,
            Incorrect
        }
        /// <summary>
        /// Записать одежду в базу
        /// </summary>
        /// <param name="name"></param>
        /// <param name="typeDress"></param>
        /// <param name="fistParametr"></param>
        /// <param name="secondParameter"></param>
        public static void AddDressToBase(string name, string typeDress, int fistParametr, int secondParameter)
        {
            string connectionString =
                "Data Source = "+ Constants.ServerName+"; Initial Catalog = Time-to-burn-fear; " +
                "Integrated Security = True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            int id = 0;
            SqlCommand sqlCommand;
            sqlCommand = new SqlCommand("SELECT MAX(id) as max FROM [Time-to-burn-fear].[dbo].[dress]", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            sqlDataReader.Read();
            if (!(sqlDataReader["max"] is DBNull))
                id = Convert.ToInt32(sqlDataReader["max"])+1;
            sqlDataReader.Close();
            sqlCommand = new SqlCommand("insert into dress (id, name, type_dress, first_parameter, second_parameter" +
                ") values ("+id+", '" +name+"', '"+ typeDress + "', " + fistParametr + ", "+ secondParameter +  ")", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }
        public cocorrectness Cocorrectness
        { get; private set; } = cocorrectness.Correctly;
        /// <summary>
        /// Записать строку в файл
        /// </summary>
        /// <param name="addingString">записываемая строка</param>
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
        /// Возвращает строку по указаному номеру в листе
        /// </summary>
        /// <param name="nameBase"></param>
        /// <param name="tableName"></param>
        /// <param name="baseColumnName"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string GetStringsByNumberFromBase(string nameBase, string tableName, string[] baseColumnName, int number)
        {
            return GetListStringsFromBase(nameBase, tableName, baseColumnName)[number];
        }
        /// <summary>
        /// Создает лист строк из указанной таблицы в базе, для совмещения с строка записывается через \t
        /// </summary>
        /// <param name="nameBase"></param>
        /// <param name="tableName"></param>
        /// <param name="baseColumnName"> необходимые поля из базы </param>
        /// <returns></returns>
        public static List<string> GetListStringsFromBase(string nameBase, string tableName, string []baseColumnName)
        {
            List<string> listStringFromBase = new List<string>();
            string connectionString =
            "Data Source = " + Constants.ServerName + "; Initial Catalog = "+nameBase+"; Integrated Security = True";
            try
            {
                SqlConnection sqlConnection = new SqlConnection(connectionString);
                SqlCommand sqlCommand = new SqlCommand("Select * from " + tableName, sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    string stringForList = string.Empty;
                    foreach (string columnName in baseColumnName)
                    {
                        if (stringForList != string.Empty)
                            stringForList = stringForList + "\t";
                        stringForList = stringForList + sqlDataReader[columnName].ToString();
                    }
                    listStringFromBase.Add(stringForList);
                }
                sqlConnection.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("не удалось обратиться к таблице: " + tableName + " в базе " + nameBase);
                WriteLog(ex.Message);
                WriteLog(ex.StackTrace);
            }
            return listStringFromBase;
        }
        public static void ChangeDressInBase(string nameBase, string tableName, (string columnName, ValueType valueType) [] baseColumnName, 
            int index, string stringToBase)
        {
            List<string> listStringFromBase = new List<string>();
            string connectionString =
            "Data Source = " + Constants.ServerName + "; Initial Catalog = " + nameBase + "; Integrated Security = True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            for (int i=0;i<baseColumnName.Length;i++)
            {
                if (baseColumnName[i].valueType == ValueType.String)
                {
                    SqlCommand sqlCommand = new SqlCommand("update " + tableName + " set " + baseColumnName[i].columnName + " = '"+ 
                        stringToBase.Split('\t')[i] + "' where id = "+ index, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }
                else
                {
                    SqlCommand sqlCommand = new SqlCommand("update " + tableName + " set " + baseColumnName[i].columnName + " = '" +
                        stringToBase.Split('\t')[i] + "' where id = " + index, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                }
            }
            sqlConnection.Close();
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
        public static List<Dress> ListDresses
        { get; private set; }
        public static void GetListDressFromBase()
        {
            string connectionString =
            "Data Source = " + Constants.ServerName + "; Initial Catalog = Time-to-burn-fear; " +
                "Integrated Security = True";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand("Select * from Dress", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                Console.WriteLine(sqlDataReader["name"].ToString() + " " +
                    sqlDataReader["surname"].ToString() + " " +
                    sqlDataReader["patronymic_name"].ToString());
            }
            sqlConnection.Close();

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
