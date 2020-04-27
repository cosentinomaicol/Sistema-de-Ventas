using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapaDeNegocio;

namespace CapaDePresentacion
{
    public partial class FrmCategoria : Form
    {
        private  bool IsNuevo = false;
        private bool IsEditar = false;
        public FrmCategoria()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(txtNombre, "Ingrese por favor el nombre de la categoria");
        }

        //Mostrar Mensaje de Confirmacion
        private void MensajeOK(string mensaje) {

            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Mostrar Mensaje de Error

        private void MensajeError(string mensaje) {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        //Limpiar controles
        private void Limpiar() {
            txtNombre.Text = string.Empty;
            txtidCategoria.Text = string.Empty;
        }

        //procedimiento para habilitar o deshabilitar controles

        private void Habilitar(bool valor) {
            this.txtNombre.ReadOnly = !valor;
            this.txtidCategoria.ReadOnly = !valor;
            //si es solo lectura no se permite agregar nombre
        }

        private void Botones() {
            if (this.IsNuevo || this.IsEditar)
            {
                Habilitar(true);
                //Habilitamos cajas de texto
                btnEditar.Enabled = false;
                btnNuevo.Enabled = false;
                //Deshabilitamos los botones para asi nadie presiona mientras se estan editando las cajas de texto
                btnCancelar.Enabled = true;
                //Habilitamos boton cancelar 
                btnGuardar.Enabled = true;
                //Habilitamos boton guardar para registrar los datos

            }
            else {
                  Habilitar(false);
                  //Habilitamos cajas de texto
                  btnEditar.Enabled = true;
                  btnNuevo.Enabled = true;
                  //Deshabilitamos los botones para asi nadie presiona mientras se estan editando las cajas de texto
                  btnCancelar.Enabled = false;
                  //Habilitamos boton cancelar 
                  btnGuardar.Enabled = false;
                  //Habilitamos boton guardar para registrar los datos


            }

        }

        //Metodo para ocultar columnas
        private void OcultarColumnas() {
            this.dataListado.Columns[0].Visible = false;
            this.dataListado.Columns[1].Visible = false;


        }

        //Metodo para mostrar todas las categorias
        private void Mostrar()
        {
            this.dataListado.DataSource = NCategoria.Mostrar();
            //LLammo al mostrar de la capa de negocio y me devuelve la tabla llena de datos
            OcultarColumnas();
            //Cuando muestro la informacion oculto la columna de eliminar
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.RowCount);

        }
        //Metodo Buscar por Nombre

        private void BuscarNombre() {

            this.dataListado.DataSource = NCategoria.BuscarTexto(txtBuscar.Text);
            //LLammo al mostrar de la capa de negocio y me devuelve la tabla llena de datos
            OcultarColumnas();
            //Cuando muestro la informacion oculto la columna de eliminar
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.RowCount);

        }


        private void FrmCategoria_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            this.Mostrar();
            this.Habilitar(false);
            this.Botones();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.BuscarNombre();

        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            //Que se valla actualizando la grilla a medida que cambia de nombre
            this.BuscarNombre();

        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = "";
                if (this.txtNombre.Text == string.Empty)
                {
                    MensajeError("Faltan ingresar algunos datos serán remarcados");
                    erroricono.SetError(txtNombre, "Ingrese un Nombre");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rpta = NCategoria.Insertar(this.txtNombre.Text.Trim().ToUpper());
                        //trim() elimina espacios en blanco
                        //ToUpper() convierte a mayusculas

                    }
                    else
                    {
                        rpta = NCategoria.Editar(Convert.ToInt32(this.txtidCategoria.Text), this.txtNombre.Text.Trim().ToUpper());
                    }

                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            MensajeOK("Se Insertó de forma correcta el registro");
                        }
                        else
                        {
                            MensajeOK("Se Actualizó de forma correcta el registro");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }
                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Mostrar();
                }             
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {

                //Si el indice de la columna corresponde a eliminar
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
                //Se determina que chekbox he seleccionado y se le cambia el valor que tiene
            }
        }

        private void dataListado_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            this.txtidCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Id"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Descripcion"].Value);
            this.tabControl1.SelectedIndex = 1;
            this.btnCancelar.Enabled = true;
            this.IsEditar = true;
            this.IsNuevo = false;
            this.Botones();
            this.Habilitar(true);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!this.txtidCategoria.Text.Equals(""))
            {
                this.IsEditar = true;
                this.IsNuevo = false;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Debe Ingresar primero el registro a Modificar,para esto valla a la grilla y presione doble click en el elemento a modificar");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);

        }

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;
            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente desea Eliminar los registros", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    string codigo ;
                    string rpta = "";
                    foreach (DataGridViewRow row in this.dataListado.Rows)
                    {
                        //itero en cada fila de mi grilla
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            //Me fijo el valor de cada checkbox , y continuo si esta seleccionado
                            codigo = Convert.ToString(row.Cells[1].Value);
                            //asigno al codigo el valor de la fila en la columna del codigo de categoria
                            rpta = NCategoria.Eliminar(Convert.ToInt32(codigo));

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se elimino correctamente el registro");
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                        }

                      }
                    this.Mostrar();
                    }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
            
