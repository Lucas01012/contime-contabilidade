namespace ConTime.Screens
{
    partial class Dre
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
            panel1 = new Panel();
            btn_salvar_dre = new Button();
            button7 = new Button();
            button6 = new Button();
            panel10 = new Panel();
            r7 = new Label();
            label8 = new Label();
            panel8 = new Panel();
            ROutras = new DataGridView();
            panel15 = new Panel();
            r6 = new Label();
            label7 = new Label();
            button5 = new Button();
            panel7 = new Panel();
            r4 = new Label();
            label6 = new Label();
            panel6 = new Panel();
            Despesas = new DataGridView();
            panel14 = new Panel();
            r5 = new Label();
            label5 = new Label();
            button4 = new Button();
            panel5 = new Panel();
            Custos = new DataGridView();
            panel13 = new Panel();
            r3 = new Label();
            label4 = new Label();
            button3 = new Button();
            panel4 = new Panel();
            r2 = new Label();
            label3 = new Label();
            panel3 = new Panel();
            Imposto = new DataGridView();
            panel12 = new Panel();
            r1 = new Label();
            label2 = new Label();
            button2 = new Button();
            panel2 = new Panel();
            RBruta = new DataGridView();
            panel11 = new Panel();
            r0 = new Label();
            label1 = new Label();
            button1 = new Button();
            panel1.SuspendLayout();
            panel10.SuspendLayout();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ROutras).BeginInit();
            panel15.SuspendLayout();
            panel7.SuspendLayout();
            panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Despesas).BeginInit();
            panel14.SuspendLayout();
            panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Custos).BeginInit();
            panel13.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Imposto).BeginInit();
            panel12.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)RBruta).BeginInit();
            panel11.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.AutoScroll = true;
            panel1.BackColor = Color.Transparent;
            panel1.Controls.Add(btn_salvar_dre);
            panel1.Controls.Add(button7);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(panel10);
            panel1.Controls.Add(panel8);
            panel1.Controls.Add(panel7);
            panel1.Controls.Add(panel6);
            panel1.Controls.Add(panel5);
            panel1.Controls.Add(panel4);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(panel2);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(697, 464);
            panel1.TabIndex = 1;
            // 
            // btn_salvar_dre
            // 
            btn_salvar_dre.Location = new Point(587, 101);
            btn_salvar_dre.Margin = new Padding(3, 4, 3, 4);
            btn_salvar_dre.Name = "btn_salvar_dre";
            btn_salvar_dre.Size = new Size(75, 29);
            btn_salvar_dre.TabIndex = 2;
            btn_salvar_dre.Text = "Salvar";
            btn_salvar_dre.UseVisualStyleBackColor = true;
            btn_salvar_dre.Click += btn_salvar_dre_Click;
            // 
            // button7
            // 
            button7.Location = new Point(587, 61);
            button7.Margin = new Padding(3, 4, 3, 4);
            button7.Name = "button7";
            button7.Size = new Size(75, 29);
            button7.TabIndex = 9;
            button7.Text = "PDF";
            button7.UseVisualStyleBackColor = true;
            button7.Click += CreatePdf;
            // 
            // button6
            // 
            button6.Location = new Point(587, 25);
            button6.Margin = new Padding(3, 4, 3, 4);
            button6.Name = "button6";
            button6.Size = new Size(75, 29);
            button6.TabIndex = 8;
            button6.Text = "Apagar";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // panel10
            // 
            panel10.BackColor = Color.FromArgb(220, 232, 220);
            panel10.Controls.Add(r7);
            panel10.Controls.Add(label8);
            panel10.Location = new Point(5, 408);
            panel10.Margin = new Padding(3, 4, 3, 4);
            panel10.Name = "panel10";
            panel10.Size = new Size(501, 51);
            panel10.TabIndex = 7;
            // 
            // r7
            // 
            r7.AutoSize = true;
            r7.Location = new Point(357, 15);
            r7.Name = "r7";
            r7.Size = new Size(26, 20);
            r7.TabIndex = 8;
            r7.Tag = "Resultado";
            r7.Text = "R$";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(45, 15);
            label8.Name = "label8";
            label8.Size = new Size(241, 20);
            label8.TabIndex = 8;
            label8.Text = "(=)Resultado de Lucro do Exercicio";
            // 
            // panel8
            // 
            panel8.BackColor = Color.ForestGreen;
            panel8.Controls.Add(ROutras);
            panel8.Controls.Add(panel15);
            panel8.Location = new Point(5, 349);
            panel8.Margin = new Padding(3, 4, 3, 4);
            panel8.Name = "panel8";
            panel8.Size = new Size(501, 51);
            panel8.TabIndex = 6;
            // 
            // ROutras
            // 
            ROutras.BackgroundColor = Color.FromArgb(54, 169, 63);
            ROutras.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            ROutras.Dock = DockStyle.Fill;
            ROutras.EditMode = DataGridViewEditMode.EditOnEnter;
            ROutras.Location = new Point(0, 40);
            ROutras.Margin = new Padding(3, 4, 3, 4);
            ROutras.Name = "ROutras";
            ROutras.RowHeadersVisible = false;
            ROutras.RowHeadersWidth = 51;
            ROutras.RowTemplate.Height = 24;
            ROutras.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            ROutras.Size = new Size(501, 11);
            ROutras.TabIndex = 5;
            ROutras.Visible = false;
            ROutras.CellEndEdit += ContentUpdate;
            // 
            // panel15
            // 
            panel15.BackColor = Color.FromArgb(220, 232, 220);
            panel15.Controls.Add(r6);
            panel15.Controls.Add(label7);
            panel15.Controls.Add(button5);
            panel15.Dock = DockStyle.Top;
            panel15.Location = new Point(0, 0);
            panel15.Margin = new Padding(3, 4, 3, 4);
            panel15.Name = "panel15";
            panel15.Size = new Size(501, 40);
            panel15.TabIndex = 4;
            // 
            // r6
            // 
            r6.AutoSize = true;
            r6.Location = new Point(357, 11);
            r6.Name = "r6";
            r6.Size = new Size(26, 20);
            r6.TabIndex = 7;
            r6.Tag = "Resultado";
            r6.Text = "R$";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(45, 11);
            label7.Name = "label7";
            label7.Size = new Size(131, 20);
            label7.TabIndex = 7;
            label7.Text = "(+)Outras Receitas";
            // 
            // button5
            // 
            button5.BackColor = Color.Transparent;
            button5.Dock = DockStyle.Left;
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Location = new Point(0, 0);
            button5.Margin = new Padding(3, 4, 3, 4);
            button5.Name = "button5";
            button5.Size = new Size(39, 40);
            button5.TabIndex = 4;
            button5.Text = "˅";
            button5.UseVisualStyleBackColor = false;
            button5.Click += btnDrop_Click;
            // 
            // panel7
            // 
            panel7.BackColor = Color.FromArgb(220, 232, 220);
            panel7.Controls.Add(r4);
            panel7.Controls.Add(label6);
            panel7.Location = new Point(5, 235);
            panel7.Margin = new Padding(3, 4, 3, 4);
            panel7.Name = "panel7";
            panel7.Size = new Size(501, 51);
            panel7.TabIndex = 4;
            // 
            // r4
            // 
            r4.AutoSize = true;
            r4.Location = new Point(357, 16);
            r4.Name = "r4";
            r4.Size = new Size(26, 20);
            r4.TabIndex = 5;
            r4.Tag = "Resultado";
            r4.Text = "R$";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(45, 16);
            label6.Name = "label6";
            label6.Size = new Size(190, 20);
            label6.TabIndex = 6;
            label6.Text = "(=)Lucro Operacional Bruto";
            // 
            // panel6
            // 
            panel6.BackColor = Color.ForestGreen;
            panel6.Controls.Add(Despesas);
            panel6.Controls.Add(panel14);
            panel6.Location = new Point(5, 292);
            panel6.Margin = new Padding(3, 4, 3, 4);
            panel6.Name = "panel6";
            panel6.Size = new Size(501, 51);
            panel6.TabIndex = 5;
            // 
            // Despesas
            // 
            Despesas.BackgroundColor = Color.FromArgb(54, 169, 63);
            Despesas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Despesas.Dock = DockStyle.Fill;
            Despesas.EditMode = DataGridViewEditMode.EditOnEnter;
            Despesas.Location = new Point(0, 40);
            Despesas.Margin = new Padding(3, 4, 3, 4);
            Despesas.Name = "Despesas";
            Despesas.RowHeadersVisible = false;
            Despesas.RowHeadersWidth = 51;
            Despesas.RowTemplate.Height = 24;
            Despesas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Despesas.Size = new Size(501, 11);
            Despesas.TabIndex = 4;
            Despesas.Visible = false;
            Despesas.CellEndEdit += ContentUpdate;
            // 
            // panel14
            // 
            panel14.BackColor = Color.FromArgb(220, 232, 220);
            panel14.Controls.Add(r5);
            panel14.Controls.Add(label5);
            panel14.Controls.Add(button4);
            panel14.Dock = DockStyle.Top;
            panel14.Location = new Point(0, 0);
            panel14.Margin = new Padding(3, 4, 3, 4);
            panel14.Name = "panel14";
            panel14.Size = new Size(501, 40);
            panel14.TabIndex = 3;
            // 
            // r5
            // 
            r5.AutoSize = true;
            r5.Location = new Point(357, 11);
            r5.Name = "r5";
            r5.Size = new Size(26, 20);
            r5.TabIndex = 6;
            r5.Tag = "Resultado";
            r5.Text = "R$";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(45, 11);
            label5.Name = "label5";
            label5.Size = new Size(87, 20);
            label5.TabIndex = 5;
            label5.Text = "(-)Despesas";
            // 
            // button4
            // 
            button4.BackColor = Color.Transparent;
            button4.Dock = DockStyle.Left;
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Location = new Point(0, 0);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(39, 40);
            button4.TabIndex = 3;
            button4.Text = "˅";
            button4.UseVisualStyleBackColor = false;
            button4.Click += btnDrop_Click;
            // 
            // panel5
            // 
            panel5.BackColor = Color.ForestGreen;
            panel5.Controls.Add(Custos);
            panel5.Controls.Add(panel13);
            panel5.Location = new Point(5, 179);
            panel5.Margin = new Padding(3, 4, 3, 4);
            panel5.Name = "panel5";
            panel5.Size = new Size(501, 51);
            panel5.TabIndex = 3;
            // 
            // Custos
            // 
            Custos.BackgroundColor = Color.FromArgb(54, 169, 63);
            Custos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Custos.Dock = DockStyle.Fill;
            Custos.EditMode = DataGridViewEditMode.EditOnEnter;
            Custos.Location = new Point(0, 40);
            Custos.Margin = new Padding(3, 4, 3, 4);
            Custos.Name = "Custos";
            Custos.RowHeadersVisible = false;
            Custos.RowHeadersWidth = 51;
            Custos.RowTemplate.Height = 24;
            Custos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Custos.Size = new Size(501, 11);
            Custos.TabIndex = 3;
            Custos.Visible = false;
            Custos.CellEndEdit += ContentUpdate;
            // 
            // panel13
            // 
            panel13.BackColor = Color.FromArgb(220, 232, 220);
            panel13.Controls.Add(r3);
            panel13.Controls.Add(label4);
            panel13.Controls.Add(button3);
            panel13.Dock = DockStyle.Top;
            panel13.Location = new Point(0, 0);
            panel13.Margin = new Padding(3, 4, 3, 4);
            panel13.Name = "panel13";
            panel13.Size = new Size(501, 40);
            panel13.TabIndex = 2;
            // 
            // r3
            // 
            r3.AutoSize = true;
            r3.Location = new Point(357, 11);
            r3.Name = "r3";
            r3.Size = new Size(26, 20);
            r3.TabIndex = 4;
            r3.Tag = "Resultado";
            r3.Text = "R$";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 11);
            label4.Name = "label4";
            label4.Size = new Size(68, 20);
            label4.TabIndex = 4;
            label4.Text = "(-)Custos";
            // 
            // button3
            // 
            button3.BackColor = Color.Transparent;
            button3.Dock = DockStyle.Left;
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Location = new Point(0, 0);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(39, 40);
            button3.TabIndex = 2;
            button3.Text = "˅";
            button3.UseVisualStyleBackColor = false;
            button3.Click += btnDrop_Click;
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(220, 232, 220);
            panel4.Controls.Add(r2);
            panel4.Controls.Add(label3);
            panel4.Location = new Point(5, 120);
            panel4.Margin = new Padding(3, 4, 3, 4);
            panel4.Name = "panel4";
            panel4.Size = new Size(501, 51);
            panel4.TabIndex = 2;
            // 
            // r2
            // 
            r2.AutoSize = true;
            r2.Location = new Point(357, 16);
            r2.Name = "r2";
            r2.Size = new Size(26, 20);
            r2.TabIndex = 3;
            r2.Tag = "Resultado";
            r2.Text = "R$";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(45, 16);
            label3.Name = "label3";
            label3.Size = new Size(131, 20);
            label3.TabIndex = 3;
            label3.Text = "(=)Receita Liquida";
            // 
            // panel3
            // 
            panel3.BackColor = Color.ForestGreen;
            panel3.Controls.Add(Imposto);
            panel3.Controls.Add(panel12);
            panel3.Location = new Point(5, 61);
            panel3.Margin = new Padding(3, 4, 3, 4);
            panel3.Name = "panel3";
            panel3.Size = new Size(501, 51);
            panel3.TabIndex = 1;
            // 
            // Imposto
            // 
            Imposto.BackgroundColor = Color.FromArgb(54, 169, 63);
            Imposto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            Imposto.Dock = DockStyle.Fill;
            Imposto.EditMode = DataGridViewEditMode.EditOnEnter;
            Imposto.Location = new Point(0, 40);
            Imposto.Margin = new Padding(3, 4, 3, 4);
            Imposto.Name = "Imposto";
            Imposto.RowHeadersVisible = false;
            Imposto.RowHeadersWidth = 51;
            Imposto.RowTemplate.Height = 24;
            Imposto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Imposto.Size = new Size(501, 11);
            Imposto.TabIndex = 2;
            Imposto.Visible = false;
            Imposto.CellEndEdit += ContentUpdate;
            // 
            // panel12
            // 
            panel12.BackColor = Color.FromArgb(220, 232, 220);
            panel12.Controls.Add(r1);
            panel12.Controls.Add(label2);
            panel12.Controls.Add(button2);
            panel12.Dock = DockStyle.Top;
            panel12.Location = new Point(0, 0);
            panel12.Margin = new Padding(3, 4, 3, 4);
            panel12.Name = "panel12";
            panel12.Size = new Size(501, 40);
            panel12.TabIndex = 1;
            // 
            // r1
            // 
            r1.AutoSize = true;
            r1.Location = new Point(357, 11);
            r1.Name = "r1";
            r1.Size = new Size(26, 20);
            r1.TabIndex = 2;
            r1.Tag = "Resultado";
            r1.Text = "R$";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(45, 11);
            label2.Name = "label2";
            label2.Size = new Size(80, 20);
            label2.TabIndex = 2;
            label2.Text = "(-)Imposto";
            // 
            // button2
            // 
            button2.BackColor = Color.Transparent;
            button2.Dock = DockStyle.Left;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Location = new Point(0, 0);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(39, 40);
            button2.TabIndex = 1;
            button2.Text = "˅";
            button2.UseVisualStyleBackColor = false;
            button2.Click += btnDrop_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.ForestGreen;
            panel2.Controls.Add(RBruta);
            panel2.Controls.Add(panel11);
            panel2.Location = new Point(5, 5);
            panel2.Margin = new Padding(3, 4, 3, 4);
            panel2.Name = "panel2";
            panel2.Size = new Size(501, 51);
            panel2.TabIndex = 0;
            // 
            // RBruta
            // 
            RBruta.BackgroundColor = Color.FromArgb(54, 169, 63);
            RBruta.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            RBruta.Dock = DockStyle.Fill;
            RBruta.EditMode = DataGridViewEditMode.EditOnEnter;
            RBruta.Location = new Point(0, 40);
            RBruta.Margin = new Padding(3, 4, 3, 4);
            RBruta.Name = "RBruta";
            RBruta.RowHeadersVisible = false;
            RBruta.RowHeadersWidth = 51;
            RBruta.RowTemplate.Height = 24;
            RBruta.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RBruta.Size = new Size(501, 11);
            RBruta.TabIndex = 1;
            RBruta.Visible = false;
            RBruta.CellEndEdit += ContentUpdate;
            // 
            // panel11
            // 
            panel11.BackColor = Color.FromArgb(220, 232, 220);
            panel11.Controls.Add(r0);
            panel11.Controls.Add(label1);
            panel11.Controls.Add(button1);
            panel11.Dock = DockStyle.Top;
            panel11.Location = new Point(0, 0);
            panel11.Margin = new Padding(3, 4, 3, 4);
            panel11.Name = "panel11";
            panel11.Size = new Size(501, 40);
            panel11.TabIndex = 0;
            // 
            // r0
            // 
            r0.AutoSize = true;
            r0.Location = new Point(357, 11);
            r0.Name = "r0";
            r0.Size = new Size(26, 20);
            r0.TabIndex = 1;
            r0.Tag = "Resultado";
            r0.Text = "R$";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 11);
            label1.Name = "label1";
            label1.Size = new Size(97, 20);
            label1.TabIndex = 1;
            label1.Text = "Receita Bruta";
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.Dock = DockStyle.Left;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Location = new Point(0, 0);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(39, 40);
            button1.TabIndex = 0;
            button1.Text = "˅";
            button1.UseVisualStyleBackColor = false;
            button1.Click += btnDrop_Click;
            // 
            // Dre
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Dre";
            Size = new Size(697, 464);
            panel1.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ROutras).EndInit();
            panel15.ResumeLayout(false);
            panel15.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Despesas).EndInit();
            panel14.ResumeLayout(false);
            panel14.PerformLayout();
            panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Custos).EndInit();
            panel13.ResumeLayout(false);
            panel13.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Imposto).EndInit();
            panel12.ResumeLayout(false);
            panel12.PerformLayout();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)RBruta).EndInit();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label r7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.DataGridView ROutras;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.Label r6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label r4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.DataGridView Despesas;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label r5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.DataGridView Custos;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.Label r3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label r2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView Imposto;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.Label r1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView RBruta;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label r0;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button6;
        private Button btn_salvar_dre;
    }
}
