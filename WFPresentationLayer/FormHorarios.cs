using DataAccessLayer;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFPresentationLayer
{
    public partial class FormHorarios : Form
    {
        public FormHorarios()
        {
            InitializeComponent();
            horario = new Horarios();
            parametro = new Parametros();
            horario.DataFim = DateTime.Now;
            horario.DataInicio = DateTime.Now;
            dgvVisualizarParametros.SelectionMode = 
                 DataGridViewSelectionMode.FullRowSelect;
            dgvVisuzalizarHorarios.SelectionMode =
                 DataGridViewSelectionMode.FullRowSelect;

            dgvVisuzalizarHorarios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVisualizarParametros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvVisuzalizarHorarios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvVisualizarParametros.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }

        private Horarios horario;
        private Parametros parametro;

        private void btnIniciar_Click(object sender, EventArgs e)
        {
           try
           {
               parametro = PrencherPropriedadesParametros();
               int id = Convert.ToInt32(lblIDParametros.Text);
               ParametrosDAL pDAL = new ParametrosDAL();

               if (id == 0)
               {
                   pDAL.Adiciona(parametro);
                   
               }
               horario = PrencherPropriedadesHorarios();
               //horario.Parametro = parametro;
               horario.ParametroID = parametro.ID;
               HorariosDAL hDAl = new HorariosDAL();
               hDAl.Adiciona(horario);
               MessageBox.Show("Cadastrado com Sucesso!!!");
               AtualizarForm();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
        }

        private void FormHorarios_Load(object sender, EventArgs e)
        {
            try
            {
                cmbAnimal.DataSource = Enum.GetNames(typeof(TipoAnimal));
                cmbTamanho.DataSource = Enum.GetNames(typeof(Tamanho));
                cmbGramas.SelectedIndex = 0;
                dtpInicio.Value = horario.DataInicio;
                dtpTermino.Value = horario.DataFim;
                
                AtualizarForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           
        }
        private void AtualizarForm()
        {
            
            List<Parametros> parametros = new ParametrosDAL().Lista();
            dgvVisualizarParametros.DataSource = parametros;
            List<Horarios> horarios = new HorariosDAL().Lista();
            dgvVisuzalizarHorarios.DataSource = horarios;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        public void AtualizarCampos(Horarios horarios, Parametros ParametroID)
        {
            dtpInicio.Text = horarios.DataInicio.ToString();
            dtpTermino.Text = horarios.DataFim.ToString();
            lblIDHorarios.Text = horarios.ID.ToString();
            lblIDParametros.Text = ParametroID.ID.ToString();
            txtIntervalo.Text = horario.Intervalo.ToString();
            lblIDParametros.Visible = true;
            lblIDHorarios.Visible = true;

            for (int i = 0; i < cmbGramas.Items.Count; i++)
            {
                if (cmbGramas.Items[i].ToString() == ParametroID.Gramas.ToString())
                {
                    cmbGramas.SelectedIndex = i;
                }
            }
            for (int i = 0; i < cmbTamanho.Items.Count; i++)
            {
                if (cmbTamanho.Items[i].ToString() == ParametroID.Tamanho.ToString())
                {
                    cmbTamanho.SelectedIndex = i;
                }
            }
            for (int i = 0; i < cmbAnimal.Items.Count; i++)
            {
                if (cmbAnimal.Items[i].ToString() == ParametroID.Tipo.ToString())
                {
                    cmbAnimal.SelectedIndex = i;
                }
            }


        }

        

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                parametro = PrencherPropriedadesParametros();
                horario = PrencherPropriedadesHorarios();
                horario.Parametro = parametro;
                int id = Convert.ToInt32(lblIDParametros.Text);
                int idh = Convert.ToInt32(lblIDHorarios.Text);
                ParametrosDAL pDAL = new ParametrosDAL();
                HorariosDAL hDAl = new HorariosDAL();
                if (id == 0)
                {
                    pDAL.Adiciona(parametro);
                }
                else
                {
                    parametro.ID = id;
                    pDAL.Atualiza(parametro);
                    lblIDParametros.Text = "0";

                }
                if (idh == 0)
                {
                    hDAl.Adiciona(horario);
                }
                else
                {
                    horario.ID = idh;
                    horario.ParametroID = parametro.ID;
                    hDAl.Atualiza(horario);
                    lblIDHorarios.Text = "0";

                }
                AtualizarForm();
                MessageBox.Show("Atualizado com sucesso!!");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            try
            {
                parametro = PrencherPropriedadesParametros();
                horario = PrencherPropriedadesHorarios();
                horario.Parametro = parametro;
                int id = Convert.ToInt32(lblIDParametros.Text);
                int idh = Convert.ToInt32(lblIDHorarios.Text);
                ParametrosDAL pDAL = new ParametrosDAL();
                HorariosDAL hDAl = new HorariosDAL();
                if (id == 0 && idh == 0)
                {
                    MessageBox.Show("Não há coluna selecionada ");
                    return;
                }
                else if (id != 0)
                {
                    parametro.ID = id;
                    pDAL.Deletar(parametro);

                }
                else if (idh != 0)
                {
                    parametro.ID = id;
                    horario.ID = idh;
                    horario.ParametroID = parametro.ID;
                    hDAl.Deletar(horario);
                }
                AtualizarForm();
                lblIDHorarios.Text = "0";
                lblIDParametros.Text = "0";
                MessageBox.Show("Deletado com sucesso!");


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }


        }

        private Parametros PrencherPropriedadesParametros()
        {
            Parametros parametros = new Parametros();
            parametros.Tamanho = (Tamanho)cmbTamanho.SelectedIndex;
            parametros.Tipo = (TipoAnimal)cmbAnimal.SelectedIndex;
            parametros.Gramas = Convert.ToInt32(cmbGramas.SelectedItem);
            parametros.ID = Convert.ToInt32(lblIDParametros.Text);
            return parametros;
        }
        private Horarios PrencherPropriedadesHorarios()
        {

            Horarios horarios = new Horarios();
            horarios.DataInicio = dtpInicio.Value;
            horarios.DataFim = dtpTermino.Value;
            horarios.Intervalo = Convert.ToInt32(txtIntervalo.Text);
            return horarios;
        }

        private void dgvVisualizarParametros_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametro =
               (Parametros)this.dgvVisualizarParametros.Rows[e.RowIndex].DataBoundItem;

            AtualizarCampos(horario, parametro);
        }

        private void dgvVisuzalizarHorarios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            horario =
               (Horarios)this.dgvVisuzalizarHorarios.Rows[e.RowIndex].DataBoundItem;

            AtualizarCampos(horario, parametro);

        }

        private void btnResetar_Click(object sender, EventArgs e)
        {
            lblIDHorarios.Text = "0";
            lblIDParametros.Text = "0";
            dtpInicio.Value = DateTime.Now;
            dtpInicio.Value = DateTime.Now;
            txtIntervalo.Text = "10";
            cmbAnimal.SelectedIndex = 0;
            cmbGramas.SelectedIndex = 0;
            cmbTamanho.SelectedIndex = 0;
        }



    }
}
