using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReneUtiles.VisualBasico.Clases.Dialogos
{
    public partial class DialogoInformacion : Form
    {
        //private string mensaje,titulo;

        public DialogoInformacion(string titulo, string mensaje)
        {
            InitializeComponent();
            label1.Text = mensaje;
            this.Text = titulo;
            this.Location = new Point(300, 300);
            this.ControlBox = false;
            
        }

        //public void setTextLabel(string text) {
        //    label1.Text = text;
        //}
    }
}
