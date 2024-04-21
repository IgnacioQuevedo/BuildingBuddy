﻿using Adapter.CustomExceptions;
using Domain;
using IServiceLogic;
using ServiceLogic.CustomExceptions;
using WebModel.Responses.CategoryResponses;

namespace Adapter;

public class CategoryAdapter
{
    #region Constructor and atributtes

    private readonly ICategoryServiceLogic _categoryServiceLogic;

    public CategoryAdapter(ICategoryServiceLogic categoryServiceLogic)
    {
        _categoryServiceLogic = categoryServiceLogic;
    }

    #endregion

    #region Get All Categories

    public IEnumerable<GetCategoryResponse> GetAllCategories()
    {
        try
        {
            IEnumerable<Category> categories = _categoryServiceLogic.GetAllCategories();
            IEnumerable<GetCategoryResponse> categoriesResponses = categories.Select(category => new GetCategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            });

            return categoriesResponses;
        }
        catch (Exception exceptionCaught)
        {
            throw new Exception(exceptionCaught.Message);
        }
    }

    #endregion

    public GetCategoryResponse GetCategoryById(Guid idOfCategoryToFind)
    {
        try
        {
            Category category = _categoryServiceLogic.GetCategoryById(idOfCategoryToFind);
            GetCategoryResponse categoryResponse = new GetCategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };

            return categoryResponse;
        }
        catch (ObjectNotFoundServiceException)
        {
            throw new ObjectNotFoundAdapterException();
        }
    }
}