using AutoMapper;
using Shop.BL.Dtos.Manufacturer;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;
using Shop.DAL.Models;
using System.Data;

namespace Shop.BL.Services.Implementation
{
    public class ManufacturersService : IManufacturersService
    {
        private readonly IManufacturersRepo _manufacturersRepo;
        private readonly IMapper _mapper;

        public ManufacturersService(IManufacturersRepo manufacturersRepo, IMapper mapper)
        {
            _manufacturersRepo = manufacturersRepo;
            _mapper = mapper;
        }

        public async Task<ManufacturerDetailedReadDto> CreateManufacturer(ManufacturerCreateDto manufacturerCreateDto)
        {
            if (await _manufacturersRepo.GetManufacturerByName(manufacturerCreateDto.Name) is not null)
            {
                throw new DuplicateNameException($"Manufacturer {manufacturerCreateDto.Name} already exists");
            }
            var manufacturer = _mapper.Map<Manufacturer>(manufacturerCreateDto);
            await _manufacturersRepo.AddManufacturer(manufacturer);
            await _manufacturersRepo.SaveChanges();
            return _mapper.Map<ManufacturerDetailedReadDto>(manufacturer);
        }

        public async Task<ManufacturerDetailedReadDto> DeleteManufacturer(int id)
        {
            var manufacturerModelFromRepo = await _manufacturersRepo.GetManufacturerById(id);
            if (manufacturerModelFromRepo is null)
            {
                throw new KeyNotFoundException($"Manufacturer not found with id:{id}");
            }
            _manufacturersRepo.DeleteManufacturer(manufacturerModelFromRepo);
            await _manufacturersRepo.SaveChanges();
            return _mapper.Map<ManufacturerDetailedReadDto>(manufacturerModelFromRepo);
        }

        public async Task<IEnumerable<ManufacturerReadDto>> GetAllManufacturers()
        {
            var manufacturers = await _manufacturersRepo.GetAllManufacturers();
            return _mapper.Map<IEnumerable<ManufacturerReadDto>>(manufacturers);
        }

        public async Task<ManufacturerDetailedReadDto> GetManufacturerById(int id)
        {
            var manufacturer = await _manufacturersRepo.GetManufacturerById(id);
            if (manufacturer is null)
            {
                throw new KeyNotFoundException($"Manufacturer not found with id:{id}");
            }
            return _mapper.Map<ManufacturerDetailedReadDto>(manufacturer);
        }

        public async Task<ManufacturerDetailedReadDto> UpdateManufacturer(int id, ManufacturerUpdateDto manufacturerUpdateDto)
        {
            var manufacturerFromRepo = await _manufacturersRepo.GetManufacturerById(id);
            if (manufacturerFromRepo is null)
            {
                throw new KeyNotFoundException($"Manufacturer not found with id:{id}");
            }
            var categoryModelToRepo = _mapper.Map(manufacturerUpdateDto, manufacturerFromRepo);
            await _manufacturersRepo.SaveChanges();
            return _mapper.Map<ManufacturerDetailedReadDto>(categoryModelToRepo);
        }
    }
}
