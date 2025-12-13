namespace GestionBiblioteca.Domain.Ports.Out
{
    // Nombre del archivo: IRepository.cs
    public interface IRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        
        // Ajuste: Devolver la entidad creada (si Entity Framework la actualiza con el Id)
        Task<T> AddAsync(T entity); 
        
        // Ajuste: Devolver la entidad actualizada (o Task si el UoW se encarga de SaveChanges)
        Task<T> UpdateAsync(T entity); 
        
        // Ajuste: Devolver bool para indicar si la eliminación fue exitosa
        Task<bool> DeleteAsync(int id);
        
        Task<bool> ExistsAsync(int id);
    }
}