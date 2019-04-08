using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proagil.Repository;
using ProAgil.Domain;

namespace ProAgil.Repository {
    public class ProAgilRepository : IProAgilRepository {
        private readonly ProAgilContext _context;
        public ProAgilRepository (ProAgilContext context) {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        public void Add<T> (T Entity) where T : class {
            _context.Add (Entity);
        }

        public void Update<T> (T Entity) where T : class {
            _context.Update (Entity);
        }
        public void Delete<T> (T Entity) where T : class {
            _context.Remove (Entity);
        }
        public async Task<bool> SaveChangesAsync () {
            return (await _context.SaveChangesAsync ()) > 0;
        }
        public async Task<Evento[]> GetAllEventoAsync (bool incluidesPalestrantes = false) {
            IQueryable<Evento> query = _context.Eventos
                .Include (c => c.Lotes)
                .Include (c => c.RedeSociais);

            if (incluidesPalestrantes) {
                query = query
                    .Include (pe => pe.PalestrantesEventos)
                    .ThenInclude (p => p.Palestrante);
            }

            query = query.OrderByDescending (c => c.DataEvento);

            return await query.ToArrayAsync ();
        }

        public async Task<Evento> GetAllEventoAsyncById (int EventoId, bool incluidesPalestrantes) {
            IQueryable<Evento> query = _context.Eventos
                .Include (c => c.Lotes)
                .Include (c => c.RedeSociais);

            if (incluidesPalestrantes) {
                query = query
                    .Include (pe => pe.PalestrantesEventos)
                    .ThenInclude (p => p.Palestrante);
            }

            query = query.OrderByDescending (c => c.DataEvento)
                .Where (c => c.Id.Equals (EventoId));

            return await query.FirstOrDefaultAsync ();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema (string tema, bool incluidesPalestrantes) {
            IQueryable<Evento> query = _context.Eventos
                .Include (c => c.Lotes)
                .Include (c => c.RedeSociais);

            if (incluidesPalestrantes) {
                query = query
                    .Include (pe => pe.PalestrantesEventos)
                    .ThenInclude (p => p.Palestrante);
            }

            query = query.OrderByDescending (c => c.DataEvento)
                .Where (c => c.Tema.Contains (tema));

            return await query.ToArrayAsync ();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsync (bool incluidesEventos) {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include (c => c.RedeSociais);

            if (incluidesEventos) {
                query = query
                    .Include (pe => pe.PalestrantesEventos)
                    .ThenInclude (e => e.Evento);
            }

            query = query.OrderBy (c => c.Nome);

            return await query.ToArrayAsync ();
        }

        public async Task<Palestrante> GetAllPalestranteAsyncById (int PalestranteId, bool incluidesEventos) {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include (c => c.RedeSociais);

            if (incluidesEventos) {
                query = query
                    .Include (pe => pe.PalestrantesEventos)
                    .ThenInclude (e => e.Evento);
            }

            query = query.OrderBy (c => c.Nome)
                .Where (p => p.Id.Equals (PalestranteId));

            return await query.FirstOrDefaultAsync ();
        }

        public async Task<Palestrante[]> GetAllPalestranteAsyncByName (string nome, bool incluidesEventos) {
            IQueryable<Palestrante> query = _context.Palestrantes
                .Include (c => c.RedeSociais);

            if (incluidesEventos) {
                query = query
                    .Include (pe => pe.PalestrantesEventos)
                    .ThenInclude (e => e.Evento);
            }

            query = query.OrderBy (c => c.Nome)
                .Where (p => p.Nome.Contains (nome));

            return await query.ToArrayAsync ();
        }

    }
}