namespace ConTime.Screens
{
    partial class Razonete
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
            pnl_razo = new RoundedPanel();
            btn_addUC = new RoundedButton();
            btn_salvar_razo = new Button();
            roundedButton1 = new RoundedButton();
            pnl_razonetes = new Panel();
            panel1 = new Panel();
            btn_ImportarRazo = new Button();
            pnl_razo.SuspendLayout();
            pnl_razonetes.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // pnl_razo
            // 
            pnl_razo.BackColor = SystemColors.ActiveBorder;
            pnl_razo.Controls.Add(btn_addUC);
            pnl_razo.Location = new Point(0, 0);
            pnl_razo.Margin = new Padding(3, 2, 3, 2);
            pnl_razo.Name = "pnl_razo";
            pnl_razo.RoundedBorderColor = Color.DarkGreen;
            pnl_razo.RoundedBorderRadius = 25;
            pnl_razo.RoundedBorderSize = 0;
            pnl_razo.Size = new Size(192, 234);
            pnl_razo.TabIndex = 0;
            // 
            // btn_addUC
            // 
            btn_addUC.BackColor = Color.FromArgb(185, 220, 201);
            btn_addUC.Dock = DockStyle.Fill;
            btn_addUC.FlatAppearance.BorderSize = 0;
            btn_addUC.FlatStyle = FlatStyle.Flat;
            btn_addUC.Font = new Font("MS PGothic", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btn_addUC.Location = new Point(0, 0);
            btn_addUC.Margin = new Padding(3, 2, 3, 2);
            btn_addUC.Name = "btn_addUC";
            btn_addUC.RoundedBorderColor = Color.DarkGreen;
            btn_addUC.RoundedBorderRadius = 25;
            btn_addUC.RoundedBorderSize = 1;
            btn_addUC.Size = new Size(192, 234);
            btn_addUC.TabIndex = 0;
            btn_addUC.Text = "+";
            btn_addUC.UseVisualStyleBackColor = false;
            btn_addUC.Click += btn_addUC_Click;
            // 
            // btn_salvar_razo
            // 
            btn_salvar_razo.BackColor = Color.FromArgb(185, 220, 201);
            btn_salvar_razo.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            btn_salvar_razo.Location = new Point(3, 14);
            btn_salvar_razo.Name = "btn_salvar_razo";
            btn_salvar_razo.Size = new Size(59, 34);
            btn_salvar_razo.TabIndex = 3;
            btn_salvar_razo.Text = "Salvar";
            btn_salvar_razo.UseVisualStyleBackColor = false;
            btn_salvar_razo.Click += btn_salvar_razo_Click;
            // 
            // roundedButton1
            // 
            roundedButton1.BackColor = Color.FromArgb(185, 220, 201);
            roundedButton1.FlatAppearance.BorderSize = 0;
            roundedButton1.FlatStyle = FlatStyle.Flat;
            roundedButton1.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            roundedButton1.Location = new Point(138, 13);
            roundedButton1.Margin = new Padding(3, 2, 3, 2);
            roundedButton1.Name = "roundedButton1";
            roundedButton1.RoundedBorderColor = Color.DarkGreen;
            roundedButton1.RoundedBorderRadius = 10;
            roundedButton1.RoundedBorderSize = 1;
            roundedButton1.Size = new Size(39, 34);
            roundedButton1.TabIndex = 0;
            roundedButton1.Text = "PDF";
            roundedButton1.UseVisualStyleBackColor = false;
            roundedButton1.Click += CreatePdf;
            // 
            // pnl_razonetes
            // 
            pnl_razonetes.BackColor = Color.FromArgb(96, 128, 111);
            pnl_razonetes.Controls.Add(pnl_razo);
            pnl_razonetes.Dock = DockStyle.Fill;
            pnl_razonetes.Location = new Point(0, 51);
            pnl_razonetes.Margin = new Padding(3, 2, 3, 2);
            pnl_razonetes.Name = "pnl_razonetes";
            pnl_razonetes.Size = new Size(839, 475);
            pnl_razonetes.TabIndex = 2;
            pnl_razonetes.Paint += pnl_razonetes_Paint;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(29, 61, 48);
            panel1.Controls.Add(btn_ImportarRazo);
            panel1.Controls.Add(btn_salvar_razo);
            panel1.Controls.Add(roundedButton1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(839, 51);
            panel1.TabIndex = 4;
            // 
            // btn_ImportarRazo
            // 
            btn_ImportarRazo.BackColor = Color.FromArgb(185, 220, 201);
            btn_ImportarRazo.Font = new Font("Yu Gothic UI", 9F, FontStyle.Bold);
            btn_ImportarRazo.Location = new Point(68, 14);
            btn_ImportarRazo.Name = "btn_ImportarRazo";
            btn_ImportarRazo.Size = new Size(64, 34);
            btn_ImportarRazo.TabIndex = 4;
            btn_ImportarRazo.Text = "Importar";
            btn_ImportarRazo.UseVisualStyleBackColor = false;
            btn_ImportarRazo.Click += btn_ImportarRazo_Click;
            // 
            // Razonete
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(pnl_razonetes);
            Controls.Add(panel1);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Razonete";
            Size = new Size(839, 526);
            pnl_razo.ResumeLayout(false);
            pnl_razonetes.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private RoundedPanel pnl_razo;
        private RoundedButton btn_addUC;
        private Panel pnl_razonetes;
        private RoundedButton roundedButton1;
        private Button btn_salvar_razo;
        private Panel panel1;
        private Button btn_ImportarRazo;
    }
}
