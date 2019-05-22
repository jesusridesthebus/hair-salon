using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientControllerTest
  {
    [TestMethod]
    public void Create_ReturnsCorrectActionType_RedirectToActionResult()
    {
      ClientsController controller = new ClientsController();
      IActionResult view = controller.Create("Billy Bob", 1);
      Assert.IsInstanceOfType(view, typeof(RedirectToActionResult));
    }

    [TestMethod]
    public void Create_RedirectsToCorrectAction_Index()
    {
      ClientsController controller = new ClientsController();
      RedirectToActionResult actionResult = controller.Create("Billy Bob", 1) as RedirectToActionResult;
      string result = actionResult.ActionName;
      Assert.AreEqual(result, "Index");
    }
  }
}