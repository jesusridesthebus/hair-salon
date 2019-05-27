using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;

namespace HairSalon.Controllers
{
  public class StylistsController : Controller
  {
    [HttpGet("/stylists")]
    public ActionResult Index()
    {
      List<Stylist> allStylists = Stylist.GetAll();
      return View(allStylists);
    }

    [HttpGet("/stylists/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/stylists")]
    public ActionResult Create(string stylistName)
    {
      Stylist newStylist = new Stylist(stylistName);
      newStylist.Save();
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{stylistId}")]
    public ActionResult Show(int stylistId, string clientName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist selectedStylist = Stylist.Find(stylistId);
      List<Specialty> specialtyDescription = selectedStylist.GetSpecialties();
      List<Specialty> specialty = Specialty.GetAll();
      List<Client> allClients = selectedStylist.GetClients();
      model.Add("stylist", selectedStylist);
      model.Add("clients", allClients);
      model.Add("specialty", specialty);
      return View("Show", model);
    }

    [HttpPost("/stylists/{stylistId}/clients/new")]
    public ActionResult AddClient(int stylistId, int clientId)
    {
      Stylist stylist = Stylist.Find(stylistId);
      Client client = Client.Find(clientId);
      stylist.AddClient(client);
      return RedirectToAction("Show", new { Id = stylistId });
    }

    [HttpPost("/stylists/{stylistId}/clients")]
    public ActionResult Create(int stylistId, string clientName)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist foundStylist = Stylist.Find(stylistId);
      Client newClient = new Client(clientName, stylistId);
      newClient.Save();
      foundStylist.AddClient(newClient);
      List<Client> stylistClients = foundStylist.GetClients();
      model.Add("clients", stylistClients);
      model.Add("stylist", foundStylist);
      return View("Show", model);
    }

    [HttpPost("/stylist/{stylistId}")]
    public ActionResult DeleteStylist(int stylistId)
    {
      Stylist client = Stylist.Find(stylistId);
      client.DeleteStylist(stylistId);
      Stylist newStylist = new Stylist("thing");
      List<Stylist> allStylists = Stylist.GetAll();
      return View("Index", allStylists);
    }

    [HttpGet("/stylists/{stylistId}/delete")]
    public ActionResult Delete(int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist client = Stylist.Find(stylistId);
      model.Add("stylist", client);
      return View(model);
    }

    [HttpGet("/stylists/{stylistId}/edit")]
    public ActionResult EditStylist(int stylistId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Stylist stylist = Stylist.Find(stylistId);
      model.Add("stylist", stylist);
      return View(model);
    }

    // [HttpPost("/stylists/{stylistId}")]
    // public ActionResult Edit(int stylistId, string editedName)
    // {
    //   Stylist stylist = Stylist.Find(stylistId);
    //   stylist.Edit(stylistId, editedName);
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   List<Client> stylistClients = stylist.Clients;
    //   List<Client> allClients = Client.GetAll();
    //   model.Add("allClients", allClients);
    //   model.Add("clients", stylistClients);
    //   model.Add("stylist", stylist);
    //   return View("Show", model);
    // }
  }
}