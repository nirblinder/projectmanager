using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjectsManager
{
    public partial class Form_ShowNote : Form
    {
        string text,id;
        DBinterface interf;
        Form1 mainForm;
        public Form_ShowNote(string noteId, string inputText, DBinterface inter, Form1 form)
        {
            InitializeComponent();
            text = inputText;
            id = noteId;
            interf = inter;
            this.mainForm = form;
        }
        private void Form_ShowNote_Load(object sender, EventArgs e)
        {
            textBoxMain.Text = text;
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void buttonDelete_Click(object sender, EventArgs e)
        {
            /// R U SURE?
            interf.Delete("notes", "idNotes", id);
            this.Close();
            mainForm.refreshView();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            string input = textBoxMain.Text.Replace(@"\", @"\\").Replace("'", @"\'").Replace('"', '\"');
            string query = "UPDATE notes SET text ='" + input + "' WHERE idNotes='" + id + "'";
            interf.Update(query);
            this.Close();
            mainForm.refreshView();
        }
    }
}
