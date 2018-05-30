using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using StartWebSiteEnglish.Models;

public class SqlQueries
{
    //static string connectionString = "workstation id=EnglishDB.mssql.somee.com;packet size=4096;user id=kuzmenkovdmitrii_SQLLogin_1; pwd=9khilj75m4;data source=EnglishDB.mssql.somee.com;persist security info=False;initial catalog=EnglishDB";

    static string connectionString = @"workstation id=SiteEnglishCloudDB.mssql.somee.com;
         packet size=4096;
         user id=dssxsasdaaaa_SQLLogin_1;
         pwd=etfwa5iczq
         ;data source=SiteEnglishCloudDB.mssql.somee.com;
         persist security info=False;
         initial catalog=SiteEnglishCloudDB";

    private static void CreateDatabase(string fullTableName)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "CREATE TABLE " + fullTableName + "([ID] int)";
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    private static void RenameDatabase(string curentName, string rename)
    {
        string DbName = "SiteEnglishCloudDB";
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "USE "+ DbName + " EXEC sp_rename '" + curentName + "', '" + rename + "'";
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    private static void AddIdToDatabase(string fullTableName, int id)
    {
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            connection.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "INSERT INTO " + fullTableName + " VALUES (@Id)";
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = id;
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    private static List<LearnedWord> ReadDatabase(string fullTableName)
    {
        List<LearnedWord> outList = new List<LearnedWord>();
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            string sqlExpression = "SELECT * FROM " + fullTableName;
            connection.Open();
            SqlCommand cmd = new SqlCommand(sqlExpression, connection);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows) // если есть данные
            {
                while (reader.Read()) // построчно считываем данные
                {
                    outList.Add(new LearnedWord { Id = (int)reader.GetValue(0) });
                }
            }
        }
        return outList;
    }

    public static void CreateDatabases(string userName)
    {
        CreateDatabase(userName + "Word");
        CreateDatabase(userName + "Adjective");
        CreateDatabase(userName + "GrammerText");
        CreateDatabase(userName + "MaterialText");
    }

    public static void RenameDatabases(string curentName, string rename)
    {
        RenameDatabase(curentName + "Word", rename + "Word");
        RenameDatabase(curentName + "Adjective", rename + "Adjective");
        RenameDatabase(curentName + "GrammerText", rename + "GrammerText");
        RenameDatabase(curentName + "MaterialText", rename + "MaterialText");
    }

    public static void AddIdToWordDatabase(string userName, int id)
    {
        AddIdToDatabase(userName + "Word", id);
    }

    public static void AddIdToAdjectiveDatabase(string userName, int id)
    {
        AddIdToDatabase(userName + "Adjective", id);
    }

    public static void AddIdToGrammerTextDatabase(string userName, int id)
    {
        AddIdToDatabase(userName + "GrammerText", id);
    }

    public static void AddIdToMaterialTextDatabase(string userName, int id)
    {
        AddIdToDatabase(userName + "MaterialText", id);
    }

    public static List<LearnedWord> ReadWordDatabase(string userName)
    {
        return ReadDatabase(userName + "Word");
    }

    public static List<LearnedWord> ReadAdjectiveDatabase(string userName)
    {
        return ReadDatabase(userName + "Adjective");
    }

    public static List<LearnedWord> ReadGrammerTextTextDatabase(string userName)
    {
        return ReadDatabase(userName + "GrammerText");
    }

    public static List<LearnedWord> ReadMaterialTextDatabase(string userName)
    {
        return ReadDatabase(userName + "MaterialText");
    }
}