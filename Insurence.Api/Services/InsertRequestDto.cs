using System.ComponentModel.DataAnnotations;
using Insurence.Api.Entities;

namespace Insurence.Api.Services;

public class InsertRequestDto
{
    [Required] public string? Name { get; set; } 
    [Required] public string? Email { get; set; }
    [Required] public List<RequestDto> RequestDtos { get; set; }
}

public class RequestDto
{
    [Required]public CoverageType Type { get; set; }
    [Required]public double Investment { get; set; }
}
