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
    public class IndustryRepository : IDataRepository<Industry>
    {
        readonly StocksAssistanceDbContext context;

        public IndustryRepository(StocksAssistanceDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Industry> GetAll()
        {
            return context.Industries.AsQueryable();
        }

        public IQueryable<Industry> GetAll(int sectorId)
        {
            return context.Industries.Where(i => i.SectorId == sectorId);
        }

        public async Task<Industry> Get(int id)
        {
            return await context.Industries.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Industry> Get(int sectorId, string name)
        {
            return await context.Industries.FirstOrDefaultAsync(i => i.SectorId == sectorId && i.Name == name);
        }

        public async Task Add(Industry entity, bool save = true)
        {
            context.Industries.Add(entity);
            if (save) await SaveChanges();
        }

        public async Task Update(Industry entity, bool save = true)
        {
            context.Industries.Update(entity);
            if (save) await SaveChanges();
        }

        public async Task Delete(Industry entity, bool save = true)
        {
            context.Industries.Remove(entity);
            if (save) await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
