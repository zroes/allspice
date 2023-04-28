namespace allspice.Repositories;

public class RecipesRepository
{

  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal void DeleteRecipe(int recipeId)
  {
    string sql = @"
    DELETE FROM recipes
    WHERE id = @recipeId LIMIT 1;
    ;";
    _db.Execute(sql, new { recipeId });
  }

  internal void EditRecipe(Recipe recipeToEdit)
  {
    string sql = @"
    UPDATE recipes
    SET
    title = @Title,
    instructions = @Instructions,
    img = @Img,
    category = @Category
    WHERE 
    id = @Id
    ;";
    _db.Execute(sql, recipeToEdit);
  }

  internal List<Recipe> GetAll()
  {
    string sql = @"
    SELECT r.*, creator.*
    FROM recipes r
    JOIN accounts creator
    ON creator.id = r.creatorId
    ;";
    List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (recipe, account) =>
    {
      recipe.Creator = account;
      return recipe;
    }).ToList();
    return recipes;
  }

  internal Recipe GetOne(int recipeId)
  {
    string sql = @"
    SELECT r.*, creator.*
    FROM recipes r
    JOIN accounts creator
    ON creator.id = r.creatorId
    WHERE r.id = @recipeId
    LIMIT 1
    ;";
    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (recipe, creator) =>
    {
      recipe.Creator = creator;
      return recipe;
    }, new { recipeId }).FirstOrDefault();
    return recipe;
  }

  internal int Post(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO recipes
    (title, instructions, img, category, creatorId)
    VALUES
    (@Title, @Instructions, @Img, @Category, @CreatorId);
    SELECT LAST_INSERT_ID();";

    int recipeId = _db.ExecuteScalar<int>(sql, recipeData);

    return recipeId;
  }
}
