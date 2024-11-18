namespace ConTime
{
    partial class Form_Body
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

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Body));
            pnl_menu = new Panel();
            btn_dre = new RoundedButton();
            btn_Balancete = new RoundedButton();
            btn_LDia = new RoundedButton();
            btn_Exer = new RoundedButton();
            btn_BPat = new RoundedButton();
            btn_Razo = new RoundedButton();
            btn_PCon = new RoundedButton();
            pnl_logo = new Panel();
            btn_menu = new Button();
            img_logo = new PictureBox();
            pnl_header = new Panel();
            btn_maximize = new Button();
            btn_minimize = new Button();
            btn_exit = new Button();
            label1 = new Label();
            pnl_body = new Panel();
            pnl_menu.SuspendLayout();
            pnl_logo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)img_logo).BeginInit();
            pnl_header.SuspendLayout();
            SuspendLayout();
            // 
            // pnl_menu
            // 
            pnl_menu.BackColor = Color.FromArgb(29, 61, 48);
            pnl_menu.Controls.Add(btn_dre);
            pnl_menu.Controls.Add(btn_Balancete);
            pnl_menu.Controls.Add(btn_LDia);
            pnl_menu.Controls.Add(btn_Exer);
            pnl_menu.Controls.Add(btn_BPat);
            pnl_menu.Controls.Add(btn_Razo);
            pnl_menu.Dock = DockStyle.Left;
            pnl_menu.Location = new Point(0, 94);
            pnl_menu.Margin = new Padding(3, 2, 3, 2);
            pnl_menu.Name = "pnl_menu";
            pnl_menu.Size = new Size(221, 626);
            pnl_menu.TabIndex = 1;
            // 
            // btn_dre
            // 
            btn_dre.BackColor = Color.FromArgb(185, 220, 200);
            btn_dre.FlatAppearance.BorderSize = 0;
            btn_dre.FlatStyle = FlatStyle.Flat;
            btn_dre.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Bold);
            btn_dre.ForeColor = Color.FromArgb(43, 100, 53);
            btn_dre.Image = Properties.Resources.schedule_2825712__1_;
            btn_dre.ImageAlign = ContentAlignment.MiddleLeft;
            btn_dre.Location = new Point(10, 218);
            btn_dre.Margin = new Padding(4);
            btn_dre.Name = "btn_dre";
            btn_dre.RoundedBorderColor = Color.DarkGreen;
            btn_dre.RoundedBorderRadius = 25;
            btn_dre.RoundedBorderSize = 1;
            btn_dre.Size = new Size(199, 64);
            btn_dre.TabIndex = 3;
            btn_dre.Tag = "Dre";
            btn_dre.Text = "Dre";
            btn_dre.UseVisualStyleBackColor = false;
            btn_dre.Click += btn_dre_Click;
            // 
            // btn_Balancete
            // 
            btn_Balancete.BackColor = Color.FromArgb(185, 220, 200);
            btn_Balancete.FlatAppearance.BorderSize = 0;
            btn_Balancete.FlatStyle = FlatStyle.Flat;
            btn_Balancete.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Bold);
            btn_Balancete.ForeColor = Color.FromArgb(43, 100, 53);
            btn_Balancete.Image = Properties.Resources.accounting_10365430__1_;
            btn_Balancete.ImageAlign = ContentAlignment.MiddleLeft;
            btn_Balancete.Location = new Point(11, 147);
            btn_Balancete.Margin = new Padding(4);
            btn_Balancete.Name = "btn_Balancete";
            btn_Balancete.RoundedBorderColor = Color.DarkGreen;
            btn_Balancete.RoundedBorderRadius = 25;
            btn_Balancete.RoundedBorderSize = 1;
            btn_Balancete.Size = new Size(199, 64);
            btn_Balancete.TabIndex = 2;
            btn_Balancete.Tag = "Balancete";
            btn_Balancete.Text = "Balancete";
            btn_Balancete.UseVisualStyleBackColor = false;
            btn_Balancete.Click += btn_Balancete_Click;
            // 
            // btn_LDia
            // 
            btn_LDia.BackColor = Color.FromArgb(185, 220, 200);
            btn_LDia.FlatAppearance.BorderSize = 0;
            btn_LDia.FlatStyle = FlatStyle.Flat;
            btn_LDia.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Bold);
            btn_LDia.ForeColor = Color.FromArgb(43, 100, 53);
            btn_LDia.Image = Properties.Resources.livro;
            btn_LDia.ImageAlign = ContentAlignment.MiddleLeft;
            btn_LDia.Location = new Point(11, 6);
            btn_LDia.Margin = new Padding(4);
            btn_LDia.Name = "btn_LDia";
            btn_LDia.RoundedBorderColor = Color.DarkGreen;
            btn_LDia.RoundedBorderRadius = 25;
            btn_LDia.RoundedBorderSize = 1;
            btn_LDia.Size = new Size(199, 64);
            btn_LDia.TabIndex = 6;
            btn_LDia.Tag = "Livro Diário";
            btn_LDia.Text = "Livro Diário";
            btn_LDia.UseVisualStyleBackColor = false;
            btn_LDia.Click += btn_LDia_Click;
            // 
            // btn_Exer
            // 
            btn_Exer.BackColor = Color.FromArgb(185, 220, 200);
            btn_Exer.FlatAppearance.BorderSize = 0;
            btn_Exer.FlatStyle = FlatStyle.Flat;
            btn_Exer.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Bold);
            btn_Exer.ForeColor = Color.FromArgb(43, 100, 53);
            btn_Exer.Image = Properties.Resources.schedule_7884931__2_;
            btn_Exer.ImageAlign = ContentAlignment.MiddleLeft;
            btn_Exer.Location = new Point(11, 361);
            btn_Exer.Margin = new Padding(4);
            btn_Exer.Name = "btn_Exer";
            btn_Exer.RoundedBorderColor = Color.DarkGreen;
            btn_Exer.RoundedBorderRadius = 25;
            btn_Exer.RoundedBorderSize = 1;
            btn_Exer.Size = new Size(199, 64);
            btn_Exer.TabIndex = 5;
            btn_Exer.Tag = "Exercicios";
            btn_Exer.Text = "Exercicios";
            btn_Exer.UseVisualStyleBackColor = false;
            btn_Exer.Click += btn_Exer_Click;
            // 
            // btn_BPat
            // 
            btn_BPat.BackColor = Color.FromArgb(185, 220, 200);
            btn_BPat.FlatAppearance.BorderSize = 0;
            btn_BPat.FlatStyle = FlatStyle.Flat;
            btn_BPat.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Bold);
            btn_BPat.ForeColor = Color.FromArgb(43, 100, 53);
            btn_BPat.Image = Properties.Resources.bank_2474043;
            btn_BPat.ImageAlign = ContentAlignment.MiddleLeft;
            btn_BPat.Location = new Point(11, 290);
            btn_BPat.Margin = new Padding(4);
            btn_BPat.Name = "btn_BPat";
            btn_BPat.RoundedBorderColor = Color.DarkGreen;
            btn_BPat.RoundedBorderRadius = 25;
            btn_BPat.RoundedBorderSize = 1;
            btn_BPat.Size = new Size(199, 64);
            btn_BPat.TabIndex = 4;
            btn_BPat.Tag = "Balanço Patrimonial";
            btn_BPat.Text = "Balanço Patrimonial";
            btn_BPat.TextImageRelation = TextImageRelation.ImageBeforeText;
            btn_BPat.UseVisualStyleBackColor = false;
            btn_BPat.Click += btn_BPat_Click;
            // 
            // btn_Razo
            // 
            btn_Razo.BackColor = Color.FromArgb(185, 220, 200);
            btn_Razo.FlatAppearance.BorderSize = 0;
            btn_Razo.FlatStyle = FlatStyle.Flat;
            btn_Razo.Font = new Font("Yu Gothic UI", 14.25F, FontStyle.Bold);
            btn_Razo.ForeColor = Color.FromArgb(43, 100, 53);
            btn_Razo.Image = Properties.Resources.t_7720312;
            btn_Razo.ImageAlign = ContentAlignment.MiddleLeft;
            btn_Razo.Location = new Point(10, 76);
            btn_Razo.Margin = new Padding(4);
            btn_Razo.Name = "btn_Razo";
            btn_Razo.RoundedBorderColor = Color.DarkGreen;
            btn_Razo.RoundedBorderRadius = 25;
            btn_Razo.RoundedBorderSize = 1;
            btn_Razo.Size = new Size(199, 64);
            btn_Razo.TabIndex = 2;
            btn_Razo.Tag = "Razonete";
            btn_Razo.Text = "Razonete";
            btn_Razo.UseVisualStyleBackColor = false;
            btn_Razo.Click += btn_Razo_Click;
            // 
            // btn_PCon
            // 
            btn_PCon.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btn_PCon.BackColor = Color.FromArgb(185, 220, 200);
            btn_PCon.FlatAppearance.BorderSize = 0;
            btn_PCon.FlatStyle = FlatStyle.Flat;
            btn_PCon.ImageAlign = ContentAlignment.MiddleLeft;
            btn_PCon.Location = new Point(874, 18);
            btn_PCon.Margin = new Padding(4);
            btn_PCon.Name = "btn_PCon";
            btn_PCon.RoundedBorderColor = Color.DarkGreen;
            btn_PCon.RoundedBorderRadius = 20;
            btn_PCon.RoundedBorderSize = 1;
            btn_PCon.Size = new Size(70, 60);
            btn_PCon.TabIndex = 1;
            btn_PCon.Tag = "Plano de Contas";
            btn_PCon.Text = "Plano de Contas";
            btn_PCon.UseVisualStyleBackColor = false;
            btn_PCon.Click += button1_Click;
            // 
            // pnl_logo
            // 
            pnl_logo.BackColor = Color.Gainsboro;
            pnl_logo.Controls.Add(btn_menu);
            pnl_logo.Controls.Add(img_logo);
            pnl_logo.Dock = DockStyle.Left;
            pnl_logo.Location = new Point(0, 0);
            pnl_logo.Margin = new Padding(3, 2, 3, 2);
            pnl_logo.Name = "pnl_logo";
            pnl_logo.Size = new Size(209, 94);
            pnl_logo.TabIndex = 0;
            // 
            // btn_menu
            // 
            btn_menu.BackColor = Color.FromArgb(29, 61, 48);
            btn_menu.FlatStyle = FlatStyle.Flat;
            btn_menu.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btn_menu.Image = Properties.Resources.barra_de_menu;
            btn_menu.Location = new Point(0, 0);
            btn_menu.Margin = new Padding(3, 2, 3, 2);
            btn_menu.Name = "btn_menu";
            btn_menu.Size = new Size(94, 94);
            btn_menu.TabIndex = 0;
            btn_menu.UseVisualStyleBackColor = false;
            btn_menu.Click += btn_menu_Click;
            // 
            // img_logo
            // 
            img_logo.BackColor = Color.FromArgb(29, 61, 48);
            img_logo.Image = Properties.Resources.logomarcaaaaa;
            img_logo.Location = new Point(88, 0);
            img_logo.Margin = new Padding(3, 2, 3, 2);
            img_logo.Name = "img_logo";
            img_logo.Size = new Size(122, 94);
            img_logo.SizeMode = PictureBoxSizeMode.Zoom;
            img_logo.TabIndex = 1;
            img_logo.TabStop = false;
            img_logo.Click += img_logo_Click_1;
            img_logo.MouseDown += pnl_header_MouseDown;
            // 
            // pnl_header
            // 
            pnl_header.BackColor = Color.FromArgb(29, 61, 48);
            pnl_header.Controls.Add(btn_maximize);
            pnl_header.Controls.Add(btn_minimize);
            pnl_header.Controls.Add(btn_exit);
            pnl_header.Controls.Add(btn_PCon);
            pnl_header.Controls.Add(pnl_logo);
            pnl_header.Controls.Add(label1);
            pnl_header.Dock = DockStyle.Top;
            pnl_header.Location = new Point(0, 0);
            pnl_header.Margin = new Padding(3, 2, 3, 2);
            pnl_header.Name = "pnl_header";
            pnl_header.Size = new Size(1080, 94);
            pnl_header.TabIndex = 0;
            pnl_header.MouseDown += pnl_header_MouseDown;
            // 
            // btn_maximize
            // 
            btn_maximize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_maximize.BackColor = Color.Transparent;
            btn_maximize.BackgroundImageLayout = ImageLayout.None;
            btn_maximize.FlatStyle = FlatStyle.Flat;
            btn_maximize.Font = new Font("Microsoft Sans Serif", 15.75F);
            btn_maximize.ForeColor = Color.FromArgb(185, 220, 200);
            btn_maximize.Location = new Point(1003, 0);
            btn_maximize.Margin = new Padding(3, 2, 3, 2);
            btn_maximize.Name = "btn_maximize";
            btn_maximize.Size = new Size(36, 29);
            btn_maximize.TabIndex = 2;
            btn_maximize.Text = "□";
            btn_maximize.UseVisualStyleBackColor = false;
            btn_maximize.Click += btn_maximize_Click;
            // 
            // btn_minimize
            // 
            btn_minimize.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_minimize.BackColor = Color.Transparent;
            btn_minimize.BackgroundImageLayout = ImageLayout.None;
            btn_minimize.FlatStyle = FlatStyle.Flat;
            btn_minimize.Font = new Font("Microsoft Sans Serif", 15.75F);
            btn_minimize.ForeColor = Color.FromArgb(185, 220, 200);
            btn_minimize.Location = new Point(961, 0);
            btn_minimize.Margin = new Padding(3, 2, 3, 2);
            btn_minimize.Name = "btn_minimize";
            btn_minimize.Size = new Size(36, 29);
            btn_minimize.TabIndex = 1;
            btn_minimize.Text = "-";
            btn_minimize.UseVisualStyleBackColor = false;
            btn_minimize.Click += btn_minimize_Click;
            // 
            // btn_exit
            // 
            btn_exit.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btn_exit.BackColor = Color.Transparent;
            btn_exit.BackgroundImageLayout = ImageLayout.None;
            btn_exit.FlatStyle = FlatStyle.Flat;
            btn_exit.Font = new Font("Microsoft Sans Serif", 15.75F);
            btn_exit.ForeColor = Color.FromArgb(185, 220, 200);
            btn_exit.Location = new Point(1044, 0);
            btn_exit.Margin = new Padding(3, 2, 3, 2);
            btn_exit.Name = "btn_exit";
            btn_exit.Size = new Size(36, 29);
            btn_exit.TabIndex = 0;
            btn_exit.Text = "x";
            btn_exit.UseVisualStyleBackColor = false;
            btn_exit.Click += btn_exit_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Impact", 39.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(96, 128, 111);
            label1.Location = new Point(418, 13);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(225, 65);
            label1.TabIndex = 3;
            label1.Text = "ConTime";
            // 
            // pnl_body
            // 
            pnl_body.BackColor = Color.FromArgb(185, 220, 200);
            pnl_body.Dock = DockStyle.Fill;
            pnl_body.Location = new Point(0, 94);
            pnl_body.Margin = new Padding(3, 2, 3, 2);
            pnl_body.Name = "pnl_body";
            pnl_body.Size = new Size(1080, 626);
            pnl_body.TabIndex = 2;
            pnl_body.ControlAdded += pnl_body_ControlAdded;
            pnl_body.Paint += pnl_body_Paint;
            // 
            // Form_Body
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 720);
            Controls.Add(pnl_menu);
            Controls.Add(pnl_body);
            Controls.Add(pnl_header);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 2, 3, 2);
            MaximumSize = new Size(1080, 720);
            MinimizeBox = false;
            MinimumSize = new Size(1080, 720);
            Name = "Form_Body";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ConTime";
            Load += Form_Body_Load;
            pnl_menu.ResumeLayout(false);
            pnl_logo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)img_logo).EndInit();
            pnl_header.ResumeLayout(false);
            pnl_header.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnl_menu;
        private System.Windows.Forms.Panel pnl_logo;
        private System.Windows.Forms.Panel pnl_header;
        private System.Windows.Forms.Panel pnl_body;
        private System.Windows.Forms.Button btn_maximize;
        private System.Windows.Forms.Button btn_minimize;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_menu;
        private System.Windows.Forms.PictureBox img_logo;
        private System.Windows.Forms.Label label1;
        private RoundedButton btn_PCon;
        private RoundedButton btn_BPat;
        private RoundedButton btn_dre;
        private RoundedButton btn_Razo;
        private RoundedButton btn_LDia;
        private RoundedButton btn_Exer;
        private RoundedButton btn_Balancete;
    }
}

