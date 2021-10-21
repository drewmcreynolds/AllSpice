using System;
using System.Collections.Generic;
using AllSpice.Models;
using AllSpice.Repositories;

namespace AllSpice.Services
{
  public class RecipesService
  {
    private readonly RecipesRepository _rr;

    public RecipesService(RecipesRepository rr)
    {
      _rr = rr;
    }

    public List<Recipe> GetAll()
    {
      return _rr.GetAll();
    }

    public Recipe GetById(int recipeId)
    {
      Recipe foundRecipe = _rr.GetById(recipeId);
      if(foundRecipe == null)
      {
        throw new Exception("Unable to find recipe");
      }
      return foundRecipe;
    }

    internal Recipe Create(Recipe recipeData)
    {
      return _rr.Create(recipeData);
    }

    internal Recipe Remove(int recipeId)
    {
      return _rr.Remove(recipeId);
    }

    internal object GetSpicesByRecipeId(int recipeId)
    {
      throw new NotImplementedException();
    }
  }
}