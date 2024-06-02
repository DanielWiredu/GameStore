using GameStore.FrontEnd.Models;

namespace GameStore.FrontEnd.Clients;

public class GamesClient(HttpClient httpClient)
{
    // private readonly List<GameSummary> games = 
    // [
    //     new(){
    //         Id = 1,
    //         Name = "Street Fighter II",
    //         Genre = "Fighting",
    //         Price = 19.99M,
    //         ReleaseDate = new DateOnly(1992, 7, 15)
    //     },
    //     new(){
    //         Id = 2,
    //         Name = "Fina Fantasy",
    //         Genre = "Sports",
    //         Price = 29.99M,
    //         ReleaseDate = new DateOnly(1992, 7, 15)
    //     },
    //     new(){
    //         Id = 3,
    //         Name = "FIFA 23",
    //         Genre = "Racing",
    //         Price = 59.99M,
    //         ReleaseDate = new DateOnly(1992, 7, 15)
    //     },
    // ];

    //private readonly Genre[] genres = new GenresClient(httpClient).GetGenres();

//    public GameSummary[] GetGames() => [.. games];
    public async Task<GameSummary[]> GetGamesAsync() => await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];

     public async Task AddGameAsync(GameDetails game) => await httpClient.PostAsJsonAsync("games", game);

     public async Task<GameDetails> GetGameAsync(int id) 
        => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}") ?? throw new Exception("Could not find game!");

     public async Task UpdateGameAsync(GameDetails UpdatedGame) 
        => await httpClient.PutAsJsonAsync($"games/{UpdatedGame.Id}", UpdatedGame);

    public async Task DeleteGameAsync(int id) => await httpClient.DeleteAsync($"games/{id}");


    // public void AddGame(GameDetails game)
    // {
    //     Genre genre = GetGenreById(game.GenreId);

    //     var gameSumary = new GameSummary
    //     {
    //         Id = games.Count + 1,
    //         Name = game.Name,
    //         Genre = genre.Name,
    //         Price = game.Price,
    //         ReleaseDate = game.ReleaseDate,
    //     };
    //     games.Add(gameSumary);
    // }

    // public GameDetails GetGame(int id)
    // {
    //     GameSummary game = GetGameSummaryById(id);

    //     var genre = genres.Single(genre => string.Equals(genre.Name, game.Genre, StringComparison.OrdinalIgnoreCase));

    //     return new GameDetails
    //     {
    //         Id = game.Id,
    //         Name = game.Name,
    //         GenreId = genre.Id.ToString(),
    //         Price = game.Price,
    //         ReleaseDate = game.ReleaseDate
    //     };
    // }
    
    // public void UpdateGame(GameDetails UpdatedGame)
    // {
    //     var genre = GetGenreById(UpdatedGame.GenreId);
    //     GameSummary existingGame = GetGameSummaryById(UpdatedGame.Id);

    //     existingGame.Name = UpdatedGame.Name;
    //     existingGame.Genre = genre.Name;
    //     existingGame.Price = UpdatedGame.Price;
    //     existingGame.ReleaseDate = UpdatedGame.ReleaseDate;
    // }
    
    // public void DeleteGame(int id)
    // {
    //     var game = GetGameSummaryById(id);
    //     games.Remove(game);
    // }
    
    // private GameSummary GetGameSummaryById(int id)
    // {
    //     // from games, find the game whose Id is equal to the id parameter
    //     GameSummary? game = games.Find(game => game.Id == id);
    //     ArgumentNullException.ThrowIfNull(game);
    //     return game;
    // }

    // private Genre GetGenreById(string? id)
    // {
    //     ArgumentException.ThrowIfNullOrWhiteSpace(id);
    //     return genres.Single(genre => genre.Id == int.Parse(id));
    // }
}
