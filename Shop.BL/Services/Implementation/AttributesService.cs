using AutoMapper;
using Shop.BL.Dtos.Attribute;
using Shop.BL.Services.Interfaces;
using Shop.DAL.Data.Interfaces;

namespace Shop.BL.Services.Implementation
{
    public class AttributesService : IAttributesService
    {
        private readonly IAttributesRepo _attributesRepo;
        private readonly IMapper _mapper;

        public AttributesService(IAttributesRepo attributesRepo, IMapper mapper)
        {
            _attributesRepo = attributesRepo;
            _mapper = mapper;
        }

        public async Task<AttributeReadDto> UpdateAttribute(int id, AttributeUpdateDto attributeUpdateDto)
        {
            var attributeFromRepo = await _attributesRepo.GetAttributeById(id);
            if (attributeFromRepo is null)
            {
                throw new KeyNotFoundException($"Attribute not found with id:{id}");
            }
            var productAttributeToRepo = _mapper.Map(attributeUpdateDto, attributeFromRepo);
            await _attributesRepo.SaveChanges();
            return _mapper.Map<AttributeReadDto>(productAttributeToRepo);
        }

        public async Task<AttributeReadDto> DeleteAttribute(int id)
        {
            var attributeFromRepo = await _attributesRepo.GetAttributeById(id);
            if (attributeFromRepo is null)
            {
                throw new KeyNotFoundException($"Attribute not found with id:{id}");
            }
            _attributesRepo.DeleteAttribute(attributeFromRepo);
            await _attributesRepo.SaveChanges();
            return _mapper.Map<AttributeReadDto>(attributeFromRepo);
        }

        public async Task<IEnumerable<AttributeReadDto>> GetAllAttributes()
        {
            return _mapper.Map<IEnumerable<AttributeReadDto>>(await _attributesRepo.GetAllAttributes());
        }
    }
}
