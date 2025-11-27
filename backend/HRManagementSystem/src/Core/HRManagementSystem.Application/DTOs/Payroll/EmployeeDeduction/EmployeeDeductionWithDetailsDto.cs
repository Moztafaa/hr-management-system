namespace HRManagementSystem.Application.DTOs.Payroll.EmployeeDeduction
{
    public class EmployeeDeductionWithDetailsDto
    {
        public int EmployeeId { get; set; }
        public int DeductionId { get; set; }
        public string DeductionName { get; set; }
        public decimal DeductionDefaultAmount { get; set; }
        public bool DeductionDefaultIsPercentage { get; set; }
        public decimal? OverrideAmount { get; set; }
        public bool? OverrideIsPercentage { get; set; }

 
        public decimal EffectiveAmount { get; set; }
        public bool EffectiveIsPercentage { get; set; }

        public RecurrenceType Recurrence { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
