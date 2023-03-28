using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using WPFApplicationArc.DBContexts;
using WPFApplicationArc.Models;

namespace WPFApplicationArc.Services
{
    /// <summary>
    /// Tüm Servis Class'larının yapacağı ortak temel işlemler
    /// için tanımlanan jenerik sınıf
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    
    // Base Model'e dikkat. Models içerisinde tanımlı bir sınıf. jenerik bir sınıf değil.
    // Böyle tanımlanmasa bu sefer BaseModel içerisinde bize aşağıda gerekli olan Id'e erişemiyoruz.
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : BaseModel
    {
        // Veritabanı Bağlantısı
        private readonly AppDBContext _context;
        
        // Kullanılacak Model sınıfının tanımı
        private readonly DbSet<TEntity> _dbSet;
        
        // Kurucu sınıf tanımı
        public GenericService(AppDBContext context)
        {
            _context = context;
            
            if (_context == null)
                throw new ArgumentNullException(context.ContextId.ToString());
            
            _dbSet = context.Set<TEntity>();
        }
        
        // Yeni kayır ekleme işlemi
        public async Task Add(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        
        // Silme işlemi
        public async Task Remove(int Id)
        {
            var entity = await GetById(Id);
            if (entity != null)
            {

                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
        
        // Bunu neden yaptım bilmiyorum. :)
        public async Task<TEntity> Get(TEntity entity)
        {
            return await GetById(entity.Id);
        }
        
        // Tümünü Gösterir.
        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }
        
        // ID'ye göre bulur.
        public async Task<TEntity> GetById(int Id)
        {
            var p =  await _dbSet.FirstOrDefaultAsync(p => p.Id == Id);
            return p ?? throw new ArgumentNullException(nameof(Id));
        }
        
        // BaseModel içerisinde yaratıma zamanı ve
        // Güncelleme zamanı var. Güncelleme işleminde
        // otomatik buradan yapıyoruz.
        public async Task Update(TEntity entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
