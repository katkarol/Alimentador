using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class Horarios
    {
        public int ID { get; set; }
        public int ParametroID { get; set; }
        [Browsable(false)]
        public virtual Parametros Parametro { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int Intervalo { get; set; }

        public List<DateTime> CalcularHorarios()
        {
            DateTime inicio = this.DataInicio;
            DateTime fim = this.DataFim;
            TimeSpan ts = fim - inicio;
            List<DateTime> datas = new List<DateTime>();
            int intervalo = this.Intervalo;
            int count = intervalo;
            for (int i = 0; i < ts.TotalSeconds / intervalo; i++)
            {
                datas.Add(inicio.AddSeconds(count));
                count += intervalo;
            }
            return datas;
        }
    }
    public static class DatetimeExtension
    {
        public static List<string> ToDateString(this List<DateTime> times)
        {
            List<string> dates = new List<string>();
            foreach (var item in times)
            {
                dates.Add(item.ToString("dd/MM/yyyy - HH:mm:ss"));
            }
            return dates;
        }
    }
}
