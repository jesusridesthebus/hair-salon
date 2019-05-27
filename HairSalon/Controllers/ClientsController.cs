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

    // [HttpPost("/clients")]
    // public ActionResult Create(string name, int stylistId, int id = 0)
    // {
    //   Client newClient = new Client(name, stylistId, id);
    //   newClient.Save();
    //   List<Client> allClients = Client.GetAll();
    //   return View("Index", allClients);
    // }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Show(int clientId, int stylistId)
    {
      Client client = Client.Find(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("client", client);
      model.Add("stylist", stylist);
      return View(model);
    }

    [HttpGet("/stylists/{stylistId}/clients/{clientId}/edit")]
    public ActionResult Edit(int clientId, int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      Client client = Client.Find(clientId);
      model.Add("stylists", stylist);
      model.Add("clients", client);
      return View(model);
    }

    [HttpPost("/stylists/{stylistId}/clients/{clientId}")]
    public ActionResult Update(int clientId, int stylistId, string clientName)
    {
      Client client = Client.Find(clientId);
      if(clientName == "")
      {
        clientName = client.Name;
      }
      Dictionary<string, object> model = new Dictionary<string, object>();
      client.Edit(clientName);
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("client", client);
      model.Add("stylist", stylist);
      return View("show", model);
    }

    [HttpGet("/client/{clientId}/delete")]
    public ActionResult DeleteClient(int clientId)
    {
      Client client = Client.Find(clientId);
      client.DeleteClient(clientId);
      List<Client> allClients = Client.GetAll();
      return RedirectToAction("Index", allClients);
    }

    [HttpPost("/stylists/{stylistId}/clients/{clientId}/delete")]
    public ActionResult Delete(int clientId, int stylistId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Client client = Client.Find(clientId);
      client.DeleteClient(clientId);
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Client> clientName = stylist.GetClients();
      model.Add("stylists", stylist);
      model.Add("clients", clientName);
      return View(model);
    }


  }
}