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
    public partial class FrmCliente : Form
    {
        private DataTable Dt;
        //variable usada para llenar la tabla
        private bool IsNuevo = false;
        private bool IsEditar = false;
        public FrmCliente()
        {
            InitializeComponent();
        }

        private void FrmCliente_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0;
            mostrar();
            //Muestro los registros de clientes cargados hasta el momento
            this.Habilitar(false);
           
            this.Botones();
        }
        private void MensajeOK(string mensaje)
        {

            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Mostrar Mensaje de Error

        private void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }
        private void Habilitar(bool valor)
        {
            if (this.IsNuevo)
            {
                this.txtIdcliente.ReadOnly = true;
                ttMensaje.SetToolTip(txtIdcliente, "No es necesario agregar un idcliente para ingresar un cliente");
            }
            else if (this.IsEditar)
            {
                this.txtIdcliente.ReadOnly = false;
                ttMensaje.SetToolTip(txtIdcliente, "El idcliente debe ser un valor entero existente en la grilla entero");
               

            }
            else {
                this.txtIdcliente.ReadOnly = !valor;
            }
      
         
            this.txtNombre.ReadOnly = !valor;
            this.txtTelefono.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.txtApellido.ReadOnly = !valor;
            this.txtDni.ReadOnly = !valor;
            //si es solo lectura no se permite agregar nombre
        }

        private void Botones()
        {
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
            else
            {
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

        private void Limpiar()
        {
            this.txtDni.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtIdcliente.Text = string.Empty;
            this.txtApellido.Text = string.Empty;
        }
        private void mostrar() {
            try
            {
                Dt = NCliente.Mostrar();
                this.DataListado.DataSource = Dt;
                //LLammo al mostrar de la capa de negocio y me devuelve la tabla llena de datos
                this.DataListado.Columns[0].Visible = false;
                //no muestro la columna Eliminar
                if (Dt.Rows.Count != 0)
                {
                    //Si tengo datos en la grilla
                    txtBuscar.Enabled = true;
                    inexistente.Visible = false;
                }
                else
                {
                    txtBuscar.Enabled = false;
                    inexistente.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            btnNuevo.Enabled = true;
            btnEditar.Enabled = false;
        


        }

        private void buscar() {

            try
            {
                if (NCliente.BuscarTexto(cbocampo.Text, txtBuscar.Text).Rows.Count != 0)
                {
                    this.DataListado.DataSource = NCliente.BuscarTexto(cbocampo.Text,txtBuscar.Text);
                    //LLammo al mostrar de la capa de negocio y me devuelve la tabla llena de datos
                    inexistente.Visible = false;
                }
                else
                {
                    this.DataListado.DataSource = null;
                    this.inexistente.Visible = true;

                }
              

            }
            catch (Exception ex) {

                MessageBox.Show(ex.Message);
            }
        }

        private void cbocampo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.txtBuscar.Text == "")
            {
                MessageBox.Show("Debe ingresar un texto a buscar", "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                buscar();
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            buscar();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(true);
          
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
          
                this.IsEditar = true;
                this.IsNuevo = false;
                this.Botones();
                this.Habilitar(true);
                MessageBox.Show("Por favor presione doble click encima del elemento de la grilla que quiere modificar");
          
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente desea Eliminar los registros", "Sistema de Ventas", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (opcion == DialogResult.OK)
                {
                    string codigo;
                    string rpta = "";
                    foreach (DataGridViewRow row in this.DataListado.Rows)
                    {
                        //itero en cada fila de mi grilla
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            //Me fijo el valor de cada checkbox , y continuo si esta seleccionado
                            codigo = Convert.ToString(row.Cells[1].Value);
                            //asigno al codigo el valor de la fila en la columna del codigo de categoria
                            rpta = NCliente.Eliminar(Convert.ToInt32(codigo));

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOK("Se elimino correctamente el registro");
                                this.checkBoxEliminar.Checked = false;
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                        }

                    }
                    this.mostrar();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (this.isInt32(this.txtDni.Text.Trim().ToUpper()))
            {
                try
                {
                    string rpta = "";
                    if (this.txtNombre.Text == string.Empty)
                    {
                        MensajeError("Faltan ingresar algunos datos como lo es el nombre");

                    }
                    if ((IsEditar) && (this.txtIdcliente.Text.Equals("")))
                    {
                        MensajeError("Debe ingresar el idcliente para poder modificar");
                    }
                    else
                    {
                        if (this.IsNuevo)
                        {


                            rpta = NCliente.Insertar(this.txtNombre.Text.Trim().ToUpper(), this.txtApellido.Text.Trim().ToUpper(), Convert.ToInt32(this.txtDni.Text.Trim().ToUpper()), this.txtDireccion.Text.Trim().ToUpper(), this.txtTelefono.Text.Trim().ToUpper());
                            //trim() elimina espacios en blanco
                            //ToUpper() convierte a mayusculas
                        }



                        else
                        {

                            rpta = NCliente.Editar(Convert.ToInt32(this.txtIdcliente.Text), this.txtNombre.Text.Trim().ToUpper(), this.txtApellido.Text.Trim().ToUpper(), Convert.ToInt32(this.txtDni.Text.Trim().ToUpper()), this.txtDireccion.Text.Trim().ToUpper(), this.txtTelefono.Text.Trim().ToUpper());



                        }
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
                    this.mostrar();
                }




                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.StackTrace);
                }
            }
            else {
                MessageBox.Show("Ingrese un valor entero para el Dni por favor");
                txtDni.Text = "";
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxEliminar.Checked)
            {
                this.DataListado.Columns[0].Visible = true;
            }
            else
            {
                this.DataListado.Columns[0].Visible = false;
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void DataListado_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.txtIdcliente.Text = Convert.ToString(this.DataListado.CurrentRow.Cells["Id"].Value);
            this.txtNombre.Text = Convert.ToString(this.DataListado.CurrentRow.Cells["Nombre"].Value);
            this.txtApellido.Text = Convert.ToString(this.DataListado.CurrentRow.Cells["Apellido"].Value);
            this.txtDireccion.Text = Convert.ToString(this.DataListado.CurrentRow.Cells["Direccion"].Value);
            this.txtTelefono.Text = Convert.ToString(this.DataListado.CurrentRow.Cells["Telefono"].Value);
            this.txtDni.Text = Convert.ToString(this.DataListado.CurrentRow.Cells["Dni"].Value);

            this.btnCancelar.Enabled = true;
        }

        private void DataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == DataListado.Columns["Eliminar"].Index)
            {

                //Si el indice de la columna corresponde a eliminar
                DataGridViewCheckBoxCell chkEliminar = (DataGridViewCheckBoxCell)DataListado.Rows[e.RowIndex].Cells["Eliminar"];
                chkEliminar.Value = !Convert.ToBoolean(chkEliminar.Value);
                //Se determina que chekbox he seleccionado y se le cambia el valor que tiene
            }
        }

        public bool isInt32(String num)
        {
            try
            {
                Int32.Parse(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
