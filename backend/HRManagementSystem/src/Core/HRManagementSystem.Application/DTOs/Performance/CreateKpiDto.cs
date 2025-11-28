using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Performance;
public class CreateKpiDto
{
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Unit { get; set; }
    public decimal Target { get; set; }
    public string? Description { get; set; }
}

