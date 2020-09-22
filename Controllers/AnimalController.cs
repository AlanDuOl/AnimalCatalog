using AnimalCatalogSqLite.Repositories;
using AnimalCatalogSqLite.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging.Console.Internal;
using System.Collections.Generic;
using System.Diagnostics;
using M = AnimalCatalogSqLite.Models;


namespace AnimalCatalogSqLite.Controllers
{
    [Route("Animal")]
    public class AnimalController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        [Authorize(Roles = "User")]
        [HttpGet("Index")]
        public IActionResult Index()
        {
            List<M.Animal> animals = _animalRepository.GetAnimals();
            return View(animals);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("Edit/{id}")]
        public IActionResult Edit(int id)
        {
            M.Animal animal = _animalRepository.GetAnimalById(id);
            if (animal == null)
            {
                Debug.WriteLine("Animal not found!");
                return RedirectToAction("Index");
            }
            return View(animal);
        }

        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        [HttpPost("Edit")]
        public IActionResult Edit(M.Animal animal)
        {
            if (ModelState.IsValid)
            {
                bool animalUpdated = _animalRepository.UpdateAnimal(animal);
                if (animalUpdated)
                {
                    Debug.WriteLine("Animal Updated!");
                    return RedirectToAction("Index");
                }
                else
                {
                    Debug.WriteLine("Failed to update animal!");
                    return View(animal);
                }
            }
            return View(animal);
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("Insert")]
        public IActionResult Insert()
        {
            return View();
        }

        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        [HttpPost("Insert")]
        public IActionResult Insert(ViewAnimalInsert animal)
        {
            //Request
            if (ModelState.IsValid)
            {
                M.Animal newAnimal = new M.Animal() { 
                    Name = animal.Name, Genus = animal.Genus,
                    SubSpecie = animal.SubSpecie, Specie = animal.Specie
                };
                bool animalInserted = _animalRepository.InsertAnimal(newAnimal);
                if (animalInserted)
                {
                    Debug.WriteLine("Animal inserted!");
                }
                else
                {
                    Debug.WriteLine("Fail to insert animal!");
                }
            }
            return View();
        }

        [Authorize(Roles = "Manager")]
        [ValidateAntiForgeryToken]
        [HttpPost("Remove")]
        public IActionResult Remove(int id)
        {
            _animalRepository.RemoveAnimal(id);
            return RedirectToAction("Index");
        }
    }
}