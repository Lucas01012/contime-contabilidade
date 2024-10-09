namespace ConTime.Screens
{
    partial class Razo
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
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            dgv_result = new DataGridView();
            Header = new TextBox();
            panel1 = new RoundedPanel();
            dataGridView1 = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgv_result).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dgv_result
            // 
            dgv_result.AllowUserToResizeColumns = false;
            dgv_result.AllowUserToResizeRows = false;
            dgv_result.BackgroundColor = Color.White;
            dgv_result.BorderStyle = BorderStyle.None;
            dgv_result.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgv_result.ColumnHeadersVisible = false;
            dgv_result.Dock = DockStyle.Bottom;
            dgv_result.EditMode = DataGridViewEditMode.EditOnEnter;
            dgv_result.GridColor = Color.DarkGreen;
            dgv_result.Location = new Point(0, 212);
            dgv_result.MultiSelect = false;
            dgv_result.Name = "dgv_result";
            dgv_result.ReadOnly = true;
            dgv_result.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgv_result.RowHeadersVisible = false;
            dgv_result.RowHeadersWidth = 25;
            dgv_result.RowTemplate.Height = 24;
            dgv_result.ScrollBars = ScrollBars.Vertical;
            dgv_result.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgv_result.Size = new Size(192, 22);
            dgv_result.TabIndex = 1;
            dgv_result.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // Header
            // 
            Header.BorderStyle = BorderStyle.None;
            Header.Dock = DockStyle.Top;
            Header.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Header.Location = new Point(0, 0);
            Header.Name = "Header";
            Header.PlaceholderText = "Cabeçalho";
            Header.Size = new Size(192, 19);
            Header.TabIndex = 3;
            Header.TextAlign = HorizontalAlignment.Center;
            // 
            // panel1
            // 
            panel1.Controls.Add(dgv_result);
            panel1.Controls.Add(dataGridView1);
            panel1.Controls.Add(Header);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.RoundedBorderColor = Color.DarkGreen;
            panel1.RoundedBorderRadius = 25;
            panel1.RoundedBorderSize = 1;
            panel1.Size = new Size(192, 234);
            panel1.TabIndex = 2;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 7.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView1.GridColor = Color.DarkGreen;
            dataGridView1.Location = new Point(0, 19);
            dataGridView1.MultiSelect = false;
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidth = 25;
            dataGridView1.RowTemplate.Height = 24;
            dataGridView1.ScrollBars = ScrollBars.Vertical;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.Size = new Size(192, 215);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            // 
            // Razo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Transparent;
            Controls.Add(panel1);
            Margin = new Padding(4);
            Name = "Razo";
            Size = new Size(192, 234);
            ((System.ComponentModel.ISupportInitialize)dgv_result).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_result;
        private System.Windows.Forms.TextBox Header;
        private RoundedPanel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}
