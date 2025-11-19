namespace HRManagementSystem.Domain.Entities;
public class Attendance
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public DateTime Date { get; set; }
    public DateTime? CheckInTime { get; set; }
    public DateTime? CheckOutTime { get; set; }
    public bool IsLate { get; set; }
    public bool IsAbsent { get; set; }
}
