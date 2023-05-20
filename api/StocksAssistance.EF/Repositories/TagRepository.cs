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
    public class TagRepository : IDataRepository<Tag>
    {
        readonly StocksAssistanceDbContext context;

        public TagRepository(StocksAssistanceDbContext context)
        {
            this.context = context;
        }

        public IQueryable<Tag> GetAll()
        {
            return context.Tags.AsQueryable();
        }

        public async Task<Tag> Get(int id)
        {
            return await context.Tags.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Tag> Get(string name, TagType type)
        {
            return await context.Tags.FirstOrDefaultAsync(c => c.Name == name && c.Type == type);
        }

        public async Task Add(Tag entity, bool save = true)
        {
            context.Tags.Add(entity);
            if (save) await SaveChanges();
        }

        public async Task Update(Tag entity, bool save = true)
        {
            context.Tags.Update(entity);
            if (save) await SaveChanges();
        }

        public async Task Delete(Tag entity, bool save = true)
        {
            context.Tags.Remove(entity);
            if (save) await SaveChanges();
        }

        public async Task SaveChanges()
        {
            await context.SaveChangesAsync();
        }
    }
}
