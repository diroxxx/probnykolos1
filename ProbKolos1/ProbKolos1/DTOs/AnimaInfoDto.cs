using ProbKolos1.Models;

namespace ProbKolos1.DTOs;

public class AnimaInfoDto
{
    // public int id { get; set; }
    // public String name { get; set; } = string.Empty;
    // public String type { get; set; } = string.Empty;
    // public DateTime admissionDate { get; set; }
    //
    // public Owner owner { get; set; } = null!;
    // public List<Procedure> procedures { get; set; } = null!;
    
    
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public DateTime AdmissionDate { get; set; }
    public OwnerDto Owner { get; set; } = null!;
    public List<ProcedureDto> Procedures { get; set; } = null!;
}

public class OwnerDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

public class ProcedureDto
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
}
    

