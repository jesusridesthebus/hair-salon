using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=liz_kelley_test;";
    }

    public void Dispose()
    {
      Stylist.ClearAll();
      Client.ClearAll();
    }

    [TestMethod]
    public void StylistContructor_CreatesInstanceOfStylist_Stylist()
    {
      Stylist newStylist = new Stylist("test stylist");
      Assert.AreEqual(typeof(Stylist), newStylist.GetType());
    }

    [TestMethod]
    public void GetName_ReturnsName_String()
    {
      string name = "Test Stylist";
      Stylist newStylist = new Stylist(name);
      string result = newStylist.Name;
      Assert.AreEqual(name, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreSame_Stylist()
    {
      Stylist firstStylist = new Stylist("Stylist Face");
      Stylist secondStylist = new Stylist("Stylist Face");
      Assert.AreEqual(firstStylist, secondStylist);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_StylistList()
    {
      Stylist testStylist = new Stylist("Freaky Styley");
      testStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{testStylist};
      CollectionAssert.AreEqual(testList, result);
    }


  }
}