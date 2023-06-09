namespace allspice.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _ingredientsService;
  private readonly Auth0Provider _auth;

  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth)
  {
    _ingredientsService = ingredientsService;
    _auth = auth;
  }



  [HttpPost]
  public async Task<ActionResult<Ingredient>> AddIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      Ingredient ingredient = _ingredientsService.AddIngredient(ingredientData, userInfo);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{ingredientId}")]
  public async Task<ActionResult<string>> RemoveIngredient(int ingredientId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _ingredientsService.RemoveIngredient(ingredientId, userInfo);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
