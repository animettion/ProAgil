using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository {
    public interface IProAgilRepository {
        void Add<T> (T Entity) where T : class;
        void Update<T> (T Entity) where T : class;
        void Delete<T> (T Entity) where T : class;
        Task<bool> SaveChangesAsync ();

        Task<Evento[]> GetAllEventoAsyncByTema (string tema, bool incluidesPalestrantes);
        Task<Evento[]> GetAllEventoAsync (bool incluidesPalestrantes);
        Task<Evento> GetAllEventoAsyncById (int EventoId, bool incluidesPalestrantes);
        Task<Palestrante[]> GetAllPalestranteAsyncByName (string nome, bool incluidesEventos);
        Task<Palestrante> GetAllPalestranteAsyncById (int PalestranteId, bool incluidesEventos);
        Task<Palestrante[]> GetAllPalestranteAsync ( bool incluidesEventos);
    }
}