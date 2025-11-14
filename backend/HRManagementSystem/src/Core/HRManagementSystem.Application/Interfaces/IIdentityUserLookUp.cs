using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Application.Interfaces;
public interface IIdentityUserLookup
{
    Task<List<int>> GetHrEmployeeIdsAsync(CancellationToken ct = default);
}
