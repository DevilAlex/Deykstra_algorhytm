using System;
using System.Drawing;
using System.Windows.Forms;

namespace DeykstraAlgorhitm
{
    public partial class Form1 : Form
    {
        const int INF = Int32.MaxValue;
        int Nodes;//Количество вершин
        int FirstNode;
        int [] D; //массив расстояний
        bool[] V; //Массив посещенных вершин
        int[,] A; //Матрица расстояний
        public Form1()
        {
            InitializeComponent();
            Grid.AllowUserToAddRows = false;
            Grid.EnableHeadersVisualStyles = false;
            Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Grid.AutoSize = true;
            Grid.BorderStyle = BorderStyle.None;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            int col = int.Parse(comboBox1.SelectedItem.ToString());
            for (int i = 1; i <= col; i++)
            {
                comboBox3.Items.Add(i);
                comboBox2.Items.Add(i);
            }
            Nodes = int.Parse((string)comboBox1.SelectedItem);
            D = new int[Nodes];
            V = new bool[Nodes];
            A = new int[Nodes, Nodes];
            for (int i = 0; i < Nodes; i++)
            {
                D[i] = INF;
                V[i] = false;
                Grid.Columns.Add((i+1).ToString(), (i+1).ToString());
                Grid.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                Grid.Columns[i].HeaderCell.Style.BackColor = Color.LightGreen;
                Grid.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grid.Columns[i].HeaderCell.Style.Font = new Font(Grid.Font, FontStyle.Bold);
            }
            Grid.Rows.Add(Nodes);
            for (int i = 0; i < Nodes; i++)
            {
                Grid.Rows[i].HeaderCell.Value = string.Format((i + 1).ToString(), "0");
                Grid.Rows[i].HeaderCell.Style.BackColor = Color.LightGreen;
                Grid.Rows[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                Grid.Rows[i].HeaderCell.Style.Font = new Font(Grid.Font, FontStyle.Bold);
                for (int j = 0; j < Nodes; j++)
                    Grid.Rows[i].Cells[j].Value = 0;
            }
            Grid.Visible = true;
        }
        private void StartButton_Click(object sender, EventArgs e) //Определение минимальных весов вершин
        {
            FirstNode = int.Parse(comboBox2.SelectedItem.ToString()) - 1;
            D[FirstNode] = 0;
            for (int i = 0; i < Nodes; i++)
                for (int j = 0; j < Nodes; j++)
                    A[i, j] = (int)Grid.Rows[i].Cells[j].Value;
            int TekIndex;
            while ((TekIndex = Minimum(D, V)) != -1)
            {
                for (int i = 0; i < Nodes; i++)
                    if (A[TekIndex, i] != 0 && V[i] == false) //Есть связь между вершинами и вершина не помечена
                        D[i] = (D[TekIndex] + A[TekIndex, i] < D[i] ? D[TekIndex] + A[TekIndex, i] : D[i]);
                V[TekIndex] = true;
            };
            textBox2.Clear();
            for (int i = 0; i < Nodes; i++)
                textBox2.Text += (i+1).ToString() + " - " + D[i] + ", ";
        }
        
        private void PathButton_Click(object sender, EventArgs e) //Кратчайший путь между вершинами
        {
            textBox1.Clear();
            int source = int.Parse(comboBox3.SelectedItem.ToString()) - 1;
            int PathLength = 0; //Длина пути
            textBox1.Text += (source + 1).ToString();
            while (D[source] > 0)
            {
                bool flag = true;
                for (int i = 0; i < Nodes && flag == true; i++)
                    if (A[source, i] != 0 && D[source] - A[source, i] == D[i])
                    {
                        textBox1.Text = (i + 1).ToString() + " - " + textBox1.Text;
                        PathLength += D[source];
                        flag = false;
                        source = i;
                    }
            };
            textBox1.Text += " path_length = " + PathLength;
        }
        private int Minimum(int[] d, bool[] v) //Поиск минимальной вершины по весу из числа непосещенных
        {
            int MinIndex = -1;
            int min = INF;
            for (int i = 0; i < d.Length; i++)
                if (v[i] == false && d[i] < min)
                {
                    min = d[i];
                    MinIndex = i;
                }
            return MinIndex;
        }

        private void ClearButton_Click(object sender, EventArgs e)
        {
            Grid.Rows.Clear();
            Grid.Columns.Clear();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            textBox1.Clear();
            textBox2.Clear();
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            OkButton.Enabled = false;
            StartButton.Enabled = false;
            button3.Enabled = false;
            Grid.Visible = false;
            this.Width = 520;
            this.Height = 290;
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) => OkButton.Enabled = true;
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) => StartButton.Enabled = true;
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) => button3.Enabled = true;
    }
}
