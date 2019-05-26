using System.Collections.Generic;
using MySql.Data.MySqlClient;
using HairSalon;


namespace HairSalon.Models
{
  public class Specialty
  {
    public string Description {get;set;}
    public int Id {get;set;}

    public Specialty(string description, int id = 0)
    {
      Id = id;
      Description = description;
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
        bool descriptionEquality = (this.Description == newSpecialty.Description);
        bool idEquality = (this.Id == newSpecialty.Id);
        return (descriptionEquality && idEquality);
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

    public static Specialty Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM specialties WHERE id = @thisId;";
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int specialtyId = 0;
      string specialtyDescription = "";
      while(rdr.Read())
      {
        specialtyId = rdr.GetInt32(0);
        specialtyDescription = rdr.GetString(1);
      }
      Specialty newSpecialty = new Specialty(specialtyDescription, specialtyId);
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return newSpecialty;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO specialties (description) VALUES (@description);";
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@description";
      description.Value = this.Description;
      cmd.Parameters.Add(description);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Edit(string newDescription)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE specialties SET description = @newDescription WHERE id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = Id;
      cmd.Parameters.Add(searchId);
      MySqlParameter description = new MySqlParameter();
      description.ParameterName = "@newDescription";
      description.Value = newDescription;
      cmd.Parameters.Add(description);
      cmd.ExecuteNonQuery();
      Description = newDescription;
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM specialties WHERE id = @searchId; DELETE FROM stylists_specialties WHERE specialty_id = @searchId;";
      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = Id;
      cmd.Parameters.Add(searchId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public void AddStylist(Stylist newStylist)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";
      MySqlParameter stylistId = new MySqlParameter();
      stylistId.ParameterName = "@StylistId";
      stylistId.Value = newStylist.Id;
      cmd.Parameters.Add(stylistId);
      MySqlParameter specialtyId = new MySqlParameter();
      specialtyId.ParameterName = "@SpecialtyId";
      specialtyId.Value = Id;
      cmd.Parameters.Add(specialtyId);
      cmd.ExecuteNonQuery();
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Stylist> GetStylists()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT stylist.* FROM specialties JOIN stylists_specialties ON (specialty.id = stylists_specialties.specialty_id) JOIN stylist ON (stylists_specialties.stylist_id = stylist.id) WHERE specialty.id = @SpecialtyId;";
      MySqlParameter specialtyIdParam = new MySqlParameter();
      specialtyIdParam.ParameterName = "@SpecialtyId";
      specialtyIdParam.Value = Id;
      cmd.Parameters.Add(specialtyIdParam);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      List<Stylist> stylists = new List<Stylist> {};
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string description = rdr.GetString(1);
        Stylist newStylist = new Stylist(description);
        stylists.Add(newStylist);
      }
      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
      return stylists;
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

