using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class ParametrosDAL
    {
        public ParametrosDAL()
        {
            contexto = new PetFeederContext();
        }
        private PetFeederContext contexto;

        public void Adiciona(Parametros Parametro)
        {
            using (contexto )
            {
                contexto.Parametros.Add(Parametro);
                contexto.SaveChanges();
            }
        }

        public List<Parametros> Lista()
        {
            using (contexto)
            {
                return contexto.Parametros.ToList();
            }
        }


        public void Atualiza(Parametros Parametro)
        {
            using (contexto)
            {
                contexto.Entry(Parametro).State = 
                System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }
        public void Deletar(Parametros parametros)
        {
            using (contexto)
            {
                contexto.Entry(parametros).State 
                = EntityState.Deleted;
                contexto.SaveChanges();
            }
        }
    }
}