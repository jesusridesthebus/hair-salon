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
      IActionResult view = controller.Create("Billy Bob");
      Assert.IsInstanceOfType(view, typeof(RedirectToActionResult));
    }
  }
}