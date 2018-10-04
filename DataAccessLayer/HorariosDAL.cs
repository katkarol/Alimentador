using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class HorariosDAL
    {
        public HorariosDAL()
        {
            contexto = new PetFeederContext();
        }
        private PetFeederContext contexto;
        public void Adiciona( Horarios horario)
        {
            using (contexto)
            {
                contexto.Horarios.Add(horario);
                contexto.SaveChanges();
            }
        }

        public List<Horarios> Lista()
        {
            using (contexto)
            {
                return contexto.Horarios.ToList();
            }
        }


        public void Atualiza(Horarios Horario)
        {
            using (contexto)
            {
                contexto.Entry(Horario).State =
              System.Data.Entity.EntityState.Modified;
                contexto.SaveChanges();
            }
        }
        public void Deletar(Horarios horarios)
        {
            using (contexto)
            {
                contexto.Entry(horarios).State = 
                    EntityState.Deleted;
                contexto.SaveChanges();
            }
        }
    }
}
