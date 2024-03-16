using Attribute = Shop.DAL.Models.Attribute;

namespace Shop.DAL.Data.Interfaces
{
    public interface IAttributesRepo
    {
        Task SaveChanges();
        Task<IEnumerable<Attribute>> GetAllAttributes();
        Task<Attribute> GetAttributeById(int id);
        //Task<Models.Attribute> GetAttributeByName(string name);
        Task AddAttribute(Attribute attribute);
        void DeleteAttribute(Attribute attribute);
    }
}
