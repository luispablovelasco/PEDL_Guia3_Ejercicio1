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

        private void Limpiar()
        {
            txtcarnet.Clear();
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

            //Capturamos los datos del empleado
            empleado.Carnet = txtcarnet.Text;
            empleado.Nombre = txtnombre.Text;
            empleado.Salario =  Decimal.Parse(txtsalario.Text);
            empleado.Fecha = dtpfecha.Value;

            Trabajadores.Enqueue(empleado); //Llamamos al metodo encolar para meter a la estructura
            dgvtabla.DataSource = null;
            dgvtabla.DataSource = Trabajadores.ToArray(); //Para pasarlo al Dgv convertimos la cola en arreglo
            Limpiar(); //Se limpian los textbox
            txtcarnet.Focus();//Se coloca el cursor sobre el primer textbox
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
                txtcarnet.Text = empleado.Carnet;
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
            txtcarnet.Focus();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Application.Exit(); //Sale de la aplicación
        }
    }
}
