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
    public partial class ModificaPartes : Form
    {
        public ModificaPartes(Principal principal, PropietarisManager propietaris_manager, PointsManager points_manager, ServerManager server_manager, UIManager ui_manager)
        {
            InitializeComponent();
            Carrega(principal, propietaris_manager, points_manager, server_manager, ui_manager);
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            ActualitzaLlistaTreballs();
            ActualitzaLListaUnitatsMesura();
            CarregaInformacioInicial();
            ActualitzaLlistaPersonal();
            ActualitzaLlistaMaquinaria();
            ActualitzaListaAdob();

            grid.CleanSelection();
            fertirrigacio_checkbox.SetSelected(false);

            eficacia_combobox.Clear();
            eficacia_combobox.AddElement("0"); eficacia_combobox.AddElement("1");
            eficacia_combobox.AddElement("2"); eficacia_combobox.AddElement("3");

            treballs_combobox.CleanSelection();
            descripcio_text_input.SetText("");
            unitats_text_input.SetText("");
            fertirrigacio_checkbox.SetSelected(false);
            adob_combobox.CleanSelection();
            personal_combobox.CleanSelection();
            maquinaria_combobox.CleanSelection();
            unitats_mesura_combobox.CleanSelection();
            eficacia_combobox.CleanSelection();
        }

        // ---------------------------------------------------------------------- Botons
        // -----------------------------------------------------------------------------

        public void EliminaLineaParte(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;
            
            string[] str = grid.GetSelectedRow();

            int id = int.Parse(str[3]);

            tblLineasPartesFinca linea = propietaris_manager.GetLineaPartePerLineaID(id);
            
            partes_linea_per_eliminar.Add(linea);

            for(int a = 0; a < partes_linea_per_afegir.Count; a++)
            {
                if (partes_linea_per_afegir[a].idLinea == linea.idLinea)
                {
                    partes_linea_per_afegir.RemoveAt(a);
                    break;
                }
            }             

            grid.DeleteRow(grid.GetSelectedRowIndex());
        }

        public void LineaParteClick(object sender, EventArgs e)
        {
            if (!grid.IsSelected())
                return;

            tblLineasPartesFinca linea_actual = null;
            Treball treball = null;
            UnitatMetrica unitat = null;
            Personal personal = null;
            Maquina maquina = null;
            Adob adob = null;

            string[] str = grid.GetSelectedRow();

            int id = int.Parse(str[3]);

            List<tblLineasPartesFinca> lineas = propietaris_manager.GetLineasPerParteId(propietaris_manager.parte_actual.idParte);

            for (int i = 0; i < lineas.Count; i++)
            {
                if (lineas[i].idLinea == id)
                {
                    linea_actual = lineas[i];
                    break;
                }
            }

            if (linea_actual == null)
                return;

            for (int y = 0; y < partes_linea_per_afegir.Count; y++)
            {
                if (partes_linea_per_afegir[y].idLinea == linea_actual.idLinea)
                {
                    linea_actual = partes_linea_per_afegir[y];
                    break;
                }
            }

            treball = propietaris_manager.GetTreballPerTreballId(linea_actual.idFamiliaCoste);

            if(linea_actual.idUnitatMetrica != null)
                unitat = propietaris_manager.GetUnitatMetricaPerId((int)linea_actual.idUnitatMetrica);

            if (treball == null)
                return;

            personal = propietaris_manager.GetPersonalPerId(linea_actual.idAplicador.ToString());
            maquina = propietaris_manager.GetMaquinaPerId(linea_actual.idMaquinaria.ToString());
            adob = propietaris_manager.GetAdobPerId(linea_actual.idProduteFito.ToString());

            treballs_combobox.SetSelectedElement(treball.GetTbl().Descripcio);
            data_dataselect.SetDate((DateTime)propietaris_manager.parte_actual.Fecha);
            descripcio_text_input.SetText(linea_actual.Descripcion);
            unitats_text_input.SetText(linea_actual.Unidades.ToString());
            fertirrigacio_checkbox.SetSelected((bool)linea_actual.FertirrigacioSiNo);

            if (adob != null)
                adob_combobox.SetSelectedElement(adob.ToString());
            else
                adob_combobox.CleanSelection();

            if (personal != null)
                personal_combobox.SetSelectedElement(personal.ToString());
            else
                personal_combobox.CleanSelection();

            if (maquina != null)
                maquinaria_combobox.SetSelectedElement(maquina.ToString());
            else
                maquinaria_combobox.CleanSelection();

            if (unitat != null)
                unitats_mesura_combobox.SetSelectedElement(unitat.ToString());
            else
                unitats_mesura_combobox.CleanSelection();

            eficacia_combobox.SetSelectedElement(linea_actual.EficaciaTractament.ToString());

            propietaris_manager.parte_linea_actual = linea_actual;
        }

        public void ModificaParteSeleccionat(object sender, EventArgs e)
        {
            decimal test;
            if (!grid.IsSelected() || !treballs_combobox.IsSelected() || propietaris_manager.parte_linea_actual == null || !decimal.TryParse(unitats_text_input.GetText(), out test))
                return;

            Treball treball = treballs_combobox.GetSelected() as Treball;
            UnitatMetrica unitat = unitats_mesura_combobox.GetSelected() as UnitatMetrica;
            Maquina maquina = maquinaria_combobox.GetSelected() as Maquina;
            Personal personal = personal_combobox.GetSelected() as Personal;
            Adob adob = adob_combobox.GetSelected() as Adob;

            tblLineasPartesFinca nova_linea = new tblLineasPartesFinca();

            nova_linea.CodigoEmpresa = propietaris_manager.parte_linea_actual.CodigoEmpresa;
            nova_linea.idFamiliaCoste = propietaris_manager.parte_linea_actual.idFamiliaCoste;
            nova_linea.idLinea = propietaris_manager.parte_linea_actual.idLinea;
            nova_linea.idParcela = propietaris_manager.parte_linea_actual.idParcela;
            nova_linea.idParte = propietaris_manager.parte_linea_actual.idParte;
            nova_linea.Observaciones = propietaris_manager.parte_linea_actual.Observaciones;
            nova_linea.Precio = propietaris_manager.parte_linea_actual.Precio;
            nova_linea.Total = propietaris_manager.parte_linea_actual.Total;

            nova_linea.Descripcion = descripcio_text_input.GetText();
            nova_linea.idFamiliaCoste = treball.GetTbl().idCost;
            nova_linea.Unidades = decimal.Parse(unitats_text_input.GetText());
            nova_linea.FertirrigacioSiNo = fertirrigacio_checkbox.IsSelected();

            if(maquinaria_combobox.IsSelected())
                nova_linea.idMaquinaria = int.Parse(maquina.GetTbl().id);

            if(personal_combobox.IsSelected())
                nova_linea.idAplicador = int.Parse(personal.GetTbl().id);

            if (eficacia_combobox.IsSelected())
                nova_linea.EficaciaTractament = int.Parse((string)eficacia_combobox.GetSelected());

            if (unitat != null)
                nova_linea.idUnitatMetrica = unitat.GetTbl().id;

            if (adob != null)
                nova_linea.idProduteFito = adob.GetTbl().id;

            // Comprova que aquesta linea no ha sigut ja modificata i actualitza
            for (int y = 0; y < partes_linea_per_afegir.Count; y++)
            {
                if (partes_linea_per_afegir[y].idLinea == nova_linea.idLinea)
                {
                    partes_linea_per_afegir.RemoveAt(y);
                    break;
                }
            }

            partes_linea_per_eliminar.Add(propietaris_manager.parte_linea_actual);
            partes_linea_per_afegir.Add(nova_linea);

            propietaris_manager.parte_linea_actual = nova_linea;

            Parcela parcela = propietaris_manager.GetParcelaPerParcelaID(nova_linea.idParcela.ToString());
            tblPartesFinca parte = propietaris_manager.GetPartePerParteId(nova_linea.idParte);

            grid.ModifyRow(grid.GetSelectedRowIndex(), treball, nova_linea.Descripcion, nova_linea.Unidades.ToString(), 
                nova_linea.idLinea.ToString(), unitat, parte.Estat, parcela.GetTbl().idParcelaVinicola, parcela.GetTbl().Ha, 
                (bool)nova_linea.FertirrigacioSiNo ? "Si" : "No", nova_linea.EficaciaTractament, personal, maquina, adob);
        }

        public void Accepta(object sender, EventArgs e)
        {
            List<tblLineasPartesFinca> lin = propietaris_manager.GetPartesLinea();

            for (int i = 0; i < partes_linea_per_eliminar.Count; i++)
            {
                for (int l = 0; l < lin.Count; l++)
                {
                    if (partes_linea_per_eliminar[i] == lin[l])
                    {
                        server_manager.DeleteLineaParteFinca(partes_linea_per_eliminar[i]);
                        break;
                    }
                }

                propietaris_manager.EliminaParteLinea(partes_linea_per_eliminar[i]);
            }

            for (int i = 0; i < partes_linea_per_afegir.Count; i++)
            {
                propietaris_manager.AfegirParteLinea(partes_linea_per_afegir[i]);
                server_manager.AddLineaParteFinca(partes_linea_per_afegir[i]);
            }

            partes_linea_per_afegir.Clear();
            partes_linea_per_eliminar.Clear();

            // Elimina partes buits
            List<tblPartesFinca> partes = propietaris_manager.GetPartes();

            for(int i = 0; i < partes.Count;)
            {
                List<tblLineasPartesFinca> lineas = propietaris_manager.GetLineasPerParteId(partes[i].idParte);

                if (lineas.Count == 0)
                {
                    server_manager.DeleteParteFinca(partes[i]);
                    propietaris_manager.EliminaParte(partes[i]);
                }
                else
                    ++i;
            }

            server_manager.SubmitChanges();

            this.Close();
        }

        // ---------------------------------------------------------------------- Botons
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Servidor --------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        // -------------------------------------------------------------------- Servidor
        // -----------------------------------------------------------------------------


        // -----------------------------------------------------------------------------
        // Utils -----------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        public string GetEstat()
        {
            if (pendent_check.GetChecked())
                return "pendent";

            if (proces_check.GetChecked())
                return "proces";

            if (acabat_check.GetChecked())
                return "acabat";

            return "";
        }

        public void SetEstat(string estat)
        {
            if (estat == "pendent")
                pendent_check.Check();
            if (estat == "proces")
                proces_check.Check();
            if (estat == "acabat")
                acabat_check.Check();
        }
        

        // ----------------------------------------------------------------------- Utils
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Gmap ------------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        // ------------------------------------------------------------------------ Gmap
        // -----------------------------------------------------------------------------

        // -----------------------------------------------------------------------------
        // Actualitza ------------------------------------------------------------------
        // -----------------------------------------------------------------------------

        public void CarregaInformacioInicial()
        {
            if (propietaris_manager.parte_actual == null || propietaris_manager.parte_actual == null)
                return;

            Propietari propietari = propietaris_manager.GetPropietariPerParte(propietaris_manager.parte_actual);
            Finca finca = propietaris_manager.GetFincaPerParte(propietaris_manager.parte_actual);

            if(propietari != null && finca != null)
            {
                propietari_nom_text.SetText(propietari.GetTbl().Nombre);
                finca_nom_text.SetText(finca.GetTbl().Nom1);

                ActualitzaLlistaPartesLlista();
            }
        }

        public void ActualitzaLlistaPartesLlista()
        {
            grid.Clear();

            if (propietaris_manager.parte_actual == null)
                return;

            List<tblLineasPartesFinca> lineas = propietaris_manager.GetLineasPerParteId(propietaris_manager.parte_actual.idParte);

            List<Parcela> parceles = propietaris_manager.GetParcelesSeleccionades();

            for(int i = 0; i < lineas.Count; i++)
            {
                Parcela parcela = null;
                for(int p = 0; p < parceles.Count; p++)
                {
                    if (parceles[p].GetTbl().idParcela == lineas[i].idParcela)
                        parcela = parceles[p];
                }

                if (parcela == null)
                    continue;

                Treball treball = propietaris_manager.GetTreballPerTreballId(lineas[i].idFamiliaCoste);
                tblPartesFinca parte = propietaris_manager.GetPartePerParteId(propietaris_manager.parte_actual.idParte);
                Personal personal = propietaris_manager.GetPersonalPerId(lineas[i].idAplicador.ToString());
                Maquina maquina = propietaris_manager.GetMaquinaPerId(lineas[i].idMaquinaria.ToString());
                Adob adob = propietaris_manager.GetAdobPerId(lineas[i].idProduteFito.ToString());

                string metrica_nom = "";
                if (lineas[i].idUnitatMetrica != null)
                {
                    UnitatMetrica metrica = propietaris_manager.GetUnitatMetricaPerId((int)lineas[i].idUnitatMetrica);
                    metrica_nom = metrica.GetTbl().Unitat;
                }

                grid.AddRow(treball, lineas[i].Descripcion, lineas[i].Unidades, lineas[i].idLinea.ToString(), metrica_nom, parte.Estat, 
                    parcela.GetTbl().idParcelaVinicola, parcela.GetTbl().Ha, (bool)lineas[i].FertirrigacioSiNo ? "Si" : "No", 
                    lineas[i].EficaciaTractament, personal, maquina, adob);
            }

            grid.CleanSelection();
        }

        public void ActualitzaLlistaTreballs()
        {
            treballs_combobox.Clear();

            List<Treball> treballs = propietaris_manager.GetTreballs();

            for (int i = 0; i < treballs.Count; i++)
            {
                treballs_combobox.AddElement(treballs[i]);
            }
        }

        private void ActualitzaLListaUnitatsMesura()
        {
            unitats_mesura_combobox.Clear();

            List<UnitatMetrica> unitats = propietaris_manager.GetUnitatsMetriques();

            for(int i = 0; i < unitats.Count; i++)
            {
                unitats_mesura_combobox.AddElement(unitats[i]);
            }
        }

        public void ActualitzaLlistaPersonal()
        {
            personal_combobox.Clear();

            List<Personal> personal = propietaris_manager.GetPersonal();

            for(int i = 0; i < personal.Count; i++)
            {
                personal_combobox.AddElement(personal[i]);
            }
        }

        public void ActualitzaLlistaMaquinaria()
        {
            maquinaria_combobox.Clear();

            List<Maquina> maquinaria = propietaris_manager.GetMaquinaria();

            for (int i = 0; i < maquinaria.Count; i++)
            {
                maquinaria_combobox.AddElement(maquinaria[i]);
            }
        }

        public void ActualitzaListaAdob()
        {
            adob_combobox.Clear();

            List<Adob> adobs = propietaris_manager.GetAdobs();

            for(int i = 0; i < adobs.Count; i++)
            {
                adob_combobox.AddElement(adobs[i]);
            }
        }
        // ------------------------------------------------------------------ Actualitza
        // -----------------------------------------------------------------------------

    }
}


