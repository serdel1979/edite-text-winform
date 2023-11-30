using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace TextEditApp
{
    public partial class formMain : Form
    {

        Form defaultFont;

        private char[] splitCharacters = "\n ,.:;\"'?!".ToArray();

        bool isEditing = false;

        string fileToOpen;
        string fileToSave;
        string retrievedText = "";

        public formMain()
        {
            InitializeComponent();
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void formatToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void formMain_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (isEditing)
            {
                DialogResult dialog = MessageBox.Show(this, "Usted está editando este documento " + Environment.NewLine + "¿Descartar cambios? ", "Abrir documento",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                if(dialog != DialogResult.Yes)
                {
                    return;
                }
            }
            
            DialogResult dialogResult = dlgOpenFile.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                string fileToOpen = dlgOpenFile.FileName;

                try
                {

                    txtContent.Text = File.ReadAllText(@fileToOpen);
                    lblStatus.Text = "Archivo abierto " + @fileToOpen;

                    isEditing = true;

                    this.Text = "Text Editor App -" + @fileToOpen;

                }catch (Exception ex)
                {
                    MessageBox.Show(this,"Error ",ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                return;
            }
           

        }
    }
}
