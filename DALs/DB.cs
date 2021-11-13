using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DALs
{
    public enum TablesName
    {
        constitution,
        dress,
        heroes
    }
    public enum DressColumnName
    {
        name,
        type,
        protection,
        luck,
        speed,
        health,
        damage0,
        damage1
    }
    public enum ConstitutionColumnName
    {
        name, 
        race
    }
    public static class WhithEnume
    {
        public static string EnumeToString(TablesName enumeToString)
        {
            return enumeToString.ToString();
        }
        
    }
    public class DB
    {
        public string ConnectionString
        {
            get;
        } = "Data Source = "+ ServerName+ " ; Initial Catalog = Time-to-burn-fear; Integrated Security = True";
        private SqlConnection sqlConnection;
        public static string ServerName;
        public void Connect()
        {
            sqlConnection = new SqlConnection(ConnectionString);
            sqlConnection.Open();
        }
        public void Disconnect()
        {
            sqlConnection.Close();
        }
        /// <summary>
        /// Возвращает лист строк имен колонок указанной таблицы
        /// </summary>
        /// <param name="tablesName"></param>
        /// <returns></returns>
        public List<string> GetListStringNameColumn(TablesName tablesName)
        {
            if (tablesName==TablesName.dress)
                return new List<string>{"name","protection","luck","speed","health","damage0", "damage1", "type" ,"id"};
            if (tablesName == TablesName.constitution)
                return new List<string> { "name", "race", "id" };
            return new List<string>();
        }
        public List<string> GetListStringNameColumnOutId(TablesName tablesName)
        {
            if (tablesName == TablesName.dress)
                return new List<string> { "name", "protection", "luck", "speed", "health", "damage0", "damage1", "type" };
            if (tablesName == TablesName.constitution)
                return new List<string> { "name", "race", "id" };
            return new List<string>();
        }
        /// <summary>
        /// Зачитываем лист строк-значений из указанной колонки базы данных
        /// </summary>
        /// <param name="tablesName">имя таблицы</param>
        /// <param name="rowName">название поля, значения которого будут записаны в лист</param>
        /// <returns></returns>
        public List<string> GetListNamesFromBase(TablesName tablesName, string columnName)
        {
            Connect();
            List<string> listRowNameValue = new List<string>();
            SqlCommand sqlCommand = new SqlCommand("Select * from "+ tablesName.ToString(), sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                listRowNameValue.Add(sqlDataReader[columnName].ToString());
            }
            Disconnect();
            return listRowNameValue;
        }
        /// <summary>
        /// Записывает данные в базу 
        /// </summary>
        /// <param name="tablesName">Имя таблицы</param>
        /// <param name="listStringNameColumn">Лист названий имен столцов таблицы</param>
        /// <param name="listStringValue">Лист с данными, текст должен быть в одинарных кавычках</param>
        public void InsertDataToDB(TablesName tablesName, List<string> listStringValue)
        {
            List<string> listStringNameColumn = GetListStringNameColumn(tablesName);
            listStringValue.Add((GetMaxID(tablesName) +1).ToString());
            Connect();
            SqlCommand sqlCommand = new SqlCommand("insert into " + tablesName.ToString() +
                " (" + StringWihtCommaFromList(listStringNameColumn) + ") values (" + StringWihtCommaFromList(listStringValue) + ")", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            Disconnect();
        }
        /// <summary>
        /// Меняе данные в БД
        /// </summary>
        /// <param name="tablesName">имя таблицы</param>
        /// <param name="listStringValue">лист данных на которые происходит замена</param>
        /// <param name="columnName">имя столбца по которому выбирается строка для замены</param>
        /// <param name="targetData">искомое значение в columnName </param>
        public void ChangeDataInDB(TablesName tablesName, List<string> listStringValue, string columnName, string targetData)
        {
            List<string> listStringNameColumn = GetListStringNameColumnOutId(tablesName);
            
            Connect();
            //SqlCommand sqlCommand = new SqlCommand("update " + tablesName.ToString() +
            //    " set (" + StringWihtCommaFromList(listStringNameColumn) + ") values (" + StringWihtCommaFromList(listStringValue) +  ") where "+ columnName + " = '"+ targetData + "'", sqlConnection);
            SqlCommand sqlCommand = new SqlCommand("update " + tablesName.ToString() +
                " set " + StringForChangeFrom2Lists(listStringValue, listStringNameColumn) + " where " + columnName + " = '" + targetData + "'", sqlConnection);
            sqlCommand.ExecuteNonQuery();
            Disconnect();
        }
        /// <summary>
        /// Создает строку из двух листов для изменений данных в БД
        /// </summary>
        /// <param name="listStringValue">лист данных на которые происходит замена</param>
        /// <param name="listStringNameColumn">лист имен столбцов в которых будут менятся значения в БД</param>
        /// <returns></returns>
        public string StringForChangeFrom2Lists(List<string> listStringValue, List<string> listStringNameColumn)
        {
            string stringForChangeDB = string.Empty;
            for (int i = 0; i<listStringValue.Count();i++)
            {
                if (string.IsNullOrEmpty(stringForChangeDB))
                {
                    stringForChangeDB = listStringNameColumn[i]+" = "+listStringValue[i];
                }
                else
                {
                    stringForChangeDB = stringForChangeDB + ", "+ listStringNameColumn[i] + " = " + listStringValue[i];
                }
            }
            return stringForChangeDB;
        }
        /// <summary>
        /// Возвращает значение поля базы данных по заданному содержанию поля этой же записи
        /// </summary>
        /// <param name="tablesName"> имя таблицы </param>
        /// <param name="columnName"> имя параметра по которому будем искать</param>
        /// <param name="targetValueKey"> содержание параметра для поиска</param>
        /// <param name="targetParametrsName">Имя параметра содержание которго нужно вернуть</param>
        /// <returns></returns>
        public string GetValueFromDBWhere(TablesName tablesName,string columnName, string targetValueKey,string targetParametrsName)
        {
            string returnsString=string.Empty;
            Connect();
            SqlCommand sqlCommand = new SqlCommand("SELECT * from " + tablesName.ToString() + " WHERE "+ columnName+ "='"+ targetValueKey+"'", sqlConnection);
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
            while (sqlDataReader.Read())
            {
                returnsString = sqlDataReader[targetParametrsName].ToString();
            }
            Disconnect();
            return returnsString;
        }
        /// <summary>
        /// Возвращает максимальный ID  в интах
        /// </summary>
        /// <param name="tablesName">имя таблицы</param>
        /// <returns></returns>
        public int GetMaxID(TablesName tablesName)
        {
            int maxId=-1;
            try
            {
                Connect();
                SqlCommand sqlCommand = new SqlCommand("SELECT * from " + tablesName.ToString() + " WHERE Id=(SELECT MAX(Id) from " + tablesName.ToString() + ")", sqlConnection);
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    maxId = int.Parse(sqlDataReader["id"].ToString());
                }
                Disconnect();
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                WriteLog(ex.StackTrace);
            }
            return maxId;
        }
        /// <summary>
        /// Возвращает лист экземпляров Enum
        /// </summary>
        /// <param name="tablesName"></param>
        /// <returns></returns>
        public List<string> ListStringFromEnumColumnName(TablesName tablesName)
        { 
           if (tablesName==TablesName.dress)
                return Enum.GetValues(typeof(DressColumnName)).Cast<DressColumnName>().Select(v => v.ToString()).ToList();
           if (tablesName==TablesName.constitution)
                return Enum.GetValues(typeof(ConstitutionColumnName)).Cast<ConstitutionColumnName>().Select(v => v.ToString()).ToList();

            return new List<string>();
        }
        /// <summary>
        /// Делает строчку из листа строк в формате "string1, string2, string3 ..." 
        /// </summary>
        /// <param name="listString">лист строк</param>
        /// <returns></returns>
        public string StringWihtCommaFromList(List<string> listString)
        {
            string stringToBase=string.Empty;
            foreach(string stringInList in listString)
            {
                if (stringToBase != string.Empty)
                {
                    stringToBase = stringToBase + ", " + stringInList;
                }
                else
                {
                    stringToBase = stringInList;
                }
            }
            return stringToBase;
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
                " " + fixedTime.Hour + ".txt";
            return fileName;
        }
    }
}
