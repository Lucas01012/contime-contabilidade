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
            btn_Importar = new Button();
            btnTeste = new Button();
            SuspendLayout();
            // 
            // btn_Salvar
            // 
            btn_Salvar.Location = new Point(0, 0);
            btn_Salvar.Name = "btn_Salvar";
            btn_Salvar.Size = new System.Drawing.Size(75, 23);
            btn_Salvar.TabIndex = 0;
            btn_Salvar.Text = "Salvar Projeto";
            btn_Salvar.UseVisualStyleBackColor = true;
            btn_Salvar.Click += btn_Salvar_Click;
            // 
            // btn_Pdf
            // 
            btn_Pdf.Location = new Point(81, 0);
            btn_Pdf.Name = "btn_Pdf";
            btn_Pdf.Size = new System.Drawing.Size(75, 23);
            btn_Pdf.TabIndex = 1;
            btn_Pdf.Text = "Gerar PDF do Projeto";
            btn_Pdf.UseVisualStyleBackColor = true;
            btn_Pdf.Click += btn_Pdf_Click;
            // 
            // btn_Importar
            // 
            btn_Importar.Location = new Point(162, 0);
            btn_Importar.Name = "btn_Importar";
            btn_Importar.Size = new System.Drawing.Size(75, 23);
            btn_Importar.TabIndex = 2;
            btn_Importar.Text = "Importar Projeto";
            btn_Importar.UseVisualStyleBackColor = true;
            // 
            // btnTeste
            // 
            btnTeste.Location = new Point(0, 29);
            btnTeste.Name = "btnTeste";
            btnTeste.Size = new System.Drawing.Size(75, 23);
            btnTeste.TabIndex = 3;
            btnTeste.Text = "button1";
            btnTeste.UseVisualStyleBackColor = true;
            // 
            // Exer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(btnTeste);
            Controls.Add(btn_Importar);
            Controls.Add(btn_Pdf);
            Controls.Add(btn_Salvar);
            Name = "Exer";
            ResumeLayout(false);
        }

        #endregion

        private Button btn_Salvar;
        private Button btn_Pdf;
        private Button btn_Importar;
        private Button btnTeste;
    }
   

}

