using ProbKolos1.Models;

namespace ProbKolos1.DTOs;

public class AddAnimal
{
    public String Name { get; set; } = String.Empty;
    public String Type { get; set; } =String.Empty;
    public DateTime AdmissionDate { get; set; }
    public int OwnerId { get; set; }
    public List<ProcedureDTO2> Procedures { get; set; } = null!;
}

public class ProcedureDTO2
{
    public int ProcedureId { get; set; }
    public DateTime date { get; set; }
}
