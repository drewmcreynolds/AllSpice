using AllSpice.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using System;

namespace AllSpice.Repositories
{
  public class RecipesRepository
  {
    private readonly IDbConnection _db;
    public RecipesRepository(IDbConnection db)
    {
      _db = db;
    }

    internal List<Recipe> GetAll()
    {
      string sql = "SELECT * FROM recipes;";
      return _db.Query<Recipe>(sql).ToList();
    }
    internal Recipe GetById(int recipeId)
    {
      string sql = @"
      SELECT
      r.*,
      a.*
      FROM recipes r
      JOIN accounts a on a.id = r.creatorId
      WHERE r.id = @recipeId;
      ";
      return _db.Query<Recipe, Account, Recipe>(sql, (r, a) =>
      {
        r.Creator = a;
        return r;
      }, new {recipeId}).FirstOrDefault();
    }

    internal Recipe Create(Recipe recipeData)
    {
      string sql = @"
      INSERT INTO recipes(creatorId, name, instructions)
      Values(@CreatorId, @Name, @Instructions);
      SELECT LAST_INSERT_ID();
      ";
      var id = _db.ExecuteScalar<int>(sql, recipeData);
      recipeData.Id = id;
      return recipeData;
    }

    internal Recipe Remove(int recipeId)
    {
      Recipe foundRecipe = GetById(recipeId);
      string sql = "DELETE FROM recipes WHERE id = @recipeId LIMIT 1;";
      var affectedRows = _db.Execute(sql, new {recipeId});
      if (affectedRows == 0)
      {
        throw new Exception("No Dice...");
      }
      return foundRecipe;
    }
  }

}