using System.Text.RegularExpressions;
using System.Threading;
using HRManagementSystem.Application.Common;

namespace HRManagementSystem.Application.Services;
public class AttendanceService(IUnitOfWork _unitOfWork, IMapper _mapper, IFaceRecognitionService _faceService) : IAttendanceService
{
    public async Task<Response<int>> CheckInAsync(int employeeId, string base64Image)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId);
        if (employee == null)
        {
            return new Response<int>(-1, "Employee not found.", true);
        }

        // Extract new embedding
        float[] newEmbedding = _faceService.ExtractEmbedding(base64Image);

        // Compare embeddings
        bool isMatch = _faceService.CompareEmbeddings(employee.FaceEmbedding, newEmbedding);
        if (!isMatch)
            return new Response<int>(-1, "Face does not match employee.", true);

        // Check existing record
        var existing = await _unitOfWork.Attendances.GetTodayAttendanceAsync(employeeId, DateTime.UtcNow);
        if (existing != null)
            return new Response<int>(-1, "Already checked in today.", true);

        var attendance = new Attendance
        {
            EmployeeId = employeeId,
            Date = DateTime.UtcNow,
            CheckInTime = DateTime.UtcNow,
            IsLate = DateTime.UtcNow.TimeOfDay > new TimeSpan(9, 0, 0)
        };

        await _unitOfWork.Attendances.AddAsync(attendance);
        await _unitOfWork.SaveChangesAsync();

        return new Response<int>(attendance.Id, "Check-in successful.", false);
    }


    public async Task<Response<int>> CheckOutAsync(int employeeId, string base64Image)
    {
        var employee = await _unitOfWork.Employees.GetByIdAsync(employeeId);
        if (employee == null)
        {
            return new Response<int>(-1, "Employee not found.", true);
        }
        float[] newEmbedding = _faceService.ExtractEmbedding(base64Image);

        if (!_faceService.CompareEmbeddings(employee.FaceEmbedding, newEmbedding))
            return new Response<int>(-1, "Face does not match employee.", true);

        var attendance = await _unitOfWork.Attendances.GetTodayAttendanceAsync(employeeId, DateTime.UtcNow);
        if (attendance == null)
            return new Response<int>(-1, "No attendance record found for today.", true);

        attendance.CheckOutTime = DateTime.UtcNow;

        await _unitOfWork.Attendances.UpdateAsync(attendance);
        await _unitOfWork.SaveChangesAsync();

        return new Response<int>(attendance.Id, "Check-out successful.", false);
    }
    public async Task<Response<IEnumerable<AttendanceEmployeeDto>>> GetEmployeeAttendanceAsync(int employeeId)
    {
        var emp= await _unitOfWork.Attendances.GetByEmployeeAsync(employeeId);
        if (emp == null)
        {
            return new Response<IEnumerable<AttendanceEmployeeDto>>(Enumerable.Empty<AttendanceEmployeeDto>(), "No attendance records found for the employee.", true);
        }

        return new Response<IEnumerable<AttendanceEmployeeDto>>(_mapper.Map<IEnumerable<AttendanceEmployeeDto>>(emp), string.Empty, false);
    }
}
