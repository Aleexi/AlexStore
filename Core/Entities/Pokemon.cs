namespace Core.Entities
{
    public class Pokemon : SuperEntity
    {
        public string Name { get; set; }

        public int Strength { get; set; }

        public string PictureURL { get; set; }

        // Entity framework creates foreign key connected to the table PokemonTypes
        public PokemonType PokemonType { get; set; }

        public int PokemonTypeId { get; set; }

        // Entity framework creates foreign key connected to the table PokemonAbilities
        public PokemonAbilitie PokemonAbilitie { get; set; }

        public int PokemonAbilitieId { get; set; }        
    }
}