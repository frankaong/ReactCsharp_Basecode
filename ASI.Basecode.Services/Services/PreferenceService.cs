using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Services
{
    public class PreferenceService : IPreferenceService
    {
        private readonly IPreferenceRepository _preferenceRepository;

        public PreferenceService(IPreferenceRepository preferenceRepository)
        {
            _preferenceRepository = preferenceRepository;
        }
        public async Task<Preference?> GetByUserIdAsync(int userId)
        {
            return await _preferenceRepository.GetByUserIdAsync(userId);
        }

        public async Task AddOrUpdateAsync(Preference preference)
        {
            await _preferenceRepository.AddOrUpdateAsync(preference);
        }
    }
}
