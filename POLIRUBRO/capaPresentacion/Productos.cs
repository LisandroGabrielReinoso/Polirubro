﻿using POLIRUBRO.capaNegocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POLIRUBRO.capaPresentacion
{
    public partial class Productos : Form
    {
        public Productos()
        {
            InitializeComponent();
        }
        //Creo un objeto cargaProducto
        CargarProducto cargarProducto = new CargarProducto();
        Verificar verificar = new Verificar();


        private void Productos_Load(object sender, EventArgs e)
        {
            //Cargo inicialmente los valores a los Combo box desde la base de datos
            boxCategoria = cargarProducto.cargar_comboBox(boxCategoria,"Nombre_categoria","Categoria");
            boxProveedor = cargarProducto.cargar_comboBox(boxProveedor, "Nombre", "Proveedor");
            boxUnidad = cargarProducto.cargar_comboBox(boxUnidad, "Nombre_Unidad", "Unidad");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Producto p = new Producto();
            //Extraigo los datos del formulario y verifico que no esten vacios
            if (!verificar.Verificar_vacio_comboBox(boxProveedor) ||
                    !verificar.Verificar_vacio_comboBox(boxCategoria) ||
                    !verificar.Verificar_vacio_txt(txtCodigoBarra) ||
                    !verificar.Verificar_vacio_txt(txtProducto) ||
                    !verificar.Verificar_vacio_txt(txtStock) ||
                    !verificar.Verificar_vacio_txt(txtPrecio) ||
                    !verificar.Verificar_vacio_comboBox(boxUnidad))
            {
                MessageBox.Show("Todos los campos son obligatorios. Por favor, complete todos los campos.", "Campos Vacíos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // Finaliza el proceso si hay campos vacíos
            }
            // Asignación de valores si todos los campos están 
            p.codigoBarra = Convert.ToInt32(txtCodigoBarra.Text);
            p.nombre = txtProducto.Text;
            p.stock = Convert.ToInt32(txtStock.Text);
            p.precio = Convert.ToDouble(txtPrecio.Text);
            p.proveedor = cargarProducto.buscar_id("Nombre","Id_Proveedor","Proveedor", boxProveedor.SelectedItem.ToString());
            p.categoria = cargarProducto.buscar_id("Nombre_Categoria", "Id_Categoria", "Categoria", boxCategoria.SelectedItem.ToString());
            p.unidad = cargarProducto.buscar_id("Nombre_Unidad","Id_Unidad","Unidad",boxUnidad.SelectedItem.ToString());

            //Llamo a la funcion cargarProducto y le paso el producto(p)
            cargarProducto.cargarProducto(p);
        }

        private void txtProducto_KeyPress(object sender, KeyPressEventArgs e)
        {
            verificar.Verificar_proveedor(e);//Verificaciones sobre el campo Proovedor
        }
                                                                      
        private void txtProducto_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
