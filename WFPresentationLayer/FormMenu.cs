using DataAccessLayer;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace WFPresentationLayer
{
    public partial class FormMenu : Form
    {
        bool ligar = false;
        System.Timers.Timer timer = new System.Timers.Timer();
        Horarios horario = null;
        string comPort = "";
        List<string> horarios = new List<string>();
        HorariosDAL dal = new HorariosDAL();
        ParametrosDAL pdal = new ParametrosDAL();
        Parametros parametro = null;

       
        //Esta função será executada a cada 1 SEGUNDO
        public void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //Se o horários de agora está contido na lista de intervalos calculados
            if (horarios.Contains(DateTime.Now.ToString("dd/MM/yyyy - HH:mm:ss")))
            {
                //Cria o objeto Portal Serial com as configurações explanadas pela Kathlen
                using (SerialPort serial = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One))
                {
                    try
                    {
                        serial.Open();
                        if (serial.IsOpen)
                        {
                            Virada virada = horario.Parametro.CalcularViradas();
                            string s = virada.Caracter;
                            //Para cada virada definida nos Parâmetros
                            for (int i = 0; i < virada.Qtd; i++)
                            {
                                //Tranforma o caracter A ou caracter B para um vetor de bytes
                                //Gerando um vetor de 8 posições com os valores
                                //01000001 = A
                                //01000010 = B
                                byte[] dados = Encoding.UTF8.GetBytes(s);
                                //Manda o vetor inteiro para o arduino
                                serial.Write(dados, 0, 1);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }

        public FormMenu()
        {
            InitializeComponent();
        }

        private void btnHorarios_Click(object sender, EventArgs e)
        {
            FormHorarios frmH = new FormHorarios();
            this.Hide();
            frmH.ShowDialog();
            this.Show();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnParametros_Click(object sender, EventArgs e)
        {
            FormParametros frmP = new FormParametros();
            this.Hide();
            frmP.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ligar = !ligar;
            if (ligar)
            {
                //Lê o ultimo registro no banco de dados da tabela horários
                this.horario = dal.Lista().Last();

                btnParar.Text = "Parar";
                //Chama a função CalcularHorários que define todos os horários aos quais a aplicação irá efetuar a comunicação com o Arduino
                this.horarios = horario.CalcularHorarios().ToDateString();
                //Lê os parâmetros para calcular o numero de viradas e a angulação necessária
                this.parametro = pdal.Lista().First(c => c.ID == horario.ParametroID);
                horario.Parametro = this.parametro;
                timer.Elapsed += timer_Elapsed;
                //Define o intervalo de 1 segundo para checagem de horários
                timer.Interval = 1000;
                comPort = SerialPort.GetPortNames().Last();
                //Inicia  
                timer.Start();
            }
            else
            {
                btnParar.Text = "Iniciar";
                timer.Stop();
                timer.Elapsed -= timer_Elapsed;
            }
        }
    }
}
