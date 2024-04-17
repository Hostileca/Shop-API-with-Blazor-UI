using Shop.DAL.Models;

namespace Shop.DAL.Data.Interfaces
{
    public interface IManufacturersRepo
    {
        Task SaveChanges();
        Task<IEnumerable<Manufacturer>> GetAllManufacturers();
        Task AddManufacturer(Manufacturer manufacturer);
        void DeleteManufacturer(Manufacturer manufacturer);
        Task<Manufacturer> GetManufacturerById(int id);
        Task<Manufacturer> GetManufacturerByName(string name);
        Task<IEnumerable<Manufacturer>> SearchByName(string searchName);
    }
}
