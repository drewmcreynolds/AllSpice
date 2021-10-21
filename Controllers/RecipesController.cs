using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using AllSpice.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _rs;

    public RecipesController(RecipesService rs)
    {
      _rs = rs;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetAll()
    {
      try
      {
           return Ok(_rs.GetAll());
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [HttpGet("{recipeId}")]

    public ActionResult<Recipe> GetById(int recipeId)
    {
      try
      {
           return Ok(_rs.GetById(recipeId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [HttpGet("{recipeId}/spices")]

    public ActionResult<Recipe> GetSpicesByRecipeId(int recipeId)
    {
      try
      {
           return Ok(_rs.GetSpicesByRecipeId(recipeId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [Authorize]
    [HttpPost]

    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe recipeData)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        recipeData.CreatorId = userInfo.Id;
           Recipe createdRecipe = (_rs.Create(recipeData));
           createdRecipe.Creator = userInfo;
           return createdRecipe;
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }

    [HttpDelete("{recipeId}")]

    public ActionResult<Recipe> Remove(int recipeId)
    {
      try
      {
           return Ok(_rs.Remove(recipeId));
      }
      catch (System.Exception e)
      {
          return BadRequest(e.Message);
      }
    }
  }
}