using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PEDL_Guia3_Ejercicio1
{
    public partial class Form1 : Form
    {
        Queue<Empleados> Trabajadores = new Queue<Empleados>();
        //Creamos objeto de la clase cola del tipo de la clase empleado 
        //(Lo que almacena son objetos)

        private bool Validarcampo() //Método auxiliar para las validaciones
        {
            //variable que verifica si algo ha sido validado
            bool validado = true;
            if (txtnombre.Text == "") //vefica que no quede vacío el campo
            {
                validado = false; //si está vacío validado es falso
                errorProvider1.SetError(txtnombre, "Ingresar nombre"); //por lo tanto manda a llamar a errorprovider
                                                                       //en los parámetros de setError se identifica a quién estoy validando y el mensaje que deseo mandar        
            }

            //Verifico la casilla de nombre
            if (txtnombre.Text == "")
            {
                validado = false; //Digo que verifico a txtnombre y si no cumple mando lo siguiente
                errorProvider1.SetError(txtnombre, "Ingrese nombre");
            }

            //Verifico la casilla de carnet
            if (mtcarnet.Text == "")
            {
                validado = false;
                errorProvider1.SetError(mtcarnet, "Ingrese un carnet");
            }

            //Verifico la casilla del salario
            if (txtsalario.Text == "")
            {
                validado = false;
                errorProvider1.SetError(txtsalario, "Ingrese un salario");
            }
            return validado;
        }

        private void Borrarmensaje()
        {
            //Borra los mensajes para que no se muestren y pueda limpiar 

            errorProvider1.SetError(mtcarnet, "");
            errorProvider1.SetError(txtnombre, "");
            errorProvider1.SetError(txtsalario, "");
            errorProvider1.SetError(dtpfecha, "");
        }

        private void Limpiar()
        {
            mtcarnet.Clear();
            txtnombre.Clear();
            txtsalario.Clear();
        }
        
        public Form1()
        {
            InitializeComponent();
        }

        private void btnregistrar_Click(object sender, EventArgs e)
        {

            Empleados empleado = new Empleados(); //Creamos instancia de la clase empleado

            Borrarmensaje(); //Limpia cualquier mensaje de error de alguna corrida previa

            if (dtpfecha.Value >= DateTime.Now.Date)
            {
                errorProvider1.SetError(dtpfecha, "Fecha no disponible");
            }
            else
            {
                if (Validarcampo())
                {

                    //Capturamos los datos del empleado
                    empleado.Carnet = mtcarnet.Text;
                    empleado.Nombre = txtnombre.Text;
                    empleado.Salario = Decimal.Parse(txtsalario.Text);
                    empleado.Fecha = dtpfecha.Value;

                    Trabajadores.Enqueue(empleado); //Llamamos al metodo encolar para meter a la estructura
                    dgvtabla.DataSource = null;
                    dgvtabla.DataSource = Trabajadores.ToArray(); //Para pasarlo al Dgv convertimos la cola en arreglo
                    Limpiar(); //Se limpian los textbox

                    
                }

                //Verificamos la fecha de nacimiento

                mtcarnet.Focus();//Se coloca el cursor sobre el primer textbox
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            if (Trabajadores.Count != 0) //Mientras haya trabajadores en la cola
            {
                Empleados empleado = new Empleados(); //Instanciamos de la clase empleado
                /* Este objeto se usa para poder recuperar los datos y 
                mostrarlos en los textbox al momento de ser eliminados de la cola */
                empleado = Trabajadores.Dequeue(); //Llamamos la metodo desencolar

                //Colocamos los datos en sus textbox
                mtcarnet.Text = empleado.Carnet;
                txtnombre.Text = empleado.Nombre;
                txtsalario.Text = empleado.Salario.ToString();
                dtpfecha.Value = empleado.Fecha;

                //La estructura convertida en la lista se le pasa al DGV
                dgvtabla.DataSource = Trabajadores.ToList();
                MessageBox.Show("Se eliminó el registro en la cola", "AVISO");
                Limpiar();
            }
            else
            {
                MessageBox.Show("No hay empleados en la cola", "AVISO");
                Limpiar();
            }
            mtcarnet.Focus();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Sale de la aplicación
        }

        private void txtsalario_KeyPress(object sender, KeyPressEventArgs e)
        {
            //condicion para solo números
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = false;
            }
            //para tecla backspace
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }

            /* Verifica que pueda ingresar un solo punto */
            else if ((e.KeyChar == '.') && (!txtsalario.Text.Contains(".")))
            {
                e.Handled = false;
            }
            //si no se cumple nada de lo anterior entonces que no lo deje pasar
            else
            {
                e.Handled = true;
                MessageBox.Show("Solo se admiten datos numericos", "Validación de numeros",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

            }
        }

        private void txtnombre_KeyPress(object sender, KeyPressEventArgs e)
        {
            //condicion para solo números
            if (char.IsLetter(e.KeyChar))
            {
                e.Handled = false;
            }
            //para backspace
            else if (char.IsControl(e.KeyChar))
            {
                e.Handled = false;
            }
            //para que admita tecla de espacio
            else if (char.IsSeparator(e.KeyChar))
            {
                e.Handled = false;
            }
            //si no cumple nada de lo anterior que no lo deje pasar
            else
            {
                e.Handled = true;
               MessageBox.Show("Solo se admiten letras", "validación de texto",MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
