using System.ComponentModel.DataAnnotations;

namespace FinBeatTestTask.Dto;

public class CodeEntity
{
    [Key]
    public int Id { get; set; }
    public int Code { get; set; }
    public string Value { get; set; } = string.Empty;
}
