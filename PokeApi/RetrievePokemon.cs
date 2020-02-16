using System;
using PokeApiNet;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;

namespace GuessWegmons.PokeApi
{
    /// <summary>
    /// Use the Poke Api to generate a board of pokemon.
    /// </summary>
    public class RetrievePokemon
    {
        /// <summary>
        /// Logger.
        /// </summary>
        private readonly ILogger<RetrievePokemon> logger;

        /// <summary>
        /// Poke Client for utilizing the Poke Api.
        /// </summary>
        private PokeApiClient pokeClient;

        /// <summary>
        /// List of currently selected Pokemon.
        /// </summary>
        private List<int> usedPokemon;

        /// <summary>
        /// Stored random object for generating a random number.
        /// </summary>
        private static Random random = new Random();

        /// <summary>
        /// Create a Retrieve Pokemon object.
        /// </summary>
        /// <param name="logger">Logger</param>
        public RetrievePokemon(ILogger<RetrievePokemon> logger)
        {
            pokeClient = new PokeApiClient();
            usedPokemon = new List<int>();
            this.logger = logger;
        }

        /// <summary>
        /// Create and send a full list of Pokemon.
        /// </summary>
        /// <returns>List of Pokemon</returns>
        public async Task<ConcurrentBag<(Pokemon, PokemonSpecies)>> CreateList()
        {
            var pokemon = new ConcurrentBag<(Pokemon, PokemonSpecies)>();
            var allPokemonList = await pokeClient.GetNamedResourcePageAsync<Pokemon>(Int32.MaxValue, 0);

            for (int i = 0; i < 25; i++)
            {
                PickPokemon(allPokemonList.Count);
            }
            Parallel.ForEach(usedPokemon, i =>
            {
                var resourceTask = pokeClient.GetResourceAsync<Pokemon>(allPokemonList.Results[i]);
                resourceTask.Wait();
                var newPoke = resourceTask.Result;
                while(newPoke.Sprites.FrontDefault is null)
                {
                    var newIndex = PickPokemon(allPokemonList.Count, false);
                    resourceTask = pokeClient.GetResourceAsync<Pokemon>(allPokemonList.Results[newIndex]);
                    resourceTask.Wait();
                    newPoke = resourceTask.Result;
                }
                var newPokeSpeciesTask = pokeClient.GetResourceAsync<PokemonSpecies>(newPoke.Species);
                newPokeSpeciesTask.Wait();
                pokemon.Add((newPoke, newPokeSpeciesTask.Result));
                logger.LogInformation($"Pokemon '{newPoke.Name}' added to board.");
            });
            return pokemon;
        }

        /// <summary>
        /// Get a unique number for a Pokemon to add to the board.
        /// </summary>
        /// <param name="size">Size of the list</param>
        /// <returns>Number for the Pokemon</returns>
        public int PickPokemon(int size, bool addToUsed = true)
        {
            var num = random.Next(0, size - 1);
            // Make sure number is unique
            while (usedPokemon.Contains(num))
                num = random.Next(0, size - 1);
            if (addToUsed)
            {
                usedPokemon.Add(num);
            }
            logger.LogInformation($"Pokemon at Id '{num}' expected to be added to board.");
            return num;
        }
    }
}