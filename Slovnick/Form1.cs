using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slovnick
{

    public partial class Form1 : Form
    {
        private Dictionary<string, string> slovnick = new Dictionary<string, string>();
        /*
         * { "apple" : "yabloko" ; "orange" :"appelsin";
         * 
         */
        private string filePath = "slovnick.txt"; 

        public Form1()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // .Add() - lодати в словник
            // slovncik[key] = value; додати/оновити

            string word = txtWord.Text.Trim();
            string translate = txtTranslate.Text;

            if (!string.IsNullOrEmpty(word) && !string.IsNullOrEmpty(translate))
            {
                slovnick[word] = translate;
                txtWord.Clear();
                txtTranslate.Clear();
                UpdateList();
            }
        }

        private void UpdateList()
        {
            listBox1.Items.Clear();

            foreach(var pair in slovnick)
            {
                listBox1.Items.Add($"{pair.Key} - {pair.Value}"); // big-O
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                // asdasd - dasdasda
                // after Split : ["asdasd ", " dasdasda"]

                string word = listBox1.SelectedItem.ToString().Split('-')[0].Trim();
                //string translate = listBox1.SelectedItem.ToString().Split('-')[1].Trim();

                slovnick.Remove(word);
                UpdateList();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchSlovo = txtSearch.Text.ToLower().Trim();

            if (searchSlovo != "")
            {
                listBox1.Items.Clear();

                foreach (var pair in slovnick)
                {
                    if (pair.Key.ToLower().StartsWith(searchSlovo))
                    {
                        listBox1.Items.Add($"{pair.Key} - {pair.Value}");
                    }
                }

                if (listBox1.Items.Count == 0)
                {
                    MessageBox.Show("404");
                    UpdateList();
                }
            }

            else
            {
                UpdateList();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                foreach(var pair in slovnick)
                {
                    sw.WriteLine($"{pair.Key}: {pair.Value}");
                }
            }
            MessageBox.Show("Файл успішно збережено");
        }
    }
}
