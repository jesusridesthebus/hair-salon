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
  }
}