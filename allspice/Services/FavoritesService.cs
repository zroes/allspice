namespace allspice.Services;

public class FavoritesService
{
  private readonly FavoritesRepo _repo;

  public FavoritesService(FavoritesRepo repo)
  {
    _repo = repo;
  }

  internal Favorite GetOne(int favoriteId)
  {
    Favorite favorite = _repo.GetOne(favoriteId);
    if (favorite == null)
      throw new Exception("Nothing exists at id: " + favoriteId);
    return favorite;
  }

  internal Favorite AddFavorite(Favorite favData, Account userInfo)
  {
    favData.AccountId = userInfo.Id;
    int favoriteId = _repo.AddFavorite(favData);
    Favorite addedFavorite = GetOne(favoriteId);
    return addedFavorite;
  }

  internal List<Favorite> GetFavorites(Account userInfo)
  {
    List<Favorite> favorites = _repo.GetFavorites(userInfo.Id);
    return favorites;
  }

  internal string RemoveFavorite(int favId, Account userInfo)
  {
    Favorite fav = GetOne(favId);
    if (fav.AccountId != userInfo.Id)
      throw new Exception("You can't remove someone else's favorites!");
    _repo.RemoveFavorite(favId);
    return $"Favorite at {favId} was deleted successfully";
  }
}
