namespace allspice.Services;

public class RecipesService
{

  private readonly RecipesRepository _repo;

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal string DeleteRecipe(int recipeId)
  {
    // Recipe recipeToDelete = GetOne(recipeId);
    _repo.DeleteRecipe(recipeId);
    return $"recipe {recipeId} was deleted.";
  }

  internal Recipe EditRecipe(Recipe editData, Account userInfo, int recipeId)
  {
    Recipe recipeToEdit = GetOne(recipeId);
    if (recipeToEdit.CreatorId != userInfo.Id)
      throw new Exception("You can't change what you don't own.");
    // if (editData.Id != recipeId)
    //   throw new Exception("I don't know what you're trying to do, but stop.");
    recipeToEdit.Title = editData.Title ?? recipeToEdit.Title;
    recipeToEdit.Img = editData.Img ?? recipeToEdit.Img;
    recipeToEdit.Category = editData.Category ?? recipeToEdit.Category;
    recipeToEdit.Instructions = editData.Instructions ?? recipeToEdit.Instructions;


    _repo.EditRecipe(recipeToEdit);

    return recipeToEdit;

  }

  internal List<Recipe> GetAll()
  {
    List<Recipe> recipes = _repo.GetAll();
    return recipes;
  }

  internal Recipe GetOne(int recipeId)
  {
    Recipe recipe = _repo.GetOne(recipeId);
    if (recipe == null)
    {
      throw new Exception("Invalid id bud! --->" + recipeId);
    }
    return recipe;
  }

  internal Recipe Post(Recipe recipeData)
  {
    int recipeId = _repo.Post(recipeData);
    Recipe createdRecipe = GetOne(recipeId);
    return createdRecipe;
  }
}
