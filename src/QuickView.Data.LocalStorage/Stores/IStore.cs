namespace QuickView.Data.LocalStorage.Stores
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using QuickView.Querying.Dto;

    public interface IStore<T, Tid>
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetAsync(Tid id);

        Task CreateAsync(T aggregate);

        Task DeleteAsync(T aggregate);

    }
}
