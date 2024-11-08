using Microsoft.AspNetCore.Mvc;
using practicaU2.Models.Entities;
using practicaU2.Models.ViewModels;

namespace practicaU2.Controllers
{
    public class HomeController : Controller
    {
        MapaCurricularContext context = new();
        public IActionResult Index()
        {
         var vm = new IndexViewModel 
         {
             Carreras = context.Carreras.OrderBy(x=>x.Nombre).Select(x=> new CarreraModel
             { Nombre = x.Nombre,
             Plan = x.Plan})
         };
            return View(vm);
        }
        [Route("Info/{nombreCarrera}")]
        public IActionResult Info(string nombreCarrera)
        {
            nombreCarrera = nombreCarrera.Replace("_"," ");
            var vm = context.Carreras.Where(x => x.Nombre == nombreCarrera)
                .Select(x => new InfoViewModel{
                Nombre = x.Nombre,
                Plan = x.Plan,
                Especialidad = x.Especialidad,
                Descripcion = x.Descripcion ?? "No cuenta con descripcion alguna",
                Id = x.Id
            })
            return View();
        }
    }
}
