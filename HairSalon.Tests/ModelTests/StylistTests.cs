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

    [TestMethod]
    public void Save_DatabaseAssignsIdToCategory_Id()
    {
      Stylist testStylist = new Stylist("Freaky Styleyyy");
      testStylist.Save();
      Stylist savedStylist = Stylist.GetAll()[0];
      int result = savedStylist.Id;
      int testId = testStylist.Id;
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void GetAll_ReturnsAllStylistObjects_StylistList()
    {
      string name1 = "Stylist";
      string name2 = "Face";
      Stylist newStylist1 = new Stylist(name1);
      newStylist1.Save();
      Stylist newStylist2 = new Stylist(name2);
      newStylist2.Save();
      List<Stylist> newList = new List<Stylist> { newStylist1, newStylist2 };
      List<Stylist> result = Stylist.GetAll();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsStylistInDatabase_Stylist()
    {
      Stylist testStylist = new Stylist("Stylist Face");
      testStylist.Save();
      Stylist foundStylist = Stylist.Find(testStylist.Id);
      Assert.AreEqual(testStylist, foundStylist);
    }

    [TestMethod]
    public void GetClients_ReturnsEmptyClientList_ClientList()
    {
      string name = "stylistface";
      Stylist newStylist = new Stylist(name);
      List<Client> newList = new List<Client> { };
      List<Client> result = newStylist.GetClients();
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void GetAll_StylistsEmptyAtFirst_List()
    {
      int result = Stylist.GetAll().Count;
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void GetClients_RetrievesAllClientsWithStylist_ClientList()
    {
      Stylist testStylist = new Stylist("styley mcStyle");
      testStylist.Save();
      Client firstClient = new Client("face face", testStylist.Id);
      firstClient.Save();
      Client secondClient = new Client("face mcFace", testStylist.Id);
      secondClient.Save();
      List<Client> testClientList = new List<Client> {firstClient, secondClient};
      List<Client> resultClientList = testStylist.GetClients();
      CollectionAssert.AreEqual(testClientList, resultClientList);
    }
  }
}