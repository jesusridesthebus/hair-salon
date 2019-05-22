using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HairSalon.Controllers;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistControllerTest
  {
    [TestMethod]
    public void Create_ReturnsCorrectActionType_RedirectToActionResult()
    {
      StylistsController controller = new StylistsController();
      IActionResult view = controller.Create("Billy Bob");
      Assert.IsInstanceOfType(view, typeof(RedirectToActionResult));
    }

    [TestMethod]
    public void Create_RedirectsToCorrectAction_Index()
    {
      StylistsController controller = new StylistsController();
      RedirectToActionResult actionResult = controller.Create("stylist face") as RedirectToActionResult;
      string result = actionResult.ActionName;
      Assert.AreEqual(result, "Index");
    }

    [TestMethod]
    public void Index_HasCorrectModelType_ClientList()
    {
      ClientsController controller = new ClientsController();
      ViewResult indexView = controller.Index() as ViewResult;
      var result = indexView.ViewData.Model;
      Assert.IsInstanceOfType(result, typeof(List<Client>));
    }
  }
}