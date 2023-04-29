namespace allspice.Repositories;

public class IngredientsRepo
{
  private readonly IDbConnection _db;

  public IngredientsRepo(IDbConnection db)
  {
    _db = db;
  }

  internal int AddIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients
    (name, quantity, recipeId)
    VALUES
    (@Name, @Quantity, @RecipeId);
    SELECT LAST_INSERT_ID()
    ;";
    int ingredientId = _db.ExecuteScalar<int>(sql, ingredientData);
    return ingredientId;
  }

  internal List<Ingredient> GetAll(int recipeId)
  {
    string sql = "SELECT * FROM ingredients WHERE recipeId = @recipeId;";
    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }

  internal Ingredient GetOne(int ingredientId)
  {
    string sql = "SELECT * FROM ingredients WHERE id = @ingredientId;";
    Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
    return ingredient;
  }

  internal void RemoveIngredient(int ingredientId)
  {
    string sql = "DELETE FROM ingredients WHERE id = @ingredientId";
    _db.Execute(sql, new { ingredientId });
  }
}
