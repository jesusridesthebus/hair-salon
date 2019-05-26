using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

namespace HairSalon.Controllers
{
  public class SpecialtyController : Controller
  {
    [HttpGet("/specialties")]
    public ActionResult Index()
    {
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View(allSpecialties);
    }

    [HttpGet("/specialties/new")]
    public ActionResult New()
    {
      return View();
    }

    [HttpPost("/specialties")]
    public ActionResult Create(string description)
    {
      Specialty newSpecialty = new Specialty(description);
      newSpecialty.Save();
      List<Specialty> allSpecialties = Specialty.GetAll();
      return View("Index", allSpecialties);
    }

    [HttpGet("/specialties/{id}")]
    public ActionResult Show(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty selectedSpecialty = Specialty.Find(id);
      List<Specialty> specialty = Specialty.GetAll();
      List<Stylist> stylistName = selectedSpecialty.GetStylists();
      List<Stylist> allStylists = Stylist.GetAll();
      model.Add("specialty", specialty);
      model.Add("selectedSpecialty", selectedSpecialty);
      model.Add("stylistName", stylistName);
      model.Add("allStylists", allStylists);
      return View(model);
    }

    [HttpGet("/specialties/{specialtyId}/edit")]
    public ActionResult Edit(int specialtyId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Specialty specialty = Specialty.Find(specialtyId);
      model.Add("specialty", specialty);
      return View(model);
    }

    [HttpPost("/specialties/{specialtyId}/delete")]
    public ActionResult Delete(int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      specialty.Delete();
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("specialty", specialty);
      return View(model);
    }

    [HttpPost("/specialties/{specialtyId}")]
    public ActionResult Update(string description, int specialtyId)
    {
      Specialty specialty = Specialty.Find(specialtyId);
      specialty.Edit(description);
      Dictionary<string, object> model = new Dictionary<string, object>();
      model.Add("specialty", specialty);
      return View("Show", model);
    }
  }
}