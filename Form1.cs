using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Desculpas_de_Bryan
{
    public partial class Form1 : Form
    {
        private string pasta;
        private string caminhoEscolhido;
        private string nomeDoArquivo;
        private string[] LinhasDoArquivo;
        private Random random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private void Pasta_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.SelectedPath = caminhoEscolhido;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                pasta = folderBrowserDialog1.SelectedPath;
                caminhoEscolhido = folderBrowserDialog1.SelectedPath;
                Salvar.Enabled = true;
                Abrir.Enabled = true;
                Aleatorio.Enabled = true;
            }
        }

        private void Salvar_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Salvar";
            saveFileDialog1.Filter = "Text File (*.txt)|*.txt";
            saveFileDialog1.InitialDirectory = caminhoEscolhido;
            saveFileDialog1.FileName = textBox1.Text + ".txt";

            if (textBox1.Text != "" || MessageBox.Show("A desculpa esta vazia, você deseja continuar?", "Aviso", MessageBoxButtons.YesNo)==DialogResult.Yes)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Form1.ActiveForm.Text = "Gerenciador de desculpas";
                    nomeDoArquivo = saveFileDialog1.FileName;
                    File.WriteAllText(nomeDoArquivo, textBox1.Text + "\n" + textBox2.Text + "\n" + dateTimePicker1.Text.ToString());
                }
            }
        }

        private void Abrir_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "Abrir";
            saveFileDialog1.InitialDirectory = caminhoEscolhido;
            openFileDialog1.Filter = "Text File (*.txt)|*.txt";

            if (Form1.ActiveForm.Text == "Gerenciador de desculpas" || MessageBox.Show("A desculpa não foi salva, você deseja continuar?", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    caminhoEscolhido = openFileDialog1.FileName;
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();

                    LinhasDoArquivo = File.ReadAllLines(caminhoEscolhido);
                    textBox1.Text = LinhasDoArquivo[0];
                    textBox2.Text = LinhasDoArquivo[1];
                    textBox3.Text = LinhasDoArquivo[2];

                    Form1.ActiveForm.Text = "Gerenciador de desculpas";
                }
            }
        }

        private void Aleatorio_Click(object sender, EventArgs e)
        {
            if (Form1.ActiveForm.Text == "Gerenciador de desculpas" || MessageBox.Show("A desculpa não foi salva, você deseja continuar?", "Aviso", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    var arquivos = Directory.GetFiles(pasta, "*.txt");
                    var arquivoAleatorio = arquivos[random.Next(0, arquivos.Length)];

                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();

                    LinhasDoArquivo = File.ReadAllLines(arquivoAleatorio);
                    textBox1.Text = LinhasDoArquivo[0];
                    textBox2.Text = LinhasDoArquivo[1];
                    textBox3.Text = LinhasDoArquivo[2];

                    Form1.ActiveForm.Text = "Gerenciador de desculpas";
                }catch(Exception erro)
                {
                    MessageBox.Show("Houve o erro " + erro, "Erro");
                }

            }
        }

        private void TextoMudado(object sender, EventArgs e)
        {
            Form1.ActiveForm.Text = "Gerenciador de desculpas*";
        }
    }
}
