using ProbKolos1.DTOs;
using ProbKolos1.Models;

namespace ProbKolos1.Repositories;

public interface IAnimalRepository
{
    Task<bool> DoesAnimalExist(int id);
    Task<AnimaInfoDto> GetAnimal(int id);

}