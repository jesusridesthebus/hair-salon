using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class ClientsController : Controller

  {
    [HttpGet("/clients")]
    public ActionResult Index()
    {
      List<Client> allClients = Client.GetAll();
      return View(allClients);
    }

    // [HttpGet("/clients")]
    // public ActionResult Create(string name)
    // {
    //   Client newClient = new Client(name);
    //   newClient.Save();
    //   List<Client> allClients = Client.GetAll();
    //   return View("Index", allClients);
    // }

    [HttpGet("/stylists/{stylistId}/clients/new")]
    public ActionResult New(int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      return View(stylist);
    }

    [HttpPost("/clients")]
    public ActionResult Create(string name, int stylistId, int id = 0)
    {
      Client newClient = new Client(name, stylistId, id);
      newClient.Save();
      List<Client> allClients = Client.GetAll();
      return View("Index", allClients);
    }

    // [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
    // public ActionResult Edit(int clientId)
    // {
    //   Client client = Client.Find(clientId);
    //   return View(client);
    // }


  }
}