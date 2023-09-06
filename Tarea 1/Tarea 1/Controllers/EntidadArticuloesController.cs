using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Tarea_1.Data;
using Tarea_1.Models;

namespace Tarea_1.Controllers
{
    public class EntidadArticuloesController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EntidadArticuloesController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: EntidadArticuloes
        public async Task<IActionResult> Index()
        {
              //return _context.Articulo != null ? 
                //          View(await _context.Articulo.ToListAsync()) :
                  //        Problem("Entity set 'ApplicationDBContext.Articulo'  is null.");

            /*
            SqlConnection conn = (SqlConnection) _context.Database.GetDbConnection();
            SqlCommand cmd = conn.CreateCommand();
            conn.Open();
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.CommandText = "spPrueba_Consulta";
            cmd.ExecuteNonQuery();
            //conn.Close();
            */
			IEnumerable<EntidadArticulo> articulos = (IEnumerable<EntidadArticulo>) _context.Articulo.FromSqlInterpolated($"SP_ConsultaOrdenadaAlfabticamente").AsAsyncEnumerable();
            

			//IEnumerable<EntidadArticulo> articulos = _context.Articulo.ToList();
			
            return View(articulos);

        }

        // GET: EntidadArticuloes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Articulo == null)
            {
                return NotFound();
            }

            var entidadArticulo = await _context.Articulo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entidadArticulo == null)
            {
                return NotFound();
            }

            return View(entidadArticulo);
        }

        // GET: EntidadArticuloes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EntidadArticuloes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Precio")] EntidadArticulo entidadArticulo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SqlConnection conn = (SqlConnection)_context.Database.GetDbConnection();
                    SqlCommand cmd = conn.CreateCommand();
                    conn.Open();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "SP_InsertarArticulo";
                    cmd.Parameters.Add("@Nombre", System.Data.SqlDbType.NVarChar, 128).Value = entidadArticulo.Nombre;
                    cmd.Parameters.Add("@Precio", System.Data.SqlDbType.Money).Value = entidadArticulo.Precio;
                    cmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = entidadArticulo.Id;
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    //_context.Add(entidadArticulo);
                    //await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (SqlException ex)
                {
                    // Captura la excepción generada por el procedimiento almacenado
                    string errorMessage = ex.Message;
                    // Muestra el mensaje de error en tu página
                    TempData["Message"] = "Ocurrió un error: " + ex.Message;
                }
            }
            return View(entidadArticulo);
        }

        // GET: EntidadArticuloes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Articulo == null)
            {
                return NotFound();
            }

            var entidadArticulo = await _context.Articulo.FindAsync(id);
            if (entidadArticulo == null)
            {
                return NotFound();
            }
            return View(entidadArticulo);
        }

        // POST: EntidadArticuloes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Precio")] EntidadArticulo entidadArticulo)
        {
            if (id != entidadArticulo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(entidadArticulo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EntidadArticuloExists(entidadArticulo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(entidadArticulo);
        }

        // GET: EntidadArticuloes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Articulo == null)
            {
                return NotFound();
            }

            var entidadArticulo = await _context.Articulo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (entidadArticulo == null)
            {
                return NotFound();
            }

            return View(entidadArticulo);
        }

        // POST: EntidadArticuloes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Articulo == null)
            {
                return Problem("Entity set 'ApplicationDBContext.Articulo'  is null.");
            }
            var entidadArticulo = await _context.Articulo.FindAsync(id);
            if (entidadArticulo != null)
            {
                _context.Articulo.Remove(entidadArticulo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EntidadArticuloExists(int id)
        {
          return (_context.Articulo?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        public IActionResult PRUEBA()
        {
            return View();
        }

        public IActionResult Prueba_kkk()
        {
            return RedirectToAction(nameof(PRUEBA));
            //return NotFound();
        }



    }
}
