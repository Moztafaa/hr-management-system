using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.DTOs.Performance;
public class CreateFeedbackDto
{
    public int PerformanceReviewId { get; set; }
    public int FromEmployeeId { get; set; }
    public string Type { get; set; } = "Self"; // Self/Manager/Peer
    public string Comments { get; set; } = string.Empty;
}

