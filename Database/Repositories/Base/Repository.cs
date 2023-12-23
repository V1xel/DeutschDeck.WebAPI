using AutoMapper;
using GLA.Database.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DeutschDeck.WebAPI.Database.Repositories.Base
{
    public abstract class Repository<TDomain, TEntity> where TEntity : class
    {
        protected DDContext _context;

        protected IMapper _mapper;

        public Repository(DDContext context)
        {
            _context = context;
            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TDomain, TEntity>();
                cfg.CreateMap<TEntity, TDomain>();
            }).CreateMapper();
        }

        public async Task Save(TDomain obj)
        {
            var entity = _mapper.Map<TEntity>(obj);

            try
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw RepositoryException.Map(ex);
            }
        }
    }
}
