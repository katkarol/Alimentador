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
    public partial class FormParametros : Form
    {
        public FormParametros()
        {
            InitializeComponent();
            parametro = new Parametros();
            dgvVisualizarParametros.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvVisualizarParametros.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVisualizarParametros.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
        }
        private Parametros parametro;


        private void btnFechar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AtualizarForm()
        {

            List<Parametros> parametros = new ParametrosDAL().Lista();
            dgvVisualizarParametros.DataSource = parametros;
        }

        private Parametros PrencherPropriedadesParametros()
        {
            Parametros parametros = new Parametros();
            parametros.Tamanho = (Tamanho)cmbTamanho.SelectedIndex;
            parametros.Tipo = (TipoAnimal)cmbTipoAnimal.SelectedIndex;
            parametros.Gramas = Convert.ToInt32(cmbGramas.SelectedItem);
            parametros.ID = Convert.ToInt32(lblID.Text);
            return parametros;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            ParametrosDAL pDAL = new ParametrosDAL();
            int id = Convert.ToInt32(lblID.Text);
            try
            {
               parametro = PrencherPropriedadesParametros();
               if (id == 0)
               {
                   pDAL.Adiciona(parametro);
                   
               }
               //horario.Parametro = parametro;
               HorariosDAL hDAl = new HorariosDAL();
               MessageBox.Show("Cadastrado com sucesso!!!");
               AtualizarForm();
           }
           catch (Exception ex)
           {
               MessageBox.Show(ex.Message);
           }
        }
        public void AtualizarCampos(Parametros ParametroID)
        {
            lblID.Text = ParametroID.ID.ToString();
            lblID.Visible = true;

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
            for (int i = 0; i < cmbTipoAnimal.Items.Count; i++)
            {
                if (cmbTipoAnimal.Items[i].ToString() == ParametroID.Tipo.ToString())
                {
                    cmbTipoAnimal.SelectedIndex = i;
                }
            }


        }

        private void dgvVisualizarParametros_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            parametro =
               (Parametros)this.dgvVisualizarParametros.Rows[e.RowIndex].DataBoundItem;

            AtualizarCampos(parametro);
        }

        private void FormParametros_Load(object sender, EventArgs e)
        {
            try
            {
                cmbTipoAnimal.DataSource = Enum.GetNames(typeof(TipoAnimal));
                cmbTamanho.DataSource = Enum.GetNames(typeof(Tamanho));
                cmbGramas.SelectedIndex = 0;

                AtualizarForm();
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
                int id = Convert.ToInt32(lblID.Text);
                ParametrosDAL pDAL = new ParametrosDAL();
                HorariosDAL hDAl = new HorariosDAL();
                if (id == 0)
                {
                    MessageBox.Show("Não há coluna selecionada ");
                    return;
                }
                else if (id != 0)
                {
                    parametro.ID = id;
                    pDAL.Deletar(parametro);
                    lblID.Text = "0";

                }
                AtualizarForm();
                MessageBox.Show("Deletado com sucesso!");
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnAtualizar_Click(object sender, EventArgs e)
        {
            try
            {
                parametro = PrencherPropriedadesParametros();
                int id = Convert.ToInt32(lblID.Text);
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
                    lblID.Text = "0";

                }
                MessageBox.Show("Atualizado com sucesso!");
                AtualizarForm();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnResetar_Click(object sender, EventArgs e)
        {
            lblID.Text = "0";
            cmbTipoAnimal.SelectedIndex = 0;
            cmbGramas.SelectedIndex = 0;
            cmbTamanho.SelectedIndex = 0;
        }
    }
}
