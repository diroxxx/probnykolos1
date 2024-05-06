using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using ProbKolos1.DTOs;
using ProbKolos1.Models;

namespace ProbKolos1.Repositories;

public class AnimalRepository: IAnimalRepository
{
    private readonly IConfiguration _configuration;

    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> DoesAnimalExist(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;

        command.CommandText = "Select 1 from Animal where Animal.id = @id";
        command.Parameters.AddWithValue("@id", id);
        
        await connection.OpenAsync();

        var res = await command.ExecuteScalarAsync();

        return res is not null;
    }


    public async Task<AnimaInfoDto> GetAnimal(int id)
    {
        using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Default"));
        using SqlCommand command = new SqlCommand();
        command.Connection = connection;
        
        command.CommandText =
            "Select Animal.ID as animalId, Animal.Name as animalName, Animal.Type, Animal.AdmissionDate, Owner.ID as ownerId, Owner.FirstName, Owner.LastName, [Procedure].Name as procedureName,  [Procedure].Description, Procedure_Animal.Date    from Animal " +
            "join Owner on Animal.Owner_ID = Owner.ID " +
            "join Procedure_Animal on Procedure_animal.Animal_id = Animal.ID " +
            "join [Procedure] on [Procedure].ID = Procedure_Animal.Procedure_ID " +
            " where Animal.id = @id";

        command.Parameters.AddWithValue("@id", id);
        
        await connection.OpenAsync();
        
        AnimaInfoDto animal = null;
        var reader = await command.ExecuteReaderAsync();

        var animalIdOrdinar = reader.GetOrdinal("animalId");
        var animalNameOrdinary = reader.GetOrdinal("animalName");
        var animalTypeOrdinar = reader.GetOrdinal("Type");
        var animalAdmissionDateOrdinal = reader.GetOrdinal("AdmissionDate");
        
        var ownerIdOrdinal = reader.GetOrdinal("ownerId");
        var ownerFirstNameOrdinal = reader.GetOrdinal("FirstName");
        var ownerLastNameOrdinal = reader.GetOrdinal("LastName");
        
        
        var procedureNameOrdinal = reader.GetOrdinal("procedureName");
        var procedureDescriptionOrdinal = reader.GetOrdinal("Description");
        var procedureDateOrdinal = reader.GetOrdinal("Date");


        while (await reader.ReadAsync()) 
        {
            if (animal is null)
            {
                animal = new AnimaInfoDto()
                {
                    Id = reader.GetInt32(animalIdOrdinar),
                    Name = reader.GetString(animalNameOrdinary),
                    Type = reader.GetString(animalTypeOrdinar),
                    AdmissionDate = reader.GetDateTime(animalAdmissionDateOrdinal),
                    Owner = new OwnerDto()
                    {
                        Id = reader.GetInt32(ownerIdOrdinal),
                        FirstName = reader.GetString(ownerFirstNameOrdinal),
                        LastName = reader.GetString(ownerLastNameOrdinal)
                    },
                    Procedures = new List<ProcedureDto>()
                    {
                        new ProcedureDto()
                        {
                            Name = reader.GetString(procedureNameOrdinal),
                            Description = reader.GetString(procedureDescriptionOrdinal),
                            Date = reader.GetDateTime(procedureDateOrdinal)
                        }
                    }
                };
            }
            else
            {
                animal.Procedures.Add(new ProcedureDto()
                {
                    Name = reader.GetString(procedureNameOrdinal),
                    Description = reader.GetString(procedureDescriptionOrdinal),
                    Date = reader.GetDateTime(procedureDateOrdinal)
                });
            }
        }

        return animal;
    }
}