using AutoMapper;
using MediatR;
using ShoppingList.Application.Abstractions.Repositories.Categories;
using ShoppingList.Domain.Entities;

namespace ShoppingList.Application.Features.Categories.Commands.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, Unit>
{
    private readonly ICategoryWriteRepository _categoryWriteRepository;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository, IMapper mapper)
    {
        _categoryWriteRepository = categoryWriteRepository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);

        await _categoryWriteRepository.AddAsync(category);
        await _categoryWriteRepository.SaveAsync();

        return Unit.Value;
    }
}
