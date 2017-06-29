﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace WindowsFormsApp4
{
    public class UIManager
    {
        public UIManager(Control control)
        {
            _control = control;
        }

        public void AddUIWindow(UI_Window window)
        {
            windows.Add(window);
        }

        List<UI_Window> windows = new List<UI_Window>();
        Control _control = null;
    }

    public class UI_Window
    {
        public UI_Window(Control control)
        {
            _control = control;
        }

        public void AddElement(UI_Element element)
        {
            _control.Controls.Add(element.GetElement());
            elements.Add(element);
        }

        public void RemoveElement(UI_Element element)
        {
            _control.Controls.Remove(element.GetElement());
        }

        public bool GetEnabled() { return enabled;}

        public void SetEnabled(bool set)
        {
            for (int i = 0; i < elements.Count; i++)
            {
                elements[i].SetEnabled(set);
            }

            enabled = set;
        }

        public List<UI_Element> elements = new List<UI_Element>();
        Control _control = null;
        bool enabled = true;
    }

    public class UI_Element
    {
        public UI_Element(string type)
        {
            _type = type;
        }

        public void SetElement(Control element)
        {
            _element = element;
        }

        public Control GetElement() { return _element; }

        public void SetEnabled(bool set)
        {
            if (_element != null)
                _element.Visible = set;
        }

        public void BringToFront()
        {
            if (_element != null)
                _element.BringToFront();
        }

        public void SendToBack()
        {
            if (_element != null)
                _element.SendToBack();
        }

        public void SetDock(DockStyle dock)
        {
            if (_element != null)
                _element.Dock = dock;
        }

        public void Layer(int layer)
        {
            _element.TabIndex = layer;
        }

        public string GetTyp() { return _type; }

        Control _element = null;
        string _type;
    }

    public class UI_Button : UI_Element
    {
        public UI_Button(Point pos, int w, int h, string text, string name = "") : base("button")
        {
            Button b = new Button();
            b.Name = name;
            b.Location = pos;
            b.Width = w;
            b.Height = h;
            b.Text = text;

            SetElement(b);
        }

        public void SetText(string text)
        {
            Button b = GetElement() as Button;
            b.Text = text;
        }
    }

    public class UI_Panel : UI_Element
    {
        public UI_Panel(Point pos, int w, int h, string name = "") : base("panel")
        {
            Panel p = new Panel();
            p.Name = name;
            p.Location = pos;
            p.Width = w;
            p.Height = h;
            p.TabIndex = 8;
            p.HorizontalScroll.Enabled = false;
            p.HorizontalScroll.Visible = false;
            p.HorizontalScroll.Maximum = 0;
            p.AutoScroll = true;

            SetElement(p);
        }

        public void SetColor(Color back_color)
        {
            Panel p = GetElement() as Panel;
            p.BackColor = back_color;
        }

        public void AddElement(UI_Element element)
        {
            Panel p = GetElement() as Panel;
            p.Controls.Add(element.GetElement());
            elements.Add(element);
        }

        public void RemoveElement(UI_Element element)
        {
            Panel p = GetElement() as Panel;
            p.Controls.Remove(p);

            elements.Remove(element);
        }

        public void ClearPanel()
        {
            Panel p = GetElement() as Panel;
            p.Controls.Clear();

            elements.Clear();
        }

        public List<UI_Element> elements = new List<UI_Element>();
    }

    public class UI_Text : UI_Element
    {
        public UI_Text(Point pos, int w, int h, string text = "", string name = "") : base("text")
        {
            Label l = new Label();
            l.Name = name;
            l.Location = pos;
            l.Width = w;
            l.Height = h;
            l.Text = text;
            l.AutoSize = true;

            SetElement(l);
        }

        public override string ToString()
        {
            Label l = GetElement() as Label;

            return l.Text;
        }

        public void SetText(string text)
        {
            Label l = GetElement() as Label;
            l.Text = text;
        }

        public void SetColor(Color back_color, Color fore_color)
        {
            Label l = GetElement() as Label;
            l.BackColor = back_color;
            l.ForeColor = fore_color;
        }

        public string GetText()
        {
            Label l = GetElement() as Label;
            return l.Text;
        }
    }

    public class UI_TextInput : UI_Element
    {
        public UI_TextInput(Point pos, int w, int h, string text = "", string name = "") : base("text_input")
        {
            MaskedTextBox mt = new MaskedTextBox();
            mt.Name = name;
            mt.Location = pos;
            mt.Width = w;
            mt.Height = h;
            mt.Text = text;
            mt.AutoSize = true;

            SetElement(mt);
        }

        public string GetText()
        {
            MaskedTextBox l = GetElement() as MaskedTextBox;
            return l.Text;
        }

        public void SetText(string text)
        {
            MaskedTextBox l = GetElement() as MaskedTextBox;
            l.Text = text;
        }
    }

    public class UI_ComboBox : UI_Element
    {
        public UI_ComboBox(Point pos, int w, int h, string name = "") : base ("combo")
        {
            ComboBox cb = new ComboBox();
            {
                cb.Name = name;
                cb.Location = pos;
                cb.Width = w;
                cb.Height = h;
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.Sorted = true;

                SetElement(cb);
            }
        }

        public void SetDrowDownVisibleItems(int set)
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.IntegralHeight = false;
            cb.MaxDropDownItems = set;
        }

        public bool IsSelected()
        {
            ComboBox cb = GetElement() as ComboBox;

            if (cb.SelectedIndex > -1)
                return true;

            return false;
        }

        public object GetSelected()
        {
            object ret = null;

            if (IsSelected())
            {
                ComboBox cb = GetElement() as ComboBox;

                ret = cb.Items[cb.SelectedIndex];
            }

            return ret;
        }

        public void CleanSelection()
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.SelectedIndex = -1;
            cb.SelectedItem = null;
        }

        public void AddElement(object one_object)
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.Items.Add(one_object);
        }

        public void Clear()
        {
            ComboBox cb = GetElement() as ComboBox;
            cb.Items.Clear();
        }
    }

    public class UI_ListBox : UI_Element
    {
        public UI_ListBox(Point pos, int w, int h, string name = "") : base("list_box")
        {
            ListBox lb = new ListBox();
            lb.Name = name;
            lb.Location = pos;
            lb.Width = w;
            lb.Height = h;

            SetElement(lb);
        }

        public void AddElement(UI_Element element)
        {
            ListBox lb = GetElement() as ListBox;

            lb.Items.Add(element.GetElement());
        }

        public void AddText(string text)
        {
            ListBox lb = GetElement() as ListBox;
            lb.Items.Add(text);
        }

        public void DeleteElement(Control element)
        {
            ListBox lb = GetElement() as ListBox;

            lb.Items.Remove(element);
        }

        public void Clear()
        {
            ListBox lb = GetElement() as ListBox;

            lb.Items.Clear();
        }

        public Control GetSelected()
        {
            Control ret = null;

            ListBox lb = GetElement() as ListBox;

            if (lb.SelectedIndex >= 0)
                ret = (Control)lb.SelectedItem;
            
            return ret;
        }

        public bool IsSelected()
        {
            bool ret = false;

            ListBox lb = GetElement() as ListBox;

            if (lb.SelectedIndex >= 0)
                ret = true;

            return ret;
        }

        public void ClearSelection()
        {
            ListBox lb = GetElement() as ListBox;
            lb.ClearSelected();
        }
    }
}
