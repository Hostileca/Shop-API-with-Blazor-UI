using Shop.BL.Dtos.Attribute;

namespace Shop.BL.Services.Interfaces
{
    public interface IAttributesService
    {
        Task<AttributeReadDto> DeleteAttribute(int id);
        Task<AttributeReadDto> UpdateAttribute(int id, AttributeUpdateDto attributeUpdateDto);
        Task<IEnumerable<AttributeReadDto>> GetAllAttributes();
    }
}
