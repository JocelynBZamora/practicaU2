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
                Carreras = context.Carreras.OrderBy(x => x.Nombre).Select(x => new CarreraModel
                {
                    Nombre = x.Nombre,
                    Plan = x.Plan
                })
            };
            return View(vm);
        }
        [Route("Info/{nombreCarrera}")]
        public IActionResult Info(string nombreCarrera)
        {
            nombreCarrera = nombreCarrera.Replace("_", " ");
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
            var existe = context.Carreras.Any(x => x.Nombre.ToLower() == nombreCarrera);
            var vm = context.Carreras.Where(z => z.Nombre.ToLower() == nombreCarrera).
                Select(x => new MapaViewModel
                {
                    NombreCarrera = x.Nombre,
                    Plan = x.Plan,
                    TotalCreditos = context.Materias.Where(c => c.IdCarreraNavigation.Nombre == x.Nombre)
                   .Sum(m => m.Semestre),
                    Semestres = context.Materias.Where(m => m.IdCarreraNavigation.Nombre == x.Nombre).
                   GroupBy(m => m.Semestre).OrderBy(m => m.Key).Select(s => new SemestreModel
                   {
                       Numero = s.Key,
                       Materias = s.Select(m => new MateriaModel
                       {
                           Clave = m.Clave,
                           Creditos = m.Creditos,
                           HorasPracticas = m.Creditos,
                           HorasTeoricas = m.HorasTeoricas,
                           Nombre = m.Nombre

                       })
                   }).ToList()
                }).First();


            return View(vm);
        }
    }
}

