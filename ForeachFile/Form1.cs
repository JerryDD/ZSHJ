using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Configuration;

namespace ForeachFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            GenHTMLBiz.GenHTML();
        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = GenHTMLBiz.LoadXML();
            string fileName=txtSearch.Text;
            string result = GenHTMLBiz.GetLastModifyDateTime(ref  xmlDoc, fileName);
            txtResult.Text = result;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            XmlDocument xmlDoc = GenHTMLBiz.LoadXML();
            string fileName = txtSearch.Text;
            string result = txtResult.Text;
            GenHTMLBiz.UpdateNode(ref xmlDoc, fileName, result);
            txtResult.Text = result;
            GenHTMLBiz.SaveXML(xmlDoc);
        }
    }
}
