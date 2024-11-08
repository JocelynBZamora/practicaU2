namespace practicaU2.Models.ViewModels
{
	public class IndexViewModel
	{
		public IEnumerable<CarreraModel> Carreras { get; set; } = Enumerable.Empty<CarreraModel>();
    }
	public class CarreraModel
	{
        public string Nombre { get; set; } = null!;
        public string Plan { get; set; } = null!;
    }
}
