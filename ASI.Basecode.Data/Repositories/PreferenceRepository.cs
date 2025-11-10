using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Data.Repositories
{
    public class PreferenceRepository : BaseRepository, IPreferenceRepository
    {
        public PreferenceRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public async Task<Preference?> GetByUserIdAsync(int userId)
        {
            return await GetDbSet<Preference>()
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.UserId == userId);
        }

        public async Task AddOrUpdateAsync(Preference preference)
        {
            var dbSet = GetDbSet<Preference>();
            var existing = await dbSet.FirstOrDefaultAsync(p => p.UserId == preference.UserId);

            if (existing == null)
            {
                await dbSet.AddAsync(preference);
            }
            else
            {
                existing.ShowStats = preference.ShowStats;
                existing.ShowSatisfaction = preference.ShowSatisfaction;
                existing.CardOrder = preference.CardOrder;

                dbSet.Update(existing);
            }

            await UnitOfWork.SaveChangesAsync();
        }
    }
}
