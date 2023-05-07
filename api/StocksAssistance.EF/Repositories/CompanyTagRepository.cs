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
    public class CompanyTagRepository : IDataRepository<CompanyTag>
    {
        readonly StocksAssistanceDbContext context;

        public CompanyTagRepository(StocksAssistanceDbContext context)
        {
            this.context = context;
        }

        public IQueryable<CompanyTag> GetAll()
        {
            return context.CompanyTags.AsQueryable();
        }

        public async Task<CompanyTag> Get(int id)
        {
            return await context.CompanyTags.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<CompanyTag> Get(string name, CompanyTagType type)
        {
            return await context.CompanyTags.FirstOrDefaultAsync(c => c.Name == name && c.Type == type);
        }

        public async Task Add(CompanyTag entity, bool save = true)
        {
            context.CompanyTags.Add(entity);
            if (save) await SaveChanges();
        }

        public async Task Update(CompanyTag entity, bool save = true)
        {
            context.CompanyTags.Update(entity);
            if (save) await SaveChanges();
        }

        public async Task Delete(CompanyTag entity, bool save = true)
        {
            context.CompanyTags.Remove(entity);
            if (save) await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
