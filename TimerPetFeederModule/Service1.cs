using DataAccessLayer;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace TimerPetFeederModule
{
    public partial class TimerPetFeederModule : ServiceBase
    {
        public TimerPetFeederModule()
        {
            InitializeComponent();
            this.horario = dal.Lista().Last();
        }

        System.Timers.Timer timer = new System.Timers.Timer();
        Horarios horario = null;
        string comPort = "";
        List<string> horarios = new List<string>();
        HorariosDAL dal = new HorariosDAL();
        ParametrosDAL pdal = new ParametrosDAL();
        Parametros parametro = null;

        protected override void OnContinue()
        {
            timer.Start();
            base.OnContinue();
        }

        public void Teste()
        {
            try
            {
                comPort = SerialPort.GetPortNames().Last();
                this.horarios = horario.CalcularHorarios().ToDateString();
                this.parametro = pdal.Lista().First(c => c.ID == horario.ParametroID);
                timer.Elapsed += timer_Elapsed;
                timer.Interval = 1000;
            }
            catch (Exception ex)
            {
                File.AppendAllText(@"‪C:\Users\moc\Desktop\Teste\log.txt", ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            Thread t = new Thread(new ThreadStart(Teste)); // e.g.
            t.Start();
            Thread.Sleep(130);
        }

        protected override void OnStop()
        {
            timer.Elapsed -= timer_Elapsed;
            timer.Stop();
        }

        public void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (horarios.Contains(DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")))
            {
                using (SerialPort serial = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One))
                {
                    try
                    {
                        serial.Open();
                        if (serial.IsOpen)
                        {
                            Virada virada = horario.Parametro.CalcularViradas();
                            string s = virada.Caracter;
                            for (int i = 0; i < virada.Qtd; i++)
                            {
                                byte[] dados = Encoding.UTF8.GetBytes(s);
                                serial.Write(dados, 0, 1);
                                System.Threading.Thread.Sleep(150);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
    }
}
