namespace allspice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{

  private readonly RecipesService _recipesService;
  private readonly Auth0Provider _auth;


  public RecipesController(RecipesService recipesService, Auth0Provider auth)
  {
    _recipesService = recipesService;
    _auth = auth;
  }


  [HttpGet]
  public ActionResult<List<Recipe>> GetAll()
  {
    try
    {
      List<Recipe> recipes = _recipesService.GetAll();
      return Ok(recipes);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpGet("{recipeId}")]
  public ActionResult<Recipe> GetOne(int recipeId)
  {
    try
    {
      Recipe recipe = _recipesService.GetOne(recipeId);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Recipe>> Post([FromBody] Recipe recipeData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      recipeData.CreatorId = userInfo.Id;
      Recipe recipe = _recipesService.Post(recipeData);
      return Ok(recipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpPut("{recipeId}")]
  public async Task<ActionResult<Recipe>> EditRecipe([FromBody] Recipe editData, int recipeId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Recipe editedRecipe = _recipesService.EditRecipe(editData, userInfo, recipeId);
      return Ok(editedRecipe);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [Authorize]
  [HttpDelete("{recipeId}")]
  public async Task<ActionResult<string>> DeleteRecipe(int recipeId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _recipesService.DeleteRecipe(recipeId);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
