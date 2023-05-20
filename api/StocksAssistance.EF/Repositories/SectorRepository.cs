using Microsoft.EntityFrameworkCore;
using StocksAssistance.Common.Enums;
using StocksAssistance.EF.Context;
using StocksAssistance.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StocksAssistance.EF.Repositories
{
    public class SectorRepository : IDataRepository<Sector>
    {
        readonly StocksAssistanceDbContext context;

        public SectorRepository(StocksAssistanceDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Sector> GetAll()
        {
            return context.Sectors.AsQueryable();
        }

        public async Task<Sector> Get(int id)
        {
            return await context.Sectors.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Sector> Get(string name)
        {
            return await context.Sectors.FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task Add(Sector entity, bool save = true)
        {
            context.Sectors.Add(entity);
            if (save) await SaveChanges();
        }

        public async Task Update(Sector entity, bool save = true)
        {
            context.Sectors.Update(entity);
            if (save) await SaveChanges();
        }

        public async Task Delete(Sector entity, bool save = true)
        {
            context.Sectors.Remove(entity);
            if (save) await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
