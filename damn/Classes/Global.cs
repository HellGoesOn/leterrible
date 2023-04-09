using damn.Classes;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace damn
{
    public static class Global
    {
        static string ServName = @"DESKTOP-JB2KS99\SQLEXPRESS";

        public static string ConnectionString = 
            $@"Data Source={ServName}; Initial Catalog=oldtown; Integrated Security=True";

        public static SqlConnection Connection = new SqlConnection(ConnectionString);

        public static User User { get; set; }

        public static SqlDataReader Read(string command)
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();

            return new SqlCommand(command, Connection).ExecuteReader();
        }

        public static void NonQuery(string command)
        {
            if (Connection.State == ConnectionState.Closed)
                Connection.Open();

            new SqlCommand(command, Connection).ExecuteNonQuery();
        }

        public static void Finish()
        {
            if(Connection.State == ConnectionState.Open)
                Connection.Close();
        }

        public static void LoadAll<T>() where T : IHasList<T>
        {
            string query = $"select * from {typeof(T).Name + "s"}";

            var reader = Read(query);
            var list = Activator.CreateInstance<T>().GetList();

            while (reader.Read()) {

                object[] shitsAndGiggles = new object[reader.FieldCount];

                for(int i = 0; i < reader.FieldCount; i++) {
                    shitsAndGiggles[i] = reader.GetValue(i);
                }

                list.Add((T)Activator.CreateInstance(typeof(T), shitsAndGiggles));
            }

            Finish();
        }

        public static void Add<T>(params object[] parameters) where T :IHasList<T>
        {
            PropertyInfo[] info = typeof(T).GetProperties();

            string query = $"insert into [{typeof(T).Name + "s"}](";

            for (int i = 1; i < info.Length; i++) {
                //query += "'" + info[i].Name + "'" + (i == info.Length-1 ? ")" : ",");
                query += info[i].Name + (i == info.Length-1 ? ")" : ",");
            }

            query += " values(";

            for (int i = 0; i < parameters.Length; i++) {
                query += "'" + parameters[i].ToString() + (i == parameters.Length-1 ? "')" : "',");
            }

            NonQuery(query);
            Finish();
            Refresh();
        }

        public static void Update<T>(params object[] parameters) where T : IHasList<T>
        {
            PropertyInfo[] info = typeof(T).GetProperties();

            string query = $"update [{typeof(T).Name + "s"}] set ";

            for (int i = 1; i < info.Length; i++) {
                query += info[i].Name + "= '" + parameters[i].ToString() + (i == info.Length - 1 ? "' where " : "', ");
            }

            query += $"Id = {parameters[0]}";

            NonQuery(query);
            Finish();
            Refresh();
        }

        public static void Delete<T>(string condition) where T : IHasList<T>
        {
            string query = $"delete from [{typeof(T).Name + "s"}] where {condition}";
            NonQuery(query);
            Finish();
            Refresh();
        }

        public static void Refresh()
        {
            User.Users.Clear();
            MasterService.Links.Clear();
            Service.Services.Clear();
            Record.Records.Clear();
            Global.LoadAll<User>();
            Global.LoadAll<MasterService>();
            Global.LoadAll<Service>();
            Global.LoadAll<Record>();
        }
    }
}
