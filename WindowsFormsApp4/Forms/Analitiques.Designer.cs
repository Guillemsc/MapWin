﻿using System.Drawing;
using System.Collections.Generic;

namespace WindowsFormsApp4
{
    partial class Analitiques
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Analitiques));
            this.SuspendLayout();
            // 
            // Analitiques
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Analitiques";
            this.ResumeLayout(false);

        }

        #endregion

        void Carrega(Principal _principal, PropietarisManager _propietaris_manager, PointsManager _points_manager, 
            ServerManager _server_manager, UIManager _ui_manager)
        {
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(970, 465);
            this.Name = "Analítiques";
            this.Text = this.Name;
            this.Load += new System.EventHandler(this.Form5_Load);
            this.ResumeLayout(false);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Set Managers ----------------------
            propietaris_manager = _propietaris_manager;
            point_manager = _points_manager;
            server_manager = _server_manager;
            ui_manager = _ui_manager;
            principal = _principal;
            // -----------------------------------

            // UI --------------------------------
            LoadUI();
            // -----------------------------------
        }

        void LoadUI()
        {
            main_window = new UI_Window(this);
            {
                grid = new UI_Grid(new Point(15, 15), 470, 405);
                grid.AddColumn("Data", 150, true); grid.AddColumn("IC", 70, true); grid.AddColumn("PH", 70, true);
                grid.AddColumn("Grau alc", 70, true); grid.AddColumn("Densitat produccio", 70, true); grid.AddColumn("Id", 50, true);
                grid.GetElement().Click += new System.EventHandler(this.AnaliticaClick);
                grid.SetFont("Courier New", 8.5f);
                main_window.AddElement(grid);

                data_text = new UI_Text(new Point(500, 20), 30, 120, "Data:");
                data_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(data_text);

                data_dataselect = new UI_DateSelect(new Point(503, 40), 200, 100);
                data_dataselect.SetFont("Courier New", 8.5f);
                main_window.AddElement(data_dataselect);

                intensitat_colorant_text = new UI_Text(new Point(500, 80), 200, 30, "Intensitat colorant:");
                intensitat_colorant_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(intensitat_colorant_text);

                intensitat_colorant_text_input = new UI_MaskedTextInput(new Point(503, 100), 200, 30);
                intensitat_colorant_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(intensitat_colorant_text_input);

                ph_text = new UI_Text(new Point(740, 80), 255, 30, "pH:");
                ph_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(ph_text);

                ph_text_input = new UI_MaskedTextInput(new Point(743, 100), 100, 30);
                ph_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(ph_text_input);

                grau_text = new UI_Text(new Point(500, 140), 255, 30, "Grau alc:");
                grau_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(grau_text);

                grau_text_input = new UI_MaskedTextInput(new Point(503, 160), 100, 30);
                grau_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(grau_text_input);

                densitat_text = new UI_Text(new Point(740, 140), 255, 30, "Densitat prod:");
                densitat_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(densitat_text);

                densitat_text_input = new UI_MaskedTextInput(new Point(743, 160), 100, 30);
                densitat_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(densitat_text_input);

                estat_sanitari_text = new UI_Text(new Point(500, 200), 255, 30, "Estat sanitari:");
                estat_sanitari_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(estat_sanitari_text);

                estat_sanitari_text_input = new UI_MaskedTextInput(new Point(503, 220), 450, 30);
                estat_sanitari_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(estat_sanitari_text_input);

                observacions_text = new UI_Text(new Point(500, 260), 255, 30, "Observacions:");
                observacions_text.SetFont("Courier New", 8.5f);
                main_window.AddElement(observacions_text);

                observacions_text_input = new UI_TextInput(new Point(503, 290), 450, 130);
                observacions_text_input.SetFont("Courier New", 8.5f);
                main_window.AddElement(observacions_text_input);

                elimina_button = new UI_Button(new Point(353, 431), 160, 25, "Elimina analítica");
                elimina_button.GetElement().Click += new System.EventHandler(this.Elimina);
                elimina_button.AddImage(principal.imageList1, 3);
                elimina_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(elimina_button);

                actualitza_button = new UI_Button(new Point(163, 431), 180, 25, "Actualitza analítica");
                actualitza_button.GetElement().Click += new System.EventHandler(this.Actualitza);
                actualitza_button.AddImage(principal.imageList1, 8);
                actualitza_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(actualitza_button);

                crea_button = new UI_Button(new Point(14, 431), 140, 25, "Crea analítica");
                crea_button.GetElement().Click += new System.EventHandler(this.Crea);
                crea_button.AddImage(principal.imageList1, 6);
                crea_button.SetFont("Courier New", 8.5f);
                main_window.AddElement(crea_button);

                accepta_button = new UI_Button(new Point(840, 431), 120, 25, "Desa i surt");
                accepta_button.GetElement().Click += new System.EventHandler(this.Accepta);
                accepta_button.AddImage(principal.imageList1, 2);
                accepta_button.SetFont("Courier New", 8.5f);
                accepta_button.SetColor(Color.Cornsilk);
                main_window.AddElement(accepta_button);
            }
        }

        // UI
        UI_Window main_window = null;
        UI_Grid grid = null;

        UI_Text data_text = null;
        UI_DateSelect data_dataselect = null;

        UI_Text intensitat_colorant_text = null;
        UI_MaskedTextInput intensitat_colorant_text_input = null;

        UI_Text ph_text = null;
        UI_MaskedTextInput ph_text_input = null;

        UI_Text grau_text = null;
        UI_MaskedTextInput grau_text_input = null;

        UI_Text densitat_text = null;
        UI_MaskedTextInput densitat_text_input = null;

        UI_Text estat_sanitari_text = null;
        UI_MaskedTextInput estat_sanitari_text_input = null;

        UI_Text observacions_text = null;
        UI_TextInput observacions_text_input = null;
        
        UI_Button elimina_button = null;

        UI_Button actualitza_button = null;

        UI_Button crea_button = null;

        UI_Button accepta_button = null;



        public PropietarisManager propietaris_manager = null;
        public PointsManager point_manager = null;
        public UIManager ui_manager = null;
        public IDManager id_manager = null;
        public ServerManager server_manager = null;

        Principal principal = null;

        List<Analitica> analitiques_per_afegir = new List<Analitica>();
        List<Analitica> analitiques_per_eliminar = new List<Analitica>();
    }
}