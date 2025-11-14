using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRManagementSystem.Domain.Interfaces.LeaveRepository;

namespace HRManagementSystem.Infrastructure.Repositories.ILeaveRepository;
public class LeaveTypeRepository(ApplicationDbContext _context) : Repository<LeaveType>(_context), ILeaveTypeRepository
{
}
