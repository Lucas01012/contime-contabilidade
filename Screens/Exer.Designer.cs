using ConTime.Classes;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ConTime.Screens
{
    partial class Exer
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
            btn_Salvar = new Button();
            btn_Pdf = new Button();
            SuspendLayout();
            // 
            // btn_Salvar
            // 
            btn_Salvar.BackColor = System.Drawing.Color.FromArgb(185, 220, 201);
            btn_Salvar.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btn_Salvar.Location = new Point(0, 101);
            btn_Salvar.Name = "btn_Salvar";
            btn_Salvar.Size = new System.Drawing.Size(359, 33);
            btn_Salvar.TabIndex = 0;
            btn_Salvar.Text = "Salvar Projeto";
            btn_Salvar.UseVisualStyleBackColor = false;
            btn_Salvar.Click += btn_Salvar_Click;
            // 
            // btn_Pdf
            // 
            btn_Pdf.BackColor = System.Drawing.Color.FromArgb(185, 220, 201);
            btn_Pdf.Font = new Font("Yu Gothic UI", 12F, FontStyle.Bold);
            btn_Pdf.Location = new Point(0, 37);
            btn_Pdf.Name = "btn_Pdf";
            btn_Pdf.Size = new System.Drawing.Size(359, 33);
            btn_Pdf.TabIndex = 1;
            btn_Pdf.Text = "Gerar PDF Geral";
            btn_Pdf.UseVisualStyleBackColor = false;
            btn_Pdf.Click += btn_Pdf_Click;
            // 
            // Exer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = System.Drawing.Color.FromArgb(29, 61, 48);
            Controls.Add(btn_Pdf);
            Controls.Add(btn_Salvar);
            Name = "Exer";
            Size = new System.Drawing.Size(359, 200);
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Salvar;
        private Button btn_Pdf;
    }
   

}

