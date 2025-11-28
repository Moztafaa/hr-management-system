using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Enums.Performance;

namespace HRManagementSystem.Application.DTOs.Performance;
public class CreateCycleDto
{
    public string Name { get; set; } = string.Empty;
    public CycleFrequency Frequency { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public RatingScaleType RatingScale { get; set; }
}

