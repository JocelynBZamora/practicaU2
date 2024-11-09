namespace practicaU2.Models.ViewModels
{
    public class MapaViewModel
    {
        public string NombreCarrera { get; set; } = null!;
        public string Plan { get; set; } = null!;
        public int TotalCreditos { get; set; }
        public IEnumerable<SemestreModel> Semestres { get; set; } = Enumerable.Empty<SemestreModel>();
    }

    public class SemestreModel
    {
        public int Numero { get; set; }
        public IEnumerable<MateriaModel> Materias { get; set; } = Enumerable.Empty<MateriaModel>();
    }

    public class MateriaModel
    {
        public string Clave { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public int HorasTeoricas { get; set; }
        public int HorasPracticas { get; set; }
        public int Creditos { get; set; }
    }
}