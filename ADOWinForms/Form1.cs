using ADOWinForms.ADO;
using ADOWinForms.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin.Controls;
using MaterialSkin;

namespace ADOWinForms
{
    public partial class Form1 : MaterialForm
    {
        TipoAccion accion;
        int id;
        ADOEstatusAlumno _objEstatus = new ADOEstatusAlumno();

        private SqlConnection conexion = new SqlConnection();
        private SqlCommand comando = new SqlCommand();
        public Form1()
        {

            InitializeComponent();
            estatusAlumnos();

        }

        public void estatusAlumnos()
        {
            dgvMuestra.DataSource = null;
            ADOEstatusAlumno estatusAlumno = new ADOEstatusAlumno();
            cbxMuestra.DataSource = null;
            dgvMuestra.DataSource = estatusAlumno.ObtenerTodos();
            cbxMuestra.DataSource = estatusAlumno.ObtenerTodos();
            cbxMuestra.ValueMember = "id";
            cbxMuestra.DisplayMember = "nombre";

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text != "" && txtClave.Text != "")
            {
                if (accion == TipoAccion.agregar)
                {
                    _objEstatus.Agregar(Convert.ToString(txtNombre.Text), txtClave.Text);
                    estatusAlumnos();
                    MessageBox.Show("Agregado Correctamente");
                    MostrarBotones();
                }
                else if (accion == TipoAccion.actualizar)
                {
                    int idEstatus = Convert.ToInt32(cbxMuestra.SelectedValue);
                    string nombre = txtNombre.Text;
                    string clave = txtClave.Text;
                    DialogResult resultDia = MessageBox.Show("¿Desea Actualizar Sus Datos", "Actualizar Datos", MessageBoxButtons.YesNo);
                    if (resultDia == DialogResult.Yes)
                    {
                        _objEstatus.Actualizar(idEstatus, nombre, clave);
                        estatusAlumnos();

                    }
                }
            }
            
            
        }

        private void dgvMuestra_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                id = Convert.ToInt32(dgvMuestra.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtNombre.Text = dgvMuestra.CurrentRow.Cells[1].Value.ToString();
                txtClave.Text = dgvMuestra.CurrentRow.Cells[2].Value.ToString();
            }
            catch
            {

            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            OcultarBotones();
            accion = TipoAccion.actualizar;
            int idEstatus = Convert.ToInt32(cbxMuestra.SelectedValue);
            EstatusAlumno nombreEstatus =  _objEstatus.ConsultarUno(idEstatus);
            txtNombre.Text = nombreEstatus.nombre;
            txtClave.Text = nombreEstatus.clave;
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int idEstatus = Convert.ToInt32(cbxMuestra.SelectedValue);
            if (idEstatus != 0)
            {
                string nombre = txtNombre.Text;
                string clave = txtClave.Text;
                DialogResult resultDial = MessageBox.Show("¿Desea Eliminar Sus Datos", "Eliminar Datos", MessageBoxButtons.YesNo);
                if (resultDial == DialogResult.Yes)
                {
                    _objEstatus.Eliminar(idEstatus);
                    estatusAlumnos();
                }
            }
            else
            {
                
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            MostrarBotones();
        }

        private void OcultarBotones()
        {
            btnActualizar.Visible = false;
            btnEliminar.Visible = false;
            btnAgregar.Visible = false;
            pnlDatos.Visible = true;
            
        }

        private void MostrarBotones()
        {
            btnActualizar.Visible = true;
            btnEliminar.Visible = true;
            btnAgregar.Visible = true;
            pnlDatos.Visible = false;
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            OcultarBotones();
            accion = TipoAccion.agregar;
            txtNombre.Text = "";
            txtClave.Text = "";
        }
    }


    
}
