using System.Linq;
using System.Threading.Tasks;

namespace WPFApplicationArc.Services;

// Tüm DB servisleri için ihtiyaç olacak temel işlemler için
// gerekli metodlar. Bunu yapmamızın nedeni her DB servisi için
// bu işlemleri sürekli tekrarlamamak.
public interface IGenericService<TEntity> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<TEntity> Get(TEntity entity);
    Task<TEntity> GetById(int id);
    Task Add(TEntity entity);
    Task Update(TEntity entity);
    Task Remove(int id);
}