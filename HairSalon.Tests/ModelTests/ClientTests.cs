using Microsoft.VisualStudio.TestTools.UnitTesting;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.TestTools
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public void Dispose()
    {
      // Client.ClearAll();
    }

    public ClientTest()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=liz_kelley_test;";
    }

    [TestMethod]
    public void ClientConstructor_CreatesInstanceOfClient_Client()
    {
      Client newClient = new Client("test test", 1);
      Assert.AreEqual(typeof(Client), newClient.GetType());
    }


  }
}