using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASI.Basecode.Data.Models;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IPreferenceRepository
    {
        Task<Preference?> GetByUserIdAsync(int userId);
        Task AddOrUpdateAsync(Preference preference);
    }
}
