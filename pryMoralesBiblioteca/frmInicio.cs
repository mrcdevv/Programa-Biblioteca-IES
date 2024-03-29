﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace pryMoralesBiblioteca
{
    public partial class frmInicio : Form
    {
        public frmInicio()
        {
            InitializeComponent();
        }

        public string[,] matLibros = new string[21, 5];
        public int contador = 0;
        public bool banDistribuidora = false;
        public bool banEditorial = false;
        public char separador = Convert.ToChar(",");




        private void frmInicio_Load(object sender, EventArgs e)
        {

            int i = 0;


            StreamReader srLibro = new StreamReader("../../../LIBRO.txt");


            while (!srLibro.EndOfStream && i < 21)
            {
                string[] vecLibro = srLibro.ReadLine().Split(separador);

                //Borrar los espacios en blanco del archivo LIBROS.txt
                for (int pos = 0; pos < vecLibro.Length; pos++)
                {
                    vecLibro[pos] = Regex.Replace(vecLibro[pos], @"\t", "");
                }

                //Uso el vector para cargar la info del libro en las columnas de la matriz
                matLibros[i, 0] = vecLibro[0];
                matLibros[i, 1] = vecLibro[1];
                matLibros[i, 2] = vecLibro[2];
                matLibros[i, 3] = vecLibro[3];
                matLibros[i, 4] = vecLibro[4];

                //Busco el nombre de la editorial y lo cargo en la matriz
                buscarEditorial(i);

                //Busco el nombre de la distribuidora y la cargo en la matriz
                buscarDistribuidora(i);

                //Limpio las banderas
                banDistribuidora = false;
                banEditorial = false;
                i++;
            }
            srLibro.Close();


            txtCodLibro.Text = matLibros[0, 0];
            txtNombre.Text = matLibros[0, 1];
            txtNomEditorial.Text = matLibros[0, 2];
            txtCodAutor.Text = matLibros[0, 3];
            txtNomDistribuidor.Text = matLibros[0, 4];

            btnAnterior.Enabled = false;
        }


        private void buscarEditorial(int index)
        {

            StreamReader srEditorial = new StreamReader("../../../EDITORIAL.txt");
            
            while (!srEditorial.EndOfStream && !banEditorial)
            {
                string[] vecEditorial = srEditorial.ReadLine().Split(separador);

                //Limpio los espacios en blanco del archivo EDITORIAL.txt
                for (int pos = 0; pos < vecEditorial.Length; pos++)
                {
                    vecEditorial[pos] = Regex.Replace(vecEditorial[pos], @"\t", "");
                }

                //Coloco el nombre de la distribuidora que corresponde
                if (vecEditorial[0] == matLibros[index, 2])
                {
                    matLibros[index, 2] = vecEditorial[1];
                    banEditorial = true;
                }
            }
            srEditorial.Close();
        }

        private void buscarDistribuidora(int index)
        {

            StreamReader srDistribuidora = new StreamReader("../../../DISTRIBUIDORA.txt");
            
            while (!srDistribuidora.EndOfStream && !banDistribuidora)
            {
                string[] vecDistribuidora = srDistribuidora.ReadLine().Split(separador);

                //Limpio los espacios en blanco del archivo DISTRIBUIDORA.txt
                for (int pos = 0; pos < vecDistribuidora.Length; pos++)
                {
                    vecDistribuidora[pos] = Regex.Replace(vecDistribuidora[pos], @"\t", "");
                }

                //Coloco el nombre de la distribuidora que corresponde
                if (vecDistribuidora[0] == matLibros[index, 4])
                {
                    matLibros[index, 4] = vecDistribuidora[1];
                    banDistribuidora = true;
                }
            }
            srDistribuidora.Close();
        }


        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            contador++;

            txtCodLibro.Text = matLibros[contador, 0];
            txtNombre.Text = matLibros[contador, 1];
            txtNomEditorial.Text = matLibros[contador, 2];
            txtCodAutor.Text = matLibros[contador, 3];
            txtNomDistribuidor.Text = matLibros[contador, 4];

            btnAnterior.Enabled = true;

            if(contador == matLibros.GetLength(0) - 1) 
            {
                btnSiguiente.Enabled = false;
            }
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            contador--;

            if (contador >= 0)
            {
                txtCodLibro.Text = matLibros[contador, 0];
                txtNombre.Text = matLibros[contador, 1];
                txtNomEditorial.Text = matLibros[contador, 2];
                txtCodAutor.Text = matLibros[contador, 3];
                txtNomDistribuidor.Text = matLibros[contador, 4];

                if (contador == 0)
                {
                    btnAnterior.Enabled = false;
                }
            }
            else
            {
                btnAnterior.Enabled = false;
            }

        }
    }
}
