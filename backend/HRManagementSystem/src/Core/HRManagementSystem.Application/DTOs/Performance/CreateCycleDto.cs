using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Performance;
public class CreateCycleDto
{
    public string Name { get; set; } = string.Empty;
    public string Frequency { get; set; } = "Annual"; // Annual | Quarterly | Monthly
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string RatingScale { get; set; } = "OneToFive"; // OneToFive | Percentile
}

