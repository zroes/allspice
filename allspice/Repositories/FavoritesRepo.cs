namespace allspice.Repositories;

public class FavoritesRepo
{
  private readonly IDbConnection _db;

  public FavoritesRepo(IDbConnection db)
  {
    _db = db;
  }

  internal int AddFavorite(Favorite favData)
  {
    string sql = @"
    INSERT INTO favorites
    (recipeId, accountId)
    VALUES
    (@RecipeId, @AccountId);
    SELECT LAST_INSERT_ID()
      ;";
    int favoriteId = _db.ExecuteScalar<int>(sql, favData);
    return favoriteId;
  }

  internal List<Favorite> GetFavorites(string accountId)
  {
    string sql = "SELECT * FROM favorites WHERE accountId = @accountId;";
    List<Favorite> favorites = _db.Query<Favorite>(sql, new { accountId }).ToList();
    return favorites;
  }


  internal Favorite GetOne(int favoriteId)
  {
    string sql = "SELECT * FROM favorites WHERE id = @favoriteId;";
    Favorite favorite = _db.Query<Favorite>(sql, new { favoriteId }).FirstOrDefault();
    return favorite;
  }

  internal void RemoveFavorite(int favId)
  {
    string sql = "DELETE FROM favorites WHERE id = @favId";
    _db.Execute(sql, new { favId });
  }
}
