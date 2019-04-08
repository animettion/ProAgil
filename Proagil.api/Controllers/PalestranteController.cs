using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProAgil.Domain;
using ProAgil.Repository;

namespace Proagil.api.Controllers {
    [Route ("api/[controller]")]
    [ApiController]
    public class PalestranteController : ControllerBase {
        private readonly IProAgilRepository _repo;
        public PalestranteController (IProAgilRepository repo) {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get () {
            try {
                var results = await _repo.GetAllPalestranteAsync (true);

                return Ok (results);
            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, "Bancos de dados Falhou");
            }
        }

        [HttpGet ("{PalestranteId}")]
        public async Task<IActionResult> Get (int PalestranteId) {
            try {
                var results = await _repo.GetAllPalestranteAsyncById (PalestranteId, true);

                return Ok (results);
            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, "Bancos de dados Falhou");
            }
        }

        [HttpGet ("getByNome/{nome}")]
        public async Task<IActionResult> Get (string nome) {
            try {
                var results = await _repo.GetAllPalestranteAsyncByName (nome, true);

                return Ok (results);
            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, "Bancos de dados Falhou");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post (Palestrante model) {
            try {
                _repo.Add (model);
                if (await _repo.SaveChangesAsync ()) {
                    return Created ($"/api/Palestrante/{model.Id}", model);
                }
            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, "Bancos de dados Falhou");
            }
            return BadRequest ();
        }

        [HttpPut]
        public async Task<IActionResult> Put (int PalestranteId, Palestrante model) {
            try {
                var Palestrante = await _repo.GetAllPalestranteAsyncById (PalestranteId, false);
                if (Palestrante == null) return NotFound ();

                _repo.Update (model);

                if (await _repo.SaveChangesAsync ()) {
                    return Created ($"/api/Palestrante/{model.Id}", model);
                }

            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, "Bancos de dados Falhou");
            }
            return BadRequest ();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete (int PalestranteId) {
            try {
                var Palestrante = await _repo.GetAllPalestranteAsyncById (PalestranteId, false);
                if (Palestrante == null) return NotFound ();

                _repo.Delete (Palestrante);

                if (await _repo.SaveChangesAsync ()) {
                    return Ok ();
                }

            } catch (System.Exception) {

                return this.StatusCode (StatusCodes.Status500InternalServerError, "Bancos de dados Falhou");
            }
            return BadRequest ();
        }

    }
}