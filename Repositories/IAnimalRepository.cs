using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using M = AnimalCatalogSqLite.Models;

namespace AnimalCatalogSqLite.Repositories
{
    public interface IAnimalRepository
    {
        List<M.Animal> GetAnimals();
        M.Animal GetAnimalById(int id);
        bool InsertAnimal(M.Animal animal);
        bool RemoveAnimal(int id);
        bool UpdateAnimal(M.Animal animal);
    }
}
