namespace ConTime.Screens
{
    partial class Bal
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            dgv_bal = new DataGridView();
            label6 = new Label();
            cb_cod = new ComboBox();
            btn_insert = new Button();
            rb_c = new RadioButton();
            btn_delete = new Button();
            rb_d = new RadioButton();
            label3 = new Label();
            tb_saldo = new TextBox();
            label5 = new Label();
            tb_Conta = new TextBox();
            panel1 = new Panel();
            button7 = new Button();
            btn_salvar_bal = new Button();
            panel2 = new Panel();
            button1 = new Button();
            panel3 = new Panel();
            ((System.ComponentModel.ISupportInitialize)dgv_bal).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // dgv_bal
            // 
            dgv_bal.BackgroundColor = Color.FromArgb(185, 220, 200);
            dgv_bal.BorderStyle = BorderStyle.None;
            dgv_bal.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = Color.ForestGreen;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.False;
            dgv_bal.DefaultCellStyle = dataGridViewCellStyle1;
            dgv_bal.Dock = DockStyle.Fill;
            dgv_bal.EditMode = DataGridViewEditMode.EditOnEnter;
            dgv_bal.GridColor = Color.DarkGreen;
            dgv_bal.Location = new Point(0, 0);
            dgv_bal.Name = "dgv_bal";
            dgv_bal.RowHeadersVisible = false;
            dgv_bal.RowHeadersWidth = 51;
            dgv_bal.RowTemplate.Height = 24;
            dgv_bal.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv_bal.Size = new Size(446, 472);
            dgv_bal.TabIndex = 0;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            label6.ForeColor = Color.White;
            label6.Location = new Point(58, 68);
            label6.Name = "label6";
            label6.Size = new Size(64, 21);
            label6.TabIndex = 27;
            label6.Text = "Código";
            // 
            // cb_cod
            // 
            cb_cod.BackColor = Color.FromArgb(185, 220, 201);
            cb_cod.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            cb_cod.FormattingEnabled = true;
            cb_cod.Items.AddRange(new object[] { "10.01", "10.02", "10.03", "10.04", "10.05", "10.06", "10.07", "10.08", "10.09", "10.10", "10.11", "10.12", "11.01", "11.02", "11.03", "12.03", "12.04", "12.05", "12.06", "12.07", "12.08", "12.09", "12.10", "12.11", "20.01", "20.02", "20.03", "20.04", "20.05", "20.06", "20.07", "20.08", "20.09", "20.10", "20.11", "20.12", "20.13", "21.01", "21.02", "23.01", "23.02", "23.03", "23.04", "23.05", "30.01", "30.02", "30.03", "30.04", "30.05", "30.06", "30.07", "30.08", "30.09", "30.10", "30.11", "30.12", "30.13", "30.14", "30.15", "30.16", "30.17", "30.18", "30.19", "30.20", "31.01", "40.01", "40.02", "40.03", "40.04", "41.01", "50.01", "50.02", "51.01", "51.02", "51.03", "51.04", "51.05", "51.06", "52.01", "60.01", "60.02", "61.01", "61.02", "62.01", "70.01", "70.02", "70.03", "71.01", "71.02" });
            cb_cod.Location = new Point(55, 92);
            cb_cod.Name = "cb_cod";
            cb_cod.Size = new Size(70, 29);
            cb_cod.TabIndex = 26;
            // 
            // btn_insert
            // 
            btn_insert.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_insert.BackColor = Color.FromArgb(185, 220, 201);
            btn_insert.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Bold);
            btn_insert.Location = new Point(131, 70);
            btn_insert.Margin = new Padding(3, 2, 3, 2);
            btn_insert.Name = "btn_insert";
            btn_insert.Size = new Size(75, 33);
            btn_insert.TabIndex = 22;
            btn_insert.Text = "Inserir";
            btn_insert.UseVisualStyleBackColor = false;
            btn_insert.Click += btn_insert_Click;
            // 
            // rb_c
            // 
            rb_c.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            rb_c.AutoSize = true;
            rb_c.ForeColor = Color.White;
            rb_c.Location = new Point(5, 107);
            rb_c.Margin = new Padding(3, 2, 3, 2);
            rb_c.Name = "rb_c";
            rb_c.Size = new Size(33, 19);
            rb_c.TabIndex = 25;
            rb_c.TabStop = true;
            rb_c.Tag = "C";
            rb_c.Text = "C";
            rb_c.UseVisualStyleBackColor = true;
            // 
            // btn_delete
            // 
            btn_delete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_delete.BackColor = Color.FromArgb(185, 220, 201);
            btn_delete.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Bold);
            btn_delete.Location = new Point(131, 118);
            btn_delete.Margin = new Padding(3, 2, 3, 2);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(75, 33);
            btn_delete.TabIndex = 23;
            btn_delete.Text = "Apagar";
            btn_delete.UseVisualStyleBackColor = false;
            btn_delete.Click += btn_delete_Click;
            // 
            // rb_d
            // 
            rb_d.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            rb_d.AutoSize = true;
            rb_d.ForeColor = Color.White;
            rb_d.Location = new Point(5, 84);
            rb_d.Margin = new Padding(3, 2, 3, 2);
            rb_d.Name = "rb_d";
            rb_d.Size = new Size(33, 19);
            rb_d.TabIndex = 24;
            rb_d.TabStop = true;
            rb_d.Tag = "D";
            rb_d.Text = "D";
            rb_d.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(3, 40);
            label3.Name = "label3";
            label3.Size = new Size(45, 21);
            label3.TabIndex = 30;
            label3.Text = "Total";
            // 
            // tb_saldo
            // 
            tb_saldo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_saldo.BackColor = Color.FromArgb(185, 220, 201);
            tb_saldo.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            tb_saldo.Location = new Point(55, 37);
            tb_saldo.Margin = new Padding(3, 2, 3, 2);
            tb_saldo.Name = "tb_saldo";
            tb_saldo.Size = new Size(151, 29);
            tb_saldo.TabIndex = 28;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            label5.ForeColor = Color.White;
            label5.Location = new Point(3, 11);
            label5.Name = "label5";
            label5.Size = new Size(53, 21);
            label5.TabIndex = 32;
            label5.Text = "Conta";
            // 
            // tb_Conta
            // 
            tb_Conta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_Conta.BackColor = Color.FromArgb(185, 220, 201);
            tb_Conta.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            tb_Conta.Location = new Point(55, 10);
            tb_Conta.Margin = new Padding(4);
            tb_Conta.Name = "tb_Conta";
            tb_Conta.Size = new Size(151, 29);
            tb_Conta.TabIndex = 31;
            tb_Conta.TextChanged += tb_Conta_TextChanged;
            // 
            // panel1
            // 
            panel1.Controls.Add(tb_saldo);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(rb_d);
            panel1.Controls.Add(btn_delete);
            panel1.Controls.Add(tb_Conta);
            panel1.Controls.Add(btn_insert);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(rb_c);
            panel1.Controls.Add(label6);
            panel1.Controls.Add(cb_cod);
            panel1.Location = new Point(1, 94);
            panel1.Name = "panel1";
            panel1.Size = new Size(212, 150);
            panel1.TabIndex = 33;
            // 
            // button7
            // 
            button7.BackColor = Color.FromArgb(185, 220, 201);
            button7.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Bold);
            button7.Location = new Point(69, 300);
            button7.Name = "button7";
            button7.Size = new Size(81, 33);
            button7.TabIndex = 34;
            button7.Text = "PDF";
            button7.UseVisualStyleBackColor = false;
            button7.Click += GerarPdf;
            // 
            // btn_salvar_bal
            // 
            btn_salvar_bal.BackColor = Color.FromArgb(185, 220, 201);
            btn_salvar_bal.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Bold);
            btn_salvar_bal.Location = new Point(6, 250);
            btn_salvar_bal.Name = "btn_salvar_bal";
            btn_salvar_bal.Size = new Size(76, 33);
            btn_salvar_bal.TabIndex = 35;
            btn_salvar_bal.Text = "Salvar";
            btn_salvar_bal.UseVisualStyleBackColor = false;
            btn_salvar_bal.Click += btn_salvar_bal_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(29, 61, 48);
            panel2.Controls.Add(button1);
            panel2.Controls.Add(panel1);
            panel2.Controls.Add(button7);
            panel2.Controls.Add(btn_salvar_bal);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(219, 472);
            panel2.TabIndex = 36;
            // 
            // button1
            // 
            button1.BackColor = Color.FromArgb(185, 220, 201);
            button1.Font = new Font("Yu Gothic UI", 9.75F, FontStyle.Bold);
            button1.Location = new Point(132, 250);
            button1.Name = "button1";
            button1.Size = new Size(76, 33);
            button1.TabIndex = 36;
            button1.Text = "Importar";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // panel3
            // 
            panel3.Controls.Add(dgv_bal);
            panel3.Dock = DockStyle.Fill;
            panel3.Location = new Point(219, 0);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(446, 472);
            panel3.TabIndex = 37;
            // 
            // Bal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel3);
            Controls.Add(panel2);
            Name = "Bal";
            Size = new Size(665, 472);
            ((System.ComponentModel.ISupportInitialize)dgv_bal).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.DataGridView dgv_bal;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_cod;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.RadioButton rb_c;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.RadioButton rb_d;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_saldo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_Conta;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button7;
        private Button btn_salvar_bal;
        private Panel panel2;
        private Panel panel3;
        private Button button1;
    }
}
