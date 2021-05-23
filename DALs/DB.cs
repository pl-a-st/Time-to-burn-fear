using System;
using System.Collections.Generic;
using System.Linq;
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
        type_dress,
        first_parameter,
        second_parameter
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
        } = "Data Source = "+ ServerName+" ; Initial Catalog = TimeToBurnFear; Integrated Security = True";
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
        public List<string> GetListStringNameColumn(TablesName tablesName)
        {
            if (tablesName==TablesName.dress)
                return new List<string>{"name","protection","luck","speed","health","damage","type"};
            return new List<string>();
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
            SqlCommand sqlCommand = new SqlCommand("insert into " + tablesName.ToString() +
                " (" + StringWihtCommaFromList(listStringNameColumn) + ") values (" + StringWihtCommaFromList(listStringValue) + ")", sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }
        public void InsertDataToDB(TablesName tablesName, string weaponName, int damage)
        {
            List<string> listStringNameColumn = ListStringFromEnumColumnName(tablesName);
            SqlCommand sqlCommand = new SqlCommand("insert into " + tablesName.ToString() +
                " (" + StringWihtCommaFromList(listStringNameColumn) + ") values ('" + weaponName + "'," + damage + ")", sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }
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
    }
}
