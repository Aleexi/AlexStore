using Core.Entities;

namespace API.DTO
{
    public class PokemonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Strength { get; set; }
        public string PokemonType { get; set; }
        public string PokemonAbilitie { get; set; }
        public string PictureURL { get; set; }
    }
}