namespace allspice.Controllers;

[ApiController]
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
}
