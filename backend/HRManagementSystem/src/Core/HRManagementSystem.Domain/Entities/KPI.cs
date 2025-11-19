using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Domain.Entities;
public class KPI : BaseEntity
{
    public string Code { get; set; } = default!; // ex: "SALES_Q1"
    public string Name { get; set; } = default!;
    public string? Unit { get; set; } // ex: %, Count, EGP
    public decimal Target { get; set; }
    public string? Description { get; set; }
}
