using System.Linq;
using TheMonarchs.Core.Entities;

namespace TheMonarchs.Core.Interfaces
{
    public interface IMonarchDataProvider
    {
        IQueryable<Monarch> MonarchesDataSource { get; }
    }
}
