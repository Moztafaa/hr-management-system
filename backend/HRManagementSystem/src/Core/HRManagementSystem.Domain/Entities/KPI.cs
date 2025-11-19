using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Domain.Entities;
public class KPI : BaseEntity
{
    public string Code { get; set; } = default!; 
    public string Name { get; set; } = default!;
    public string? Unit { get; set; } 
    public decimal Target { get; set; }
    public string? Description { get; set; }
}
