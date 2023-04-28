namespace allspice.Services;

public class IngredientsService
{
  private readonly IngredientsRepo _repo;
  private readonly RecipesService _recipesService;

  public IngredientsService(IngredientsRepo repo, RecipesService recipesService)
  {
    _repo = repo;
    _recipesService = recipesService;
  }

  internal Ingredient GetOne(int ingredientId)
  {
    Ingredient ingredient = _repo.GetOne(ingredientId);
    if (ingredient == null)
      throw new Exception("That ingredient doesn't exist ----> " + ingredientId);

    return ingredient;
  }

  internal Ingredient AddIngredient(Ingredient ingredientData, Account userInfo)
  {
    Recipe recipe = _recipesService.GetOne(ingredientData.RecipeId);
    if (recipe.CreatorId != userInfo.Id)
      throw new Exception("You can't add ingredients to someone else's recipe!");
    int ingredientId = _repo.AddIngredient(ingredientData);
    Ingredient newIngredient = GetOne(ingredientId);
    return newIngredient;
  }
}
