using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Client
  {
    public string Name { get; set; }
    public int Id { get; set; }
    public int StylistId { get; set; }

    public Client (string name, int stylistId, int id = 0)
    {
      Name = name;
      StylistId = stylistId;
      Id = id;
    }

    public static List<Client> GetAll()
    {
      List<Client> allClients = new List<Client> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM clients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int clientId = rdr.GetInt32(0);
        string clientName = rdr.GetString(1);
        int clientStylistId = rdr.GetInt32(2);
        Client newClient = new Client(clientName, clientStylistId, clientId);
        allClients.Add(newClient);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allClients;
    }

    public override bool Equals(System.Object otherClient)
    {
      if(!(otherClient is Client))
      {
        return false;
      }
      else
      {
        Client newClient = (Client)otherClient;
        bool idEquality = this.Id == newClient.Id;
        bool nameEquality = this.Name == newClient.Name;
        bool stylistEquality = this.StylistId == newClient.StylistId;
        return(idEquality && nameEquality && stylistEquality);
      }
    }

    
  }
}