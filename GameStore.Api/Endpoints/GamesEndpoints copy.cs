using GameStore.Api.Data;
using GameStore.Api.Dtos;
using GameStore.Api.Mapping;

namespace GameStore.Api.Endpoints;

public static class GamesEndpointsCopy
{
    const string GetGameEndpointName = "GetGame";

    private static readonly List<GameSummaryDto> games = [
        new (1, "Street Fighter II", "Fighting", 19.99M, new DateOnly(1992, 7, 30)),
        new (2, "Final Fantasy XIV", "Roleplaying", 59.99M, new DateOnly(2010, 9, 30)),
        new (3, "FIFA 23", "Sports", 69.99M, new DateOnly(2022, 10, 28))
    ];

public static RouteGroupBuilder MapGamesEndpointsCopy(this WebApplication app)
{
    var group = app.MapGroup("games");

    // GET /games
    group.MapGet("/", () => games);

    // GET /games/1
    group.MapGet("/{id}", (int id) => 
    {
        // var game = games.Find(game => game.Id == id);
        GameSummaryDto? game = games.Find(game => game.Id == id);

        return game is null ? Results.NotFound() : Results.Ok(game);
    }).WithName(GetGameEndpointName);

    // POST /games
    group.MapPost("/", (CreateGameDto newGame) => 
    {
        // Game game = new()
        // {
        //     Name = newGame.Name,
        //     Genre = dbContext.Genres.Find(newGame.GenreId),
        //     GenreId = newGame.GenreId,
        //     Price = newGame.Price,
        //     ReleaseDate = newGame.ReleaseDate
        // };

        // if (string.IsNullOrEmpty(newGame.Name))
        // {
        //     return Results.BadRequest("Name is Required");
        // }

        GameSummaryDto game = new(
            games.Count + 1,
            newGame.Name,
            Convert.ToString(newGame.GenreId),
            newGame.Price,
            newGame.ReleaseDate);

            // games.Add(game);

        // GameDto clientGame = new
        // (
        //     game.Id,
        //     game.Name,
        //     game.Genre!.Name,
        //     game.Price,
        //     game.ReleaseDate
        // );

            return Results.CreatedAtRoute(GetGameEndpointName, new {id = game.Id}, game);
    }).WithParameterValidation();

    // PUT /games
    group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) => 
    {
        var index = games.FindIndex(game => game.Id == id);
        if (index == -1)
        {
            return Results.NotFound();
        }
        games[index] = new GameSummaryDto(
            id,
            updatedGame.Name,
            Convert.ToString(updatedGame.GenreId),
            updatedGame.Price,
            updatedGame.ReleaseDate
        );

        return Results.NoContent();
    });

    // DELETE /games/1
    group.MapDelete("/{id}", (int id) => {
        games.RemoveAll(game => game.Id == id);

        return Results.NoContent();
    });

    return group;
}



}
