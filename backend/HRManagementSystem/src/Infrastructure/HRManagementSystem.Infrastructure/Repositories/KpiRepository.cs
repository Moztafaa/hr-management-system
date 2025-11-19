using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRManagementSystem.Infrastructure.Repositories;
public class KpiRepository(ApplicationDbContext _ctx) : Repository<KPI>(_ctx), IKpiRepository
{
}

