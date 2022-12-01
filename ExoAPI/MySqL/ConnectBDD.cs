using ExoAPI.Entitie;
using ExoAPI.Type;
using MySql.Data.MySqlClient;

namespace ExoAPI.MySqL
{
    // just for excemple not used
    public class ConnectBDD
    {
        public static MySqlConnection Connect()
        {
            string server = "localhost";
            string database = "testapi";
            string username = "root";
            string password = "";
            string cs = @$"server={server};userid={username};password={password};database={database}";
            MySqlConnection connect = new MySqlConnection(cs);
            return connect;
        }
        public static MySqlConnection Desconnect()
        {
            MySqlConnection desconnect = null;
            return desconnect;
        }
        public void ProductsData()
        {
            MySqlConnection con = Connect();
            con.Open();
            string stm = "SELECT * FROM products_data";
            MySqlCommand cmd = new MySqlCommand(stm, con);

            using MySqlDataReader rdr = cmd.ExecuteReader();

            //while (rdr.Read())
            //{
            //    Console.WriteLine("{0} {1} {2} {3} {4}", rdr.GetInt32(0), rdr.GetString(1),
            //    rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(4));
            //}
            con = Desconnect();
        }
        public void AddData(Product product)
        {
            int id = product.Id;
            string origin = product.Origin;
            string name = product.Name;
            int quantity = product.Quantite;
            Usages usage = product.Usage;
            MySqlConnection conn = Connect();
            conn.Open();
            string table = "product_data";
            string request = $"INSERT INTO {table} (id,origin,name,quantity,usage) VALUES({id},{origin},{name},{quantity},{usage})";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = request;
            cmd.ExecuteNonQuery();

        }
    }
}
