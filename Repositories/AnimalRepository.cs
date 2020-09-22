using M = AnimalCatalogSqLite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalCatalogSqLite.Context;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace AnimalCatalogSqLite.Repositories
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly CatalogContext _dbContext;
        public AnimalRepository(CatalogContext dbContext)
        {
            _dbContext = dbContext;
        }

        public M.Animal GetAnimalById(int id)
        {
            M.Animal animal = new M.Animal();
            try
            {
                animal = _dbContext.Animals.SingleOrDefault(a => a.Id == id);
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
            return animal;
        }

        public List<M.Animal> GetAnimals()
        {
            List<M.Animal> animals = new List<M.Animal>();
            try
            {
                animals = _dbContext.Animals.ToList();
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
            return animals;
        }

        public bool InsertAnimal(M.Animal animal)
        {
            bool animalInserted = false;
            try
            {
                _dbContext.Animals.Add(animal);
                _dbContext.SaveChanges();
                animalInserted = true;
            }
            catch (Exception err)
            {
                Debug.WriteLine($"Error: {err.Message}");
            }
            return animalInserted;
        }

        public bool RemoveAnimal(int id)
        {
            bool animalRemoved = false;
            try
            {
                M.Animal animal = _dbContext.Animals.SingleOrDefault(a => a.Id == id);
                _dbContext.Animals.Remove(animal);
                _dbContext.SaveChanges();
                animalRemoved = true;
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
            return animalRemoved;
        }

        public bool UpdateAnimal(M.Animal animal)
        {
            bool animalUpdated = false;
            try
            {
                M.Animal matchedAnimal = _dbContext.Animals.SingleOrDefault(a => a.Id == animal.Id);
                if (matchedAnimal != null)
                {
                    // Detach first entity (there cannot be two entities with same id being searched in the DB at the same time)
                    _dbContext.Entry(matchedAnimal).State = EntityState.Detached;
                    _dbContext.Animals.Update(animal);
                    _dbContext.SaveChanges();
                    animalUpdated = true;
                }
                else
                {
                    Debug.WriteLine("Animal not found!");
                }
            }
            catch (Exception err)
            {
                Debug.WriteLine(err.Message);
            }
            return animalUpdated;
        }
    }
}
