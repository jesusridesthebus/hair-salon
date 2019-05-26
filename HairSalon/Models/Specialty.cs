using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;


namespace HairSalon.Models
{
  public class Specialty
  {
    public string Name {get;set;}
    public int Id {get;set;}

    public Specialty(string name)
    {
      Name = name;
    }

    public override int GetHashCode()
    {
      return this.Id.GetHashCode();
    }

    public override bool Equals(System.Object otherSpecialty)
    {
      if(!(otherSpecialty is Specialty))
      {
        return false;
      }
      else
      {
        Specialty newSpecialty = (Specialty) otherSpecialty;
        bool nameEquality = (this.Name == newSpecialty.Name);
        bool idEquality = (this.Id == newSpecialty.Id);
        return (nameEquality && idEquality);
      }
    }

    public static List<Specialty> GetAll()
    {
      List<Specialty> specialties = new List<Specialty> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        Specialty newSpecialty = new Specialty(rdr.GetString(1));
        newSpecialty.Id = rdr.GetInt32(0);
        specialties.Add(newSpecialty);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return specialties;
    }

    public static void ClearAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties;";
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    // public static List<Specialty> SpecialtyByStylist(int stylistId)
    // {
    //   List<Specialty> specialties = new List<Specialty> {};
    //   MySqlConnection conn = DB.Connection();
    //   conn.Open();
    //   var cmd = conn.CreateCommand() as MySqlCommand;
    //   cmd.CommandText = @"SELECT"
    // }


  }
}

