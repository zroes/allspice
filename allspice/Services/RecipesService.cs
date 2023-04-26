namespace allspice.Services;

public class RecipesService
{

  private readonly RecipesRepository _repo;

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }
}
