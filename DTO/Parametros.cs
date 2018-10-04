using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Parametros
    {
        public int ID { get; set; }
        public Tamanho Tamanho { get; set; }
        public TipoAnimal Tipo { get; set; }
        public int Gramas { get; set; }

        public Virada CalcularViradas()
        {
            Virada v = new Virada();
            switch (this.Tamanho)
            {
                case Tamanho.Filhote:
                    v.Qtd = 2;
                    v.Caracter = "b";
                    break;
                case Tamanho.Pequeno:
                    v.Qtd = 2;
                    v.Caracter = "a";
                    break;
                case Tamanho.Medio:
                    v.Qtd = 4;
                    v.Caracter = "b";
                    break;
                case Tamanho.Grande:
                    v.Qtd = 4;
                    v.Caracter = "a";
                    break;
                case Tamanho.Gigante:
                    v.Qtd = 6;
                    v.Caracter = "a";
                    break;
                default:
                    break;
            }
            return v;
        }
    }
}
