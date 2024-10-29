namespace ConTime.Screens
{
    partial class BPat
    {
        /// <summary> 
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Designer de Componentes

        /// <summary> 
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            tb_Header = new TextBox();
            panel1 = new Panel();
            btn_ImpBpat = new Button();
            btn_salvar_bpat = new Button();
            button1 = new Button();
            splitContainer1 = new SplitContainer();
            splitContainer3 = new SplitContainer();
            splitContainer4 = new SplitContainer();
            label2 = new Label();
            panel7 = new Panel();
            AtvCirculante = new DataGridView();
            panel2 = new Panel();
            label4 = new Label();
            splitContainer6 = new SplitContainer();
            panel10 = new Panel();
            AtvNCirculante = new DataGridView();
            panel6 = new Panel();
            label5 = new Label();
            lbl_TAtivos = new Label();
            label9 = new Label();
            splitContainer2 = new SplitContainer();
            splitContainer5 = new SplitContainer();
            label1 = new Label();
            panel8 = new Panel();
            PsvCirculante = new DataGridView();
            panel3 = new Panel();
            label3 = new Label();
            splitContainer7 = new SplitContainer();
            panel9 = new Panel();
            PsvNCirculante = new DataGridView();
            panel4 = new Panel();
            label6 = new Label();
            splitContainer8 = new SplitContainer();
            panel11 = new Panel();
            Patrimonio = new DataGridView();
            panel5 = new Panel();
            label7 = new Label();
            lbl_TPassivos = new Label();
            label8 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer3).BeginInit();
            splitContainer3.Panel1.SuspendLayout();
            splitContainer3.Panel2.SuspendLayout();
            splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer4).BeginInit();
            splitContainer4.Panel1.SuspendLayout();
            splitContainer4.Panel2.SuspendLayout();
            splitContainer4.SuspendLayout();
            panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AtvCirculante).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer6).BeginInit();
            splitContainer6.Panel1.SuspendLayout();
            splitContainer6.Panel2.SuspendLayout();
            splitContainer6.SuspendLayout();
            panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)AtvNCirculante).BeginInit();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer5).BeginInit();
            splitContainer5.Panel1.SuspendLayout();
            splitContainer5.Panel2.SuspendLayout();
            splitContainer5.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PsvCirculante).BeginInit();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer7).BeginInit();
            splitContainer7.Panel1.SuspendLayout();
            splitContainer7.Panel2.SuspendLayout();
            splitContainer7.SuspendLayout();
            panel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PsvNCirculante).BeginInit();
            panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer8).BeginInit();
            splitContainer8.Panel1.SuspendLayout();
            splitContainer8.Panel2.SuspendLayout();
            splitContainer8.SuspendLayout();
            panel11.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Patrimonio).BeginInit();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // tb_Header
            // 
            tb_Header.Dock = DockStyle.Fill;
            tb_Header.Location = new Point(0, 0);
            tb_Header.Margin = new Padding(3, 2, 3, 2);
            tb_Header.Name = "tb_Header";
            tb_Header.PlaceholderText = "Cabeçalho";
            tb_Header.Size = new Size(755, 23);
            tb_Header.TabIndex = 0;
            tb_Header.TextAlign = HorizontalAlignment.Center;
            // 
            // panel1
            // 
            panel1.Controls.Add(btn_ImpBpat);
            panel1.Controls.Add(btn_salvar_bpat);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(tb_Header);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(755, 26);
            panel1.TabIndex = 1;
            // 
            // btn_ImpBpat
            // 
            btn_ImpBpat.Location = new Point(571, 0);
            btn_ImpBpat.Name = "btn_ImpBpat";
            btn_ImpBpat.Size = new Size(75, 23);
            btn_ImpBpat.TabIndex = 4;
            btn_ImpBpat.Text = "Importar";
            btn_ImpBpat.UseVisualStyleBackColor = true;
            btn_ImpBpat.Click += btn_ImpBpat_Click;
            // 
            // btn_salvar_bpat
            // 
            btn_salvar_bpat.Location = new Point(652, 0);
            btn_salvar_bpat.Name = "btn_salvar_bpat";
            btn_salvar_bpat.Size = new Size(75, 23);
            btn_salvar_bpat.TabIndex = 3;
            btn_salvar_bpat.Text = "Salvar";
            btn_salvar_bpat.UseVisualStyleBackColor = true;
            btn_salvar_bpat.Click += btn_salvar_bpat_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            button1.Image = Properties.Resources.importar;
            button1.Location = new Point(724, 0);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(31, 26);
            button1.TabIndex = 1;
            button1.UseVisualStyleBackColor = true;
            button1.Click += PdfCreate;
            // 
            // splitContainer1
            // 
            splitContainer1.BackColor = Color.Transparent;
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 26);
            splitContainer1.Margin = new Padding(3, 2, 3, 2);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer3);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(755, 382);
            splitContainer1.SplitterDistance = 371;
            splitContainer1.TabIndex = 2;
            // 
            // splitContainer3
            // 
            splitContainer3.Dock = DockStyle.Fill;
            splitContainer3.Location = new Point(0, 0);
            splitContainer3.Margin = new Padding(3, 2, 3, 2);
            splitContainer3.Name = "splitContainer3";
            splitContainer3.Orientation = Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            splitContainer3.Panel1.Controls.Add(splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            splitContainer3.Panel2.Controls.Add(splitContainer6);
            splitContainer3.Size = new Size(371, 382);
            splitContainer3.SplitterDistance = 204;
            splitContainer3.SplitterWidth = 3;
            splitContainer3.TabIndex = 0;
            // 
            // splitContainer4
            // 
            splitContainer4.Dock = DockStyle.Fill;
            splitContainer4.Location = new Point(0, 0);
            splitContainer4.Margin = new Padding(3, 2, 3, 2);
            splitContainer4.Name = "splitContainer4";
            splitContainer4.Orientation = Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            splitContainer4.Panel1.BackColor = Color.FromArgb(54, 169, 63);
            splitContainer4.Panel1.Controls.Add(label2);
            // 
            // splitContainer4.Panel2
            // 
            splitContainer4.Panel2.BackColor = Color.Transparent;
            splitContainer4.Panel2.Controls.Add(panel7);
            splitContainer4.Panel2.Controls.Add(panel2);
            splitContainer4.Size = new Size(371, 204);
            splitContainer4.SplitterDistance = 25;
            splitContainer4.SplitterWidth = 3;
            splitContainer4.TabIndex = 0;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Segoe UI", 12F);
            label2.ForeColor = Color.WhiteSmoke;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(371, 25);
            label2.TabIndex = 1;
            label2.Text = "Ativo";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel7
            // 
            panel7.Controls.Add(AtvCirculante);
            panel7.Dock = DockStyle.Fill;
            panel7.Location = new Point(0, 24);
            panel7.Margin = new Padding(3, 2, 3, 2);
            panel7.Name = "panel7";
            panel7.Size = new Size(371, 152);
            panel7.TabIndex = 5;
            // 
            // AtvCirculante
            // 
            AtvCirculante.BackgroundColor = Color.FromArgb(232, 220, 232);
            AtvCirculante.BorderStyle = BorderStyle.None;
            AtvCirculante.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AtvCirculante.Dock = DockStyle.Fill;
            AtvCirculante.EditMode = DataGridViewEditMode.EditOnEnter;
            AtvCirculante.Location = new Point(0, 0);
            AtvCirculante.Margin = new Padding(3, 2, 3, 2);
            AtvCirculante.Name = "AtvCirculante";
            AtvCirculante.RowHeadersVisible = false;
            AtvCirculante.RowHeadersWidth = 51;
            AtvCirculante.Size = new Size(371, 152);
            AtvCirculante.TabIndex = 0;
            AtvCirculante.CellContentClick += AtvCirculante_CellContentClick;
            AtvCirculante.CellEndEdit += ContentUpdate;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(220, 232, 220);
            panel2.Controls.Add(label4);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(371, 24);
            panel2.TabIndex = 4;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.ForeColor = Color.FromArgb(54, 169, 63);
            label4.Location = new Point(0, 0);
            label4.Name = "label4";
            label4.Size = new Size(371, 24);
            label4.TabIndex = 3;
            label4.Text = "Ativo Circulante";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // splitContainer6
            // 
            splitContainer6.Dock = DockStyle.Fill;
            splitContainer6.Location = new Point(0, 0);
            splitContainer6.Margin = new Padding(3, 2, 3, 2);
            splitContainer6.Name = "splitContainer6";
            splitContainer6.Orientation = Orientation.Horizontal;
            // 
            // splitContainer6.Panel1
            // 
            splitContainer6.Panel1.BackColor = Color.WhiteSmoke;
            splitContainer6.Panel1.Controls.Add(panel10);
            splitContainer6.Panel1.Controls.Add(panel6);
            // 
            // splitContainer6.Panel2
            // 
            splitContainer6.Panel2.BackColor = Color.FromArgb(220, 232, 220);
            splitContainer6.Panel2.Controls.Add(lbl_TAtivos);
            splitContainer6.Panel2.Controls.Add(label9);
            splitContainer6.Size = new Size(371, 175);
            splitContainer6.SplitterDistance = 145;
            splitContainer6.SplitterWidth = 3;
            splitContainer6.TabIndex = 2;
            // 
            // panel10
            // 
            panel10.Controls.Add(AtvNCirculante);
            panel10.Dock = DockStyle.Fill;
            panel10.Location = new Point(0, 24);
            panel10.Margin = new Padding(3, 2, 3, 2);
            panel10.Name = "panel10";
            panel10.Size = new Size(371, 121);
            panel10.TabIndex = 6;
            // 
            // AtvNCirculante
            // 
            AtvNCirculante.BackgroundColor = Color.FromArgb(232, 220, 232);
            AtvNCirculante.BorderStyle = BorderStyle.None;
            AtvNCirculante.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            AtvNCirculante.Dock = DockStyle.Fill;
            AtvNCirculante.EditMode = DataGridViewEditMode.EditOnEnter;
            AtvNCirculante.Location = new Point(0, 0);
            AtvNCirculante.Margin = new Padding(3, 2, 3, 2);
            AtvNCirculante.Name = "AtvNCirculante";
            AtvNCirculante.RowHeadersVisible = false;
            AtvNCirculante.RowHeadersWidth = 51;
            AtvNCirculante.Size = new Size(371, 121);
            AtvNCirculante.TabIndex = 1;
            AtvNCirculante.CellEndEdit += ContentUpdate;
            // 
            // panel6
            // 
            panel6.BackColor = Color.FromArgb(220, 232, 220);
            panel6.Controls.Add(label5);
            panel6.Dock = DockStyle.Top;
            panel6.Location = new Point(0, 0);
            panel6.Margin = new Padding(3, 2, 3, 2);
            panel6.Name = "panel6";
            panel6.Size = new Size(371, 24);
            panel6.TabIndex = 5;
            // 
            // label5
            // 
            label5.Dock = DockStyle.Fill;
            label5.ForeColor = Color.FromArgb(54, 169, 63);
            label5.Location = new Point(0, 0);
            label5.Name = "label5";
            label5.Size = new Size(371, 24);
            label5.TabIndex = 4;
            label5.Text = "Ativo não Circulante";
            label5.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbl_TAtivos
            // 
            lbl_TAtivos.Dock = DockStyle.Fill;
            lbl_TAtivos.ForeColor = Color.FromArgb(54, 169, 63);
            lbl_TAtivos.Location = new Point(76, 0);
            lbl_TAtivos.Name = "lbl_TAtivos";
            lbl_TAtivos.Size = new Size(295, 27);
            lbl_TAtivos.TabIndex = 9;
            lbl_TAtivos.Text = "R$0,00";
            lbl_TAtivos.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.Dock = DockStyle.Left;
            label9.ForeColor = Color.FromArgb(54, 169, 63);
            label9.Location = new Point(0, 0);
            label9.Name = "label9";
            label9.Size = new Size(76, 27);
            label9.TabIndex = 8;
            label9.Text = "Total Ativos";
            label9.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Margin = new Padding(3, 2, 3, 2);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(splitContainer5);
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(splitContainer7);
            splitContainer2.Size = new Size(380, 382);
            splitContainer2.SplitterDistance = 135;
            splitContainer2.SplitterWidth = 3;
            splitContainer2.TabIndex = 0;
            // 
            // splitContainer5
            // 
            splitContainer5.Dock = DockStyle.Fill;
            splitContainer5.Location = new Point(0, 0);
            splitContainer5.Margin = new Padding(3, 2, 3, 2);
            splitContainer5.Name = "splitContainer5";
            splitContainer5.Orientation = Orientation.Horizontal;
            // 
            // splitContainer5.Panel1
            // 
            splitContainer5.Panel1.BackColor = Color.FromArgb(54, 169, 63);
            splitContainer5.Panel1.Controls.Add(label1);
            // 
            // splitContainer5.Panel2
            // 
            splitContainer5.Panel2.BackColor = Color.WhiteSmoke;
            splitContainer5.Panel2.Controls.Add(panel8);
            splitContainer5.Panel2.Controls.Add(panel3);
            splitContainer5.Size = new Size(380, 135);
            splitContainer5.SplitterDistance = 25;
            splitContainer5.SplitterWidth = 3;
            splitContainer5.TabIndex = 1;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Font = new Font("Segoe UI", 12F);
            label1.ForeColor = Color.WhiteSmoke;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(380, 25);
            label1.TabIndex = 0;
            label1.Text = "Passivo";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel8
            // 
            panel8.Controls.Add(PsvCirculante);
            panel8.Dock = DockStyle.Fill;
            panel8.Location = new Point(0, 24);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(380, 83);
            panel8.TabIndex = 6;
            // 
            // PsvCirculante
            // 
            PsvCirculante.BackgroundColor = Color.FromArgb(232, 220, 232);
            PsvCirculante.BorderStyle = BorderStyle.None;
            PsvCirculante.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PsvCirculante.Dock = DockStyle.Fill;
            PsvCirculante.EditMode = DataGridViewEditMode.EditOnEnter;
            PsvCirculante.Location = new Point(0, 0);
            PsvCirculante.Margin = new Padding(3, 2, 3, 2);
            PsvCirculante.Name = "PsvCirculante";
            PsvCirculante.RowHeadersVisible = false;
            PsvCirculante.RowHeadersWidth = 51;
            PsvCirculante.Size = new Size(380, 83);
            PsvCirculante.TabIndex = 1;
            PsvCirculante.CellEndEdit += ContentUpdate;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(220, 232, 220);
            panel3.Controls.Add(label3);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(380, 24);
            panel3.TabIndex = 5;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.ForeColor = Color.FromArgb(54, 169, 63);
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(380, 24);
            label3.TabIndex = 2;
            label3.Text = "Passivo Circulante";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // splitContainer7
            // 
            splitContainer7.Dock = DockStyle.Fill;
            splitContainer7.Location = new Point(0, 0);
            splitContainer7.Margin = new Padding(3, 2, 3, 2);
            splitContainer7.Name = "splitContainer7";
            splitContainer7.Orientation = Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            splitContainer7.Panel1.BackColor = Color.WhiteSmoke;
            splitContainer7.Panel1.Controls.Add(panel9);
            splitContainer7.Panel1.Controls.Add(panel4);
            // 
            // splitContainer7.Panel2
            // 
            splitContainer7.Panel2.Controls.Add(splitContainer8);
            splitContainer7.Size = new Size(380, 244);
            splitContainer7.SplitterDistance = 112;
            splitContainer7.SplitterWidth = 3;
            splitContainer7.TabIndex = 2;
            // 
            // panel9
            // 
            panel9.Controls.Add(PsvNCirculante);
            panel9.Dock = DockStyle.Fill;
            panel9.Location = new Point(0, 24);
            panel9.Margin = new Padding(3, 2, 3, 2);
            panel9.Name = "panel9";
            panel9.Size = new Size(380, 88);
            panel9.TabIndex = 7;
            // 
            // PsvNCirculante
            // 
            PsvNCirculante.BackgroundColor = Color.FromArgb(232, 220, 232);
            PsvNCirculante.BorderStyle = BorderStyle.None;
            PsvNCirculante.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            PsvNCirculante.Dock = DockStyle.Fill;
            PsvNCirculante.EditMode = DataGridViewEditMode.EditOnEnter;
            PsvNCirculante.Location = new Point(0, 0);
            PsvNCirculante.Margin = new Padding(3, 2, 3, 2);
            PsvNCirculante.Name = "PsvNCirculante";
            PsvNCirculante.RowHeadersVisible = false;
            PsvNCirculante.RowHeadersWidth = 51;
            PsvNCirculante.Size = new Size(380, 88);
            PsvNCirculante.TabIndex = 1;
            PsvNCirculante.CellEndEdit += ContentUpdate;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(220, 232, 220);
            panel4.Controls.Add(label6);
            panel4.Dock = DockStyle.Top;
            panel4.Location = new Point(0, 0);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(380, 24);
            panel4.TabIndex = 6;
            // 
            // label6
            // 
            label6.Dock = DockStyle.Fill;
            label6.ForeColor = Color.FromArgb(54, 169, 63);
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(380, 24);
            label6.TabIndex = 5;
            label6.Text = "Passivo não Circulante";
            label6.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // splitContainer8
            // 
            splitContainer8.Dock = DockStyle.Fill;
            splitContainer8.Location = new Point(0, 0);
            splitContainer8.Margin = new Padding(3, 2, 3, 2);
            splitContainer8.Name = "splitContainer8";
            splitContainer8.Orientation = Orientation.Horizontal;
            // 
            // splitContainer8.Panel1
            // 
            splitContainer8.Panel1.BackColor = Color.WhiteSmoke;
            splitContainer8.Panel1.Controls.Add(panel11);
            splitContainer8.Panel1.Controls.Add(panel5);
            // 
            // splitContainer8.Panel2
            // 
            splitContainer8.Panel2.BackColor = Color.FromArgb(220, 232, 220);
            splitContainer8.Panel2.Controls.Add(lbl_TPassivos);
            splitContainer8.Panel2.Controls.Add(label8);
            splitContainer8.Size = new Size(380, 129);
            splitContainer8.SplitterDistance = 99;
            splitContainer8.SplitterWidth = 3;
            splitContainer8.TabIndex = 2;
            // 
            // panel11
            // 
            panel11.Controls.Add(Patrimonio);
            panel11.Dock = DockStyle.Fill;
            panel11.Location = new Point(0, 24);
            panel11.Margin = new Padding(3, 2, 3, 2);
            panel11.Name = "panel11";
            panel11.Size = new Size(380, 75);
            panel11.TabIndex = 8;
            // 
            // Patrimonio
            // 
            Patrimonio.BackgroundColor = Color.FromArgb(232, 220, 232);
            Patrimonio.BorderStyle = BorderStyle.None;
            Patrimonio.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Patrimonio.Dock = DockStyle.Fill;
            Patrimonio.EditMode = DataGridViewEditMode.EditOnEnter;
            Patrimonio.Location = new Point(0, 0);
            Patrimonio.Margin = new Padding(3, 2, 3, 2);
            Patrimonio.Name = "Patrimonio";
            Patrimonio.RowHeadersVisible = false;
            Patrimonio.RowHeadersWidth = 51;
            Patrimonio.Size = new Size(380, 75);
            Patrimonio.TabIndex = 1;
            Patrimonio.CellEndEdit += ContentUpdate;
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(220, 232, 220);
            panel5.Controls.Add(label7);
            panel5.Dock = DockStyle.Top;
            panel5.Location = new Point(0, 0);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(380, 24);
            panel5.TabIndex = 7;
            // 
            // label7
            // 
            label7.Dock = DockStyle.Fill;
            label7.ForeColor = Color.FromArgb(54, 169, 63);
            label7.Location = new Point(0, 0);
            label7.Name = "label7";
            label7.Size = new Size(380, 24);
            label7.TabIndex = 6;
            label7.Text = "Patrimônio Líquido";
            label7.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lbl_TPassivos
            // 
            lbl_TPassivos.Dock = DockStyle.Fill;
            lbl_TPassivos.ForeColor = Color.FromArgb(54, 169, 63);
            lbl_TPassivos.Location = new Point(81, 0);
            lbl_TPassivos.Name = "lbl_TPassivos";
            lbl_TPassivos.Size = new Size(299, 27);
            lbl_TPassivos.TabIndex = 9;
            lbl_TPassivos.Text = "R$0,00";
            lbl_TPassivos.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label8
            // 
            label8.Dock = DockStyle.Left;
            label8.ForeColor = Color.FromArgb(54, 169, 63);
            label8.Location = new Point(0, 0);
            label8.Name = "label8";
            label8.Size = new Size(81, 27);
            label8.TabIndex = 7;
            label8.Text = "Total Passivo";
            label8.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // BPat
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splitContainer1);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "BPat";
            Size = new Size(755, 408);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer3.Panel1.ResumeLayout(false);
            splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer3).EndInit();
            splitContainer3.ResumeLayout(false);
            splitContainer4.Panel1.ResumeLayout(false);
            splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer4).EndInit();
            splitContainer4.ResumeLayout(false);
            panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)AtvCirculante).EndInit();
            panel2.ResumeLayout(false);
            splitContainer6.Panel1.ResumeLayout(false);
            splitContainer6.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer6).EndInit();
            splitContainer6.ResumeLayout(false);
            panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)AtvNCirculante).EndInit();
            panel6.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            splitContainer5.Panel1.ResumeLayout(false);
            splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer5).EndInit();
            splitContainer5.ResumeLayout(false);
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PsvCirculante).EndInit();
            panel3.ResumeLayout(false);
            splitContainer7.Panel1.ResumeLayout(false);
            splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer7).EndInit();
            splitContainer7.ResumeLayout(false);
            panel9.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PsvNCirculante).EndInit();
            panel4.ResumeLayout(false);
            splitContainer8.Panel1.ResumeLayout(false);
            splitContainer8.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer8).EndInit();
            splitContainer8.ResumeLayout(false);
            panel11.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Patrimonio).EndInit();
            panel5.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TextBox tb_Header;
        private Panel panel1;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer3;
        private SplitContainer splitContainer2;
        private SplitContainer splitContainer4;
        private Label label2;
        private SplitContainer splitContainer6;
        private SplitContainer splitContainer5;
        private Label label1;
        private SplitContainer splitContainer7;
        private SplitContainer splitContainer8;
        private Label label4;
        private Label label5;
        private Label label3;
        private Label label6;
        private Label label7;
        private Label label9;
        private Label label8;
        private Panel panel2;
        private Panel panel6;
        private Panel panel3;
        private Panel panel4;
        private Panel panel5;
        private Button button1;
        private Panel panel7;
        private DataGridView AtvCirculante;
        private Panel panel10;
        private DataGridView AtvNCirculante;
        private Panel panel8;
        private DataGridView PsvCirculante;
        private Panel panel9;
        private DataGridView PsvNCirculante;
        private Panel panel11;
        private DataGridView Patrimonio;
        private Label lbl_TAtivos;
        private Label lbl_TPassivos;
        private Button btn_salvar_bpat;
        private Button btn_ImpBpat;
    }
}
