using System.Collections.Generic;

namespace SportsStore.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }

        void AddCategory(Category category);

        void UpdateCategory(Category category);

        void DeleteCategory(Category category);
    }
}
