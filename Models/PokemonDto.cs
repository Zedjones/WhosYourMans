using System.Collections.Generic;
using System;
using System.Linq;

namespace GuessWegmons.Models
{
    /// <summary>
    /// Stores information/data about a Pokemon.
    /// </summary>
    [Serializable]
    public class PokemonDto
    {
        /// <summary>
        /// Name of the Pokemon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type(s) the Pokemon has.
        /// </summary>
        public List<string> Types { get; set; }

        /// <summary>
        /// The Pokemon's generation (the game it is from in numeric form).
        /// </summary>
        public int Generation { get; set; }

        /// <summary>
        /// Id of the Pokemon.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Url of the sprite for the Pokemon.
        /// </summary>
        public string PictureURL { get; set; }

        /// <summary>
        /// Create a Pokemon Dto object.
        /// </summary>
        /// <param name="fromMon">Pokemon object is based on</param>
        public PokemonDto(PokeApiNet.Pokemon fromMon, PokeApiNet.PokemonSpecies fromSpecies)
        {
            Name = fromMon.Name.Contains("-mega") ? "Mega " : "";
            Name += fromSpecies.Names.Where(name => name.Language.Name == "en").FirstOrDefault().Name;
            Generation = Int32.Parse(new Uri(fromSpecies.Generation.Url).Segments.Last().TrimEnd('/'));
            PictureURL = fromMon.Sprites.FrontDefault;
            Types = fromMon.Types.Select(type => type.Type.Name).ToList();
            Id = fromMon.Id;
        }
    }
}