using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;



namespace WebAPI.Controllers
{
    public class TournamentController : ApiController
    {
        Player player = new Player();
        DataBaseManager dataBase = new DataBaseManager();
        /// <summary>
        /// Receives the data file that contains all players and make all the tournamet process. 
        /// </summary>
        /// <param name="dataFile">contains all players</param>
        /// <returns>return the player that won the tournament</returns>
         [System.Web.Http.AcceptVerbs("POST", "GET")]
        public Player GetWinner(string dataFile) 
        {
            String dataFile1 = dataFile;
            Player player = new Player();
            return player = fillPlayerList(dataFile1);
        }

        private Player fillPlayerList(string dataFile)
         {
             Player player = new Player();
           
               
                Boolean flag = false;
              

                List<string> rawData = dataFile.Split(new char[] { '[', ',', ']', ' ', '"', '}', '{' }).ToList();
                List<string> dataPlayers = new List<string>();
                List<Player> playerList = new List<Player>();
                for (int i = 0; i < rawData.Count; i++)
                {
                    if (!rawData[i].Equals(""))
                    {
                        dataPlayers.Add(rawData[i]);
                    }
                }
                for (int j = 0; j < dataPlayers.Count; j += 2)
                {
                    String name = dataPlayers[j];
                    String strategy = dataPlayers[j + 1].ToUpper();
                    playerList.Add(new Player { name = name, strategy = strategy });
                }

                for (int x = 0; x < playerList.Count; x++)
                {
                    if (!playerList[x].strategy.Equals("R") && !playerList[x].strategy.Equals("P") && !playerList[x].strategy.Equals("S"))
                    {

                        flag = true;
                        break;

                    }

                }
                if (flag == false)
                {
                    player = tournament(playerList); //return the winner of the tournament
                    dataBase.insertPlayer(player);


                }
                if (flag == true)
                {
                    String name = "One player of the tournament has an wrong strategy";
                    String strategy = "Please check the file again, and remember, only the letters P(Paper),R(Rock) and S(scissors) are allowed";
                    player = new Player { name = name, strategy = strategy };
                }

                return player;//here comes an exception

          
           

            return player;

           
        }


        private Player tournament(List<Player> playerList)
        {
            List<Player> winningPlayerList1 = new List<Player>();
            List<Player> winningPlayerList2 = new List<Player>();
            Player champion = new Player();

            for (int i = 0; i <= playerList.Count + 1; i++)
            {
                if (playerList == null)
                {
                    break;
                }
                Player player1 = playerList[0];
                Player player2 = playerList[1];
                Player winner = playerVsPlayer(player1, player2);
                winningPlayerList1.Add(winner);
                playerList.Remove(player1);
                playerList.Remove(player2);
            }

            for (int i = 0; i <= winningPlayerList1.Count + 1; i++)
            {
                if (winningPlayerList1 == null)
                {
                    break;
                }
                Player player1 = winningPlayerList1[0];
                Player player2 = winningPlayerList1[1];
                Player winner = playerVsPlayer(player1, player2);
                winningPlayerList2.Add(winner);
                winningPlayerList1.Remove(player1);
                winningPlayerList1.Remove(player2);
            }

            Player finalist1 = winningPlayerList2[0];
            Player finalist2 = winningPlayerList2[1];
            champion = playerVsPlayer(finalist1, finalist2);
            if(!finalist1.name.Equals(champion.name))
            {
                dataBase.insert2Place(finalist1);
            }
            if (!finalist2.name.Equals(champion.name))
            {
                dataBase.insert2Place(finalist2);
            }
            return champion;
        }

        private Player playerVsPlayer(Player player1, Player player2)
        {
            Player winner = new Player();

            if (player1.strategy.Equals("P"))
            {

                if (player2.strategy.Equals("P"))
                {

                    winner = player1;
                }
                else
                {
                    if (player2.strategy.Equals("S"))
                    {

                        winner = player2;
                    }
                    if (player2.strategy.Equals("R"))
                    {

                        winner = player1;
                    }
                }
            }

            if (player1.strategy.Equals("R"))
            {

                if (player2.strategy.Equals("R"))
                {

                    winner = player1;
                }
                else
                {
                    if (player2.strategy.Equals("P"))
                    {

                        winner = player2;
                    }
                    if (player2.strategy.Equals("S"))
                    {

                        winner = player1;
                    }
                }
            }

            if (player1.strategy.Equals("S"))
            {

                if (player2.strategy.Equals("S"))
                {

                    winner = player1;
                }
                else
                {
                    if (player2.strategy.Equals("R"))
                    {

                        winner = player2;
                    }
                    if (player2.strategy.Equals("P"))
                    {

                        winner = player1;
                    }
                }
            }

            return winner;
        }

        /// <summary>
        /// Provides the top 10 players that are stored in the data base. 
        /// </summary>
        /// <returns>return the player that won the tournament</returns>
        [System.Web.Http.AcceptVerbs("GET")]
        public List<Player> getTopTen() 
        {
            return dataBase.getTopTen();
        }

        /// <summary>
        /// Deletes all player from the data base. 
        /// </summary>
        /// <returns>return nothing</returns>
        [System.Web.Http.AcceptVerbs("DELETE")]
        public void resetTournament() 
        {
            this.dataBase.getReset();
        }
        

    }
}
