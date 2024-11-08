using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var existente = context.Carreras.Any(x => x.Nombre.ToLower() == nombreCarrera);
            if (existente is false) 
            {
                return RedirectToAction("Index");
            }
            var vm = context.Carreras.
                Where(x => x.Nombre.ToLower() == nombreCarrera)
                .Select(x => new InfoViewModel
                {
                    Nombre = x.Nombre,
                    Plan = x.Plan,
                    Especialidad = x.Especialidad,
                    Descripcion = x.Descripcion ?? "No cuenta con descripcion alguna",
                    Id = x.Id
                }).First();
            return View(vm);
        }
        [Route("Mapa/{nombreCarrera}")]

        public IActionResult Mapa(string nombreCarrera) 
        {
            nombreCarrera = nombreCarrera.Replace("_", " ");
            var existe = context.Carreras.Any(c => c.Nombre.ToLower() == nombreCarrera);

            if (existe is false) return RedirectToAction("Index");

            var vm = context.Carreras
                .Where(x => x.Nombre.ToLower() == nombreCarrera)
                .Select(x => new MapaViewModel
                {
                    NombreCarrera = x.Nombre,
                    Plan = x.Plan,
                    TotalCreditos = context.Materias
                        .Where(x => x.IdCarreraNavigation.Nombre == x.Nombre)
                        .Sum(m => m.Creditos),
                    Semestres = context.Materias
                        .Where(x => x.IdCarreraNavigation.Nombre == x.Nombre)
                        .GroupBy(x => x.Semestre)
                        .OrderBy(x => x.Key)
                        .Select(x => new SemestreModel
                        {
                            Numero = x.Key,
                            Materias = x.Select(x => new MateriaModel
                            {
                                Clave = x.Clave,
                                Nombre = x.Nombre,
                                HorasTeoricas = x.HorasTeoricas,
                                HorasPracticas = x.HorasPracticas,
                                Creditos = x.Creditos
                            })
                        })
                        .ToList()
                })
                .First();

            return View(vm);
        }
    }
}
