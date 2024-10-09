namespace ConTime.Screens
{
    partial class LDia
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
            dgv = new DataGridView();
            rb_c = new RadioButton();
            rb_d = new RadioButton();
            btn_delete = new Button();
            tb_saldo = new TextBox();
            btn_insert = new Button();
            dtp = new DateTimePicker();
            tb_historico = new TextBox();
            panel1 = new Panel();
            label6 = new Label();
            cb_cod = new ComboBox();
            label5 = new Label();
            tb_Conta = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            panel2 = new Panel();
            button7 = new Button();
            panel3 = new Panel();
            btn_salvar_ldia = new Button();
            ((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            SuspendLayout();
            // 
            // dgv
            // 
            dgv.AllowUserToResizeColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.BackgroundColor = Color.FromArgb(185, 220, 200);
            dgv.BorderStyle = BorderStyle.None;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv.Dock = DockStyle.Fill;
            dgv.EditMode = DataGridViewEditMode.EditOnEnter;
            dgv.Location = new Point(0, 0);
            dgv.Margin = new Padding(5);
            dgv.Name = "dgv";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = Color.Transparent;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgv.RowHeadersVisible = false;
            dgv.RowHeadersWidth = 51;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.Size = new Size(416, 340);
            dgv.TabIndex = 1;
            // 
            // rb_c
            // 
            rb_c.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            rb_c.AutoSize = true;
            rb_c.Location = new Point(5, 188);
            rb_c.Name = "rb_c";
            rb_c.Size = new Size(39, 24);
            rb_c.TabIndex = 12;
            rb_c.TabStop = true;
            rb_c.Tag = "C";
            rb_c.Text = "C";
            rb_c.UseVisualStyleBackColor = true;
            // 
            // rb_d
            // 
            rb_d.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            rb_d.AutoSize = true;
            rb_d.Location = new Point(5, 152);
            rb_d.Name = "rb_d";
            rb_d.Size = new Size(41, 24);
            rb_d.TabIndex = 11;
            rb_d.TabStop = true;
            rb_d.Tag = "D";
            rb_d.Text = "D";
            rb_d.UseVisualStyleBackColor = true;
            // 
            // btn_delete
            // 
            btn_delete.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_delete.Location = new Point(193, 187);
            btn_delete.Name = "btn_delete";
            btn_delete.Size = new Size(75, 29);
            btn_delete.TabIndex = 10;
            btn_delete.Text = "Apagar";
            btn_delete.UseVisualStyleBackColor = true;
            btn_delete.Click += btn_delete_Click;
            // 
            // tb_saldo
            // 
            tb_saldo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_saldo.Location = new Point(87, 109);
            tb_saldo.Name = "tb_saldo";
            tb_saldo.Size = new Size(181, 27);
            tb_saldo.TabIndex = 8;
            // 
            // btn_insert
            // 
            btn_insert.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_insert.Location = new Point(193, 149);
            btn_insert.Name = "btn_insert";
            btn_insert.Size = new Size(75, 29);
            btn_insert.TabIndex = 9;
            btn_insert.Text = "Inserir";
            btn_insert.UseVisualStyleBackColor = true;
            btn_insert.Click += btn_insert_Click;
            // 
            // dtp
            // 
            dtp.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            dtp.Format = DateTimePickerFormat.Short;
            dtp.Location = new Point(87, 0);
            dtp.Margin = new Padding(5);
            dtp.Name = "dtp";
            dtp.Size = new Size(181, 27);
            dtp.TabIndex = 13;
            dtp.Value = new DateTime(2024, 4, 1, 15, 39, 10, 0);
            // 
            // tb_historico
            // 
            tb_historico.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_historico.Location = new Point(87, 75);
            tb_historico.Margin = new Padding(5);
            tb_historico.Name = "tb_historico";
            tb_historico.Size = new Size(181, 27);
            tb_historico.TabIndex = 14;
            // 
            // panel1
            // 
            panel1.Controls.Add(label6);
            panel1.Controls.Add(cb_cod);
            panel1.Controls.Add(label5);
            panel1.Controls.Add(tb_Conta);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(tb_historico);
            panel1.Controls.Add(tb_saldo);
            panel1.Controls.Add(dtp);
            panel1.Controls.Add(btn_insert);
            panel1.Controls.Add(rb_c);
            panel1.Controls.Add(btn_delete);
            panel1.Controls.Add(rb_d);
            panel1.Location = new Point(5, 5);
            panel1.Margin = new Padding(5);
            panel1.Name = "panel1";
            panel1.Size = new Size(277, 239);
            panel1.TabIndex = 15;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(96, 155);
            label6.Name = "label6";
            label6.Size = new Size(58, 20);
            label6.TabIndex = 21;
            label6.Text = "Codigo";
            // 
            // cb_cod
            // 
            cb_cod.FormattingEnabled = true;
            cb_cod.Items.AddRange(new object[] { "10.01", "10.02", "10.03", "10.04", "10.05", "10.06", "10.07", "10.08", "10.09", "10.10", "10.11", "10.12", "11.01", "11.02", "11.03", "12.03", "12.04", "12.05", "12.06", "12.07", "12.08", "12.09", "12.10", "12.11", "20.01", "20.02", "20.03", "20.04", "20.05", "20.06", "20.07", "20.08", "20.09", "20.10", "20.11", "20.12", "20.13", "21.01", "21.02", "23.01", "23.02", "23.03", "23.04", "23.05", "30.01", "30.02", "30.03", "30.04", "30.05", "30.06", "30.07", "30.08", "30.09", "30.10", "30.11", "30.12", "30.13", "30.14", "30.15", "30.16", "30.17", "30.18", "30.19", "30.20", "31.01", "40.01", "40.02", "40.03", "40.04", "41.01", "50.01", "50.02", "51.01", "51.02", "51.03", "51.04", "51.05", "51.06", "52.01", "60.01", "60.02", "61.01", "61.02", "62.01", "70.01", "70.02", "70.03", "71.01", "71.02" });
            cb_cod.Location = new Point(87, 181);
            cb_cod.Margin = new Padding(3, 4, 3, 4);
            cb_cod.Name = "cb_cod";
            cb_cod.Size = new Size(79, 28);
            cb_cod.TabIndex = 20;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 41);
            label5.Name = "label5";
            label5.Size = new Size(48, 20);
            label5.TabIndex = 19;
            label5.Text = "Conta";
            // 
            // tb_Conta
            // 
            tb_Conta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            tb_Conta.Location = new Point(87, 37);
            tb_Conta.Margin = new Padding(5);
            tb_Conta.Name = "tb_Conta";
            tb_Conta.Size = new Size(181, 27);
            tb_Conta.TabIndex = 18;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 79);
            label4.Name = "label4";
            label4.Size = new Size(69, 20);
            label4.TabIndex = 17;
            label4.Text = "Historico";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 117);
            label3.Name = "label3";
            label3.Size = new Size(47, 20);
            label3.TabIndex = 16;
            label3.Text = "Saldo";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 8);
            label2.Name = "label2";
            label2.Size = new Size(41, 20);
            label2.TabIndex = 15;
            label2.Text = "Data";
            // 
            // panel2
            // 
            panel2.Controls.Add(dgv);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(298, 0);
            panel2.Margin = new Padding(5);
            panel2.Name = "panel2";
            panel2.Size = new Size(416, 340);
            panel2.TabIndex = 16;
            // 
            // button7
            // 
            button7.Location = new Point(3, 291);
            button7.Margin = new Padding(3, 4, 3, 4);
            button7.Name = "button7";
            button7.Size = new Size(75, 29);
            button7.TabIndex = 17;
            button7.Text = "PDF";
            button7.UseVisualStyleBackColor = true;
            button7.Click += GerarPdf;
            // 
            // panel3
            // 
            panel3.Controls.Add(btn_salvar_ldia);
            panel3.Controls.Add(button7);
            panel3.Controls.Add(panel1);
            panel3.Dock = DockStyle.Left;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(298, 340);
            panel3.TabIndex = 18;
            // 
            // btn_salvar_ldia
            // 
            btn_salvar_ldia.Location = new Point(198, 253);
            btn_salvar_ldia.Margin = new Padding(3, 4, 3, 4);
            btn_salvar_ldia.Name = "btn_salvar_ldia";
            btn_salvar_ldia.Size = new Size(75, 31);
            btn_salvar_ldia.TabIndex = 19;
            btn_salvar_ldia.Text = "Salvar";
            btn_salvar_ldia.UseVisualStyleBackColor = true;
            btn_salvar_ldia.Click += button1_Click;
            // 
            // LDia
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(panel2);
            Controls.Add(panel3);
            Margin = new Padding(5);
            Name = "LDia";
            Size = new Size(714, 340);
            ((System.ComponentModel.ISupportInitialize)dgv).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.RadioButton rb_c;
        private System.Windows.Forms.RadioButton rb_d;
        private System.Windows.Forms.Button btn_delete;
        private System.Windows.Forms.TextBox tb_saldo;
        private System.Windows.Forms.Button btn_insert;
        private System.Windows.Forms.DateTimePicker dtp;
        private System.Windows.Forms.TextBox tb_historico;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_Conta;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cb_cod;
        private System.Windows.Forms.Button button7;
        private Panel panel3;
        private Button btn_salvar_ldia;
    }
}
