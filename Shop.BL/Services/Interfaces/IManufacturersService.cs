using Shop.BL.Dtos.Manufacturer;

namespace Shop.BL.Services.Interfaces
{
    public interface IManufacturersService
    {
        Task<IEnumerable<ManufacturerReadDto>> GetAllManufacturers();
        Task<ManufacturerDetailedReadDto> GetManufacturerById(int id);
        Task<ManufacturerDetailedReadDto> CreateManufacturer(ManufacturerCreateDto manufacturerCreateDto);
        Task<ManufacturerDetailedReadDto> DeleteManufacturer(int id);
        Task<ManufacturerDetailedReadDto> UpdateManufacturer(int id, ManufacturerUpdateDto manufacturerUpdateDto);
        Task<IEnumerable<ManufacturerReadDto>> SearchManufacturers(string searchText);
    }
}
