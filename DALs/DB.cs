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
        public void InsertDataToDB(TablesName tableType, List<string> listStringNameColumn, string weaponName, int damage)
        {
            SqlCommand sqlCommand = new SqlCommand("insert into " + tableType.ToString() +
                " (" + stringFromListToBase(listStringNameColumn) + ") values ('" + weaponName + "'," + damage + ")", sqlConnection);
            sqlCommand.ExecuteNonQuery();
        }

        public string stringFromListToBase(List<string> listString)
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
