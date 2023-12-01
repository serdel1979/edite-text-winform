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

        Font defaultFont;

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
            DialogResult dr = dlgSaveFile.ShowDialog();

            if (dr == DialogResult.OK)
            {


                string fileToSave = @dlgSaveFile.FileName;

                try
                {

                    File.WriteAllText(@fileToSave, txtContent.Text);

                    lblStatus.Text = "Archivo guardado: " + @fileToSave;

                    isEditing = false;

                    this.Text = "Text Editor App - " + @fileToSave;


                }
                catch (Exception ex) {

                    MessageBox.Show(this, "Error ", ex.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                return;
            }
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
            if (txtContent.WordWrap)
            {
                wordWrapOffToolStripMenuItem.Text = "Word Wrap (ON)";
            }
            else
            {
                wordWrapOffToolStripMenuItem.Text = "Word Wrap (OFF)";
            }
            defaultFont = txtContent.Font;
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

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isEditing)
            {
                DialogResult dialog = MessageBox.Show(this, "Usted está editando este documento " + Environment.NewLine + "¿Descartar cambios? ", "Nuevo documento", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                
                if(dialog != DialogResult.Yes ) {

                    txtContent.Clear();
                    lblStatus.Text = "Nuevo Documento";
                    this.Text = "Text Editor App - Nuevo Documento";
                
                }

            }
            else
            {
                txtContent.Clear();
                lblStatus.Text = "Nuevo Documento";
                this.Text = "Text Editor App - Nuevo Documento";
            }
            isEditing = true;
        }

        private void wordWrapOffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtContent.WordWrap)
            {
                txtContent.WordWrap = false;
                wordWrapOffToolStripMenuItem.Text = "Word Wrap (OFF)";
            }
            else
            {
                txtContent.WordWrap = true;
                wordWrapOffToolStripMenuItem.Text = "Word Wrap (ON)";
            }
            defaultFont = txtContent.Font;
        }

        private void increaseFontSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(txtContent.Font.Size < 18)
            {
                txtContent.Font = new Font("Microsoft Sans Serif",txtContent.Font.Size + 2);
            }
            else
            {
                MessageBox.Show(this, "Máximo Tamaño de fuente ("+Convert.ToInt32(txtContent.Font.Size).ToString()+")","Rechazado",MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void decreaseFontSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (txtContent.Font.Size > 5)
            {
                txtContent.Font = new Font("Microsoft Sans Serif", txtContent.Font.Size - 2);
            }
            else
            {
                MessageBox.Show(this, "Mínimo Tamaño de fuente (" + Convert.ToInt32(txtContent.Font.Size).ToString() + ")", "Rechazado", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void resetFontDefaultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtContent.Font = defaultFont;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,"Editor Text App en .Net con WinForms","Información",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void txtContent_TextChanged(object sender, EventArgs e)
        {
            updateStatusLines();
            isEditing = true;
        }

        private void updateStatusLines()
        {
            lblStatus.Text = "Editando linea "+(txtContent.GetLineFromCharIndex(txtContent.SelectionStart)+1).ToString();
        }

        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.V) {
                txtContent.Text += (string)Clipboard.GetData("Text");
                e.Handled = true;
            }
        }

        private void formMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isEditing)
            {
                DialogResult dr = MessageBox.Show(this, "Hay cambios en el documento. ¿Desea guardarlos?","Salir", MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question);
                
                if(dr != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
