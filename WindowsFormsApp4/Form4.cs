﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp4
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

            this.reportViewer.RefreshReport();
            this.reportViewer.RefreshReport();
        }

        public void SetInfo(List<ReportDataParte> info, string imatge_url, string treball, string data)
        {
            this.Size = new System.Drawing.Size(983, 689);
            this.MaximizeBox = false;
            this.reportViewer.Clear();

            report1DSBindingSource.DataSource = info;

            Microsoft.Reporting.WinForms.ReportParameter[] para = new Microsoft.Reporting.WinForms.ReportParameter[3];
            para[0] = new Microsoft.Reporting.WinForms.ReportParameter("nom", treball);
            para[1] = new Microsoft.Reporting.WinForms.ReportParameter("data", data);
            para[2] = new Microsoft.Reporting.WinForms.ReportParameter("imatge_mapa", imatge_url);

            this.reportViewer.LocalReport.EnableExternalImages = true;
            var setup = this.reportViewer.GetPageSettings();
            setup.Margins = new System.Drawing.Printing.Margins(5, 5, 5, 5);
            setup.Landscape = true;
            this.reportViewer.SetPageSettings(setup);

            this.reportViewer.LocalReport.SetParameters(para);
            this.reportViewer.RefreshReport();
        }

        private void reportViewer_Load(object sender, EventArgs e)
        {

        }
    }
}
