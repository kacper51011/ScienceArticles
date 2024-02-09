using MediatR;
using ScienceArticles.Application.Dtos.GetCategories;
using ScienceArticles.Domain.Interfaces;

namespace ScienceArticles.Application.Queries.GetCategories
{
    public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryResponeItem>>
    {
        private readonly ICategoryRepository _categoryRepository;

        public GetCategoriesQueryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<CategoryResponeItem>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _categoryRepository.GetCategories();

                List<CategoryResponeItem> items = new();

                if (response != null)
                {
                    foreach (var category in response)
                    {
                        var item = new CategoryResponeItem();
                        item.Name = category.Name;
                        item.Id = category.CategoryId.Value.ToString();

                        items.Add(item);
                    }
                }
                return items;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
