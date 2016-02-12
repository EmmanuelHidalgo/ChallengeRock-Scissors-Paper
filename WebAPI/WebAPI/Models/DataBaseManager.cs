using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; 
using System.Data.SqlClient;

namespace WebAPI.Models
{
    public class DataBaseManager
    {
        private String route = "Data Source = (local); Initial Catalog=ChGameDB; Integrated Security=true";
        private SqlConnection objConection;
        private SqlCommandBuilder cmb;
        private DataSet dataSet = new DataSet();
        private SqlDataAdapter dataAdapter;
        private SqlCommand command; 

        public void connect() 
        {
            objConection = new SqlConnection(route);
        }

        public void insertPlayer(Player player) 
        {
            connect();
            int score = 0;
            Boolean flag = false;
            List<Player> AllplayerList = new List<Player>();
            AllplayerList = this.getAllPlayers();
            for (int i = 0; i < AllplayerList.Count;i++ )
            {
                if (AllplayerList[i].name.Equals(player.name))
                {
                    flag = true;
                    score = int.Parse(AllplayerList[i].strategy) + 3;
                    break;
                }
                else
                {
                    flag = false;
                    
                }
                
            } 

            if(flag==true){

                this.updatePlayer(player, score);
            } 
            if(flag==false)
            {
                objConection.Open();
                String instruction = "INSERT INTO Player(name,score)VALUES(" + "'" + player.name + "'," + 3 + ");";
                command = new SqlCommand(instruction, objConection);
                command.ExecuteNonQuery();
                objConection.Close();
            }
            
        }




        public void insert2Place(Player player)
        {
            connect();
            int score = 0;
            Boolean flag = false;
            List<Player> AllplayerList = new List<Player>();
            AllplayerList = this.getAllPlayers();
            for (int i = 0; i < AllplayerList.Count; i++)
            {
                if (AllplayerList[i].name.Equals(player.name))
                {
                    flag = true;
                    score = int.Parse(AllplayerList[i].strategy) + 1;
                    break;
                }
                else
                {
                    flag = false;

                }

            }

            if (flag == true)
            {

                this.updatePlayer(player, score);
            }
            if (flag == false)
            {
                objConection.Open();
                String instruction = "INSERT INTO Player(name,score)VALUES(" + "'" + player.name + "'," + 1 + ");";
                command = new SqlCommand(instruction, objConection);
                command.ExecuteNonQuery();
                objConection.Close();
            }

        }












        public void getReset() 
        {
            connect();
            objConection.Open();
            String instruction = "Truncate Table [ChGameDB].[dbo].[Player]; ";
            command = new SqlCommand(instruction, objConection);
            command.ExecuteNonQuery();
            objConection.Close();
        }

        public List<Player> getTopTen() 
        {
            connect();
            objConection.Open();
            List<Player> playerList= new List<Player>();
            SqlCommand command = new SqlCommand("SELECT TOP 10  [name],[score] FROM [ChGameDB].[dbo].[Player] ORDER BY [ChGameDB].[dbo].[Player].score DESC;", objConection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
               {
                   while (dataReader.Read())
            {
                    String name =dataReader.GetString(0); 
                    String strategy=dataReader.GetInt32(1).ToString();  
                    Player p = new Player();
                    p.name=name; 
                    p.strategy=strategy; 
                    playerList.Add(p);
            }
        }
            objConection.Close();
  
            return playerList;     
        }


        public List<Player> getAllPlayers()
        {
            connect();
            objConection.Open();
            List<Player> playerList = new List<Player>();
            SqlCommand command = new SqlCommand("SELECT  [name],[score] FROM [ChGameDB].[dbo].[Player] ;", objConection);
            SqlDataReader dataReader = command.ExecuteReader();
            if (dataReader.HasRows)
            {
                while (dataReader.Read())
                {
                    String name = dataReader.GetString(0);
                    String strategy = dataReader.GetInt32(1).ToString();
                    Player p = new Player();
                    p.name = name;
                    p.strategy = strategy;
                    playerList.Add(p);
                }
            }
            objConection.Close();

            return playerList;
        }

        public void updatePlayer(Player player, int score) 
        {
            objConection.Open();
            String instruction = " UPDATE [ChGameDB].[dbo].[Player]  SET [Player] .score =" + score + "WHERE [Player].name ="+"'"+player.name+"';";
            command = new SqlCommand(instruction, objConection);
            command.ExecuteNonQuery();
            objConection.Close();
        }

    }
}