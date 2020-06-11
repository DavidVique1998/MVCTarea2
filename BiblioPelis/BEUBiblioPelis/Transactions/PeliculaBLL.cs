using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEUBiblioPelis.Transactions
{
    public class PeliculaBLL
    {
        //TBLL Transactional Bissines Logic Layer
        //Capa de Logica del Negocio
        public static void Create(pelicula p)
        {   //Se crea una instancia de la Entidad Base de datos
            using (Entities db = new Entities())
            {
                //Se crea una instancia de la transaccion
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.peliculas.Add(p);
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public static pelicula Get(int? id)
        {
            Entities db = new Entities();
            return db.peliculas.Find(id);
        }
        public static void Update(pelicula pelicula)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        db.peliculas.Attach(pelicula);
                        db.Entry(pelicula).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public static void Delete(int? id)
        {
            using (Entities db = new Entities())
            {
                using (var transaction = db.Database.BeginTransaction())
                {
                    try
                    {
                        pelicula pelicula = db.peliculas.Find(id);
                        db.Entry(pelicula).State = System.Data.Entity.EntityState.Deleted;
                        db.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                }
            }
        }
        public static List<pelicula> List()
        {
            Entities db = new Entities();
            return db.peliculas.ToList();
        }

        private static List<pelicula> GetPeliculas(string criterio)
        {
            Entities db = new Entities();
            return db.peliculas.Where(x => x.categoria.ToLower().Contains(criterio)).ToList();
        }

        private static pelicula GetPelicula(string nombre)
        {
            Entities db = new Entities();
            return db.peliculas.FirstOrDefault(x => x.nombre == nombre);
        }
    }
}
