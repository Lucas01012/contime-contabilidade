using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConTime.Screens
{
    public partial class PlanoCon : Form
    {
        private static PlanoCon instance;

        private PlanoCon()
        {
            InitializeComponent();
            InicializarLayout();
        }

        // Método GetInstance para garantir que o formulário seja um Singleton
        public static PlanoCon GetInstance()
        {
            if (instance == null || instance.IsDisposed)
            {
                instance = new PlanoCon();
            }
            return instance;
        }

        private void ConfigurarEstiloGlobal(Control controle, Font fonte, Color corDeFundo)
        {
            foreach (Control c in controle.Controls)
            {
                // Aplicar fonte global
                c.Font = fonte;

                // Aplicar cor de fundo para painéis
                if (c is Panel)
                {
                    c.BackColor = corDeFundo;
                }

                // Recursividade para aplicar em subcontroles
                if (c.Controls.Count > 0)
                {
                    ConfigurarEstiloGlobal(c, fonte, corDeFundo);
                }
            }
        }

        private void InicializarLayout()
        {
            this.Text = "Plano de Contas";
            ConfigurarEstiloGlobal(this, new Font("Yu Gothic UI", 9, FontStyle.Bold), Color.FromArgb(185, 220, 200));

            // Criar um painel principal com scroll
            Panel mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true // Adiciona o scroll
            };

            this.Controls.Add(mainPanel);

            // Criar painéis expansíveis com DataGrid
            CriarPainelExpansivel(mainPanel, "Ativo Circulante", new List<string>
        {
            "10.01 Caixa",
            "10.02 Bancos Conta Movimento",
            "10.03 Aplicações Financeiras",
            "10.04 Clientes",
            "10.05 Duplicatas a Receber",
            "10.06 (-) Provisão para Crédito de Liquidação Duvidosa",
            "10.07 Promissórias a Receber",
            "10.08 ICMS a Recuperar",
            "10.09 Estoques: Estoque de Mercadorias",
            "10.10 Estoque de Material de Expediente",
            "10.11 Juros Passivos a Vencer",
            "10.12 Prêmios de Seguros a Vencer"
        });

            CriarPainelExpansivel(mainPanel, "Ativo Não Circulante", new List<string>
        {
            "20.01 Investimentos",
            "20.02 Imobilizado",
            "20.03 Intangível",
            "20.04 Realizável a Longo Prazo",
            "20.05 Propriedades para Investimento"
        });

            CriarPainelExpansivel(mainPanel, "Passivo Circulante", new List<string>
        {
            "20.01 Fornecedores",
            "20.02 Duplicatas a Pagar",
            "20.03 Promissórias a Pagar",
            "20.04 COFINS a Recolher",
            "20.05 ICMS a Recolher",
            "20.06 PIS s/ Faturamento a Recolher",
            "20.07 INSS. a Recolher",
            "20.08 FGTS a Recolher",
            "20.09 Salários a Pagar",
            "20.10 Dividendos a Pagar",
            "20.11 Impostos e Taxas a Recolher",
            "20.12 ISS a Recolher",
            "20.13 Provisão para Imposto de Renda"
        });

            CriarPainelExpansivel(mainPanel, "Despesas Operacionais", new List<string>
        {
            "30.01 Água e Esgoto",
            "30.02 Aluguéis Pagos",
            "30.03 Amortização",
            "30.04 Café e Lanches",
            "30.05 Contribuições de Previdência",
            "30.06 Depreciação",
            "30.07 Descontos Concedidos",
            "30.08 Despesas Bancárias",
            "30.09 Despesas com Crédito de Liquidação Duvidosa",
            "30.10 Encargos Sociais",
            "30.11 FGTS",
            "30.12 Fretes e Carretos",
            "30.13 Impostos e Taxas",
            "30.14 Juros Pagos",
            "30.15 Energia Elétrica",
            "30.16 Consumo de telefone",
            "30.17 Material de Expediente",
            "30.18 Prêmios de Seguro",
            "30.19 Salário",
            "30.20 Despesas Eventuais",
            "Perdas em Transações do Ativo"
        });

            CriarPainelExpansivel(mainPanel, "Receitas Operacionais", new List<string>
        {
            "40.01 Aluguéis Recebidos",
            "40.02 Descontos Obtidos",
            "40.03 Juros Recebidos",
            "40.04 Receitas Eventuais",
            "41.01 Ganhos em Transações do Ativo"
        });

            CriarPainelExpansivel(mainPanel, "Custos e Receitas", new List<string>
        {
            "50. Custos das Compras",
            "50.02 Fretes e Seguros Sobre Compras",
            "51. Deduções e Abatimentos das Vendas",
            "51.01 Vendas Anuladas",
            "51.02 Descontos Incondicionais Concedidos",
            "51.03 ICMS Sobre Vendas",
            "51.04 ISS",
            "51.05 PIS Sobre Faturamento",
            "51.06 COFINS",
            "52. Prejuízo Bruto",
            "52.01 Prejuízo sobre Vendas"
        });

            CriarPainelExpansivel(mainPanel, "Receita Bruta/Lucro Bruto", new List<string>
        {
            "60. Receita Bruta",
            "60.01 Vendas de Mercadorias",
            "60.02 Receitas de Serviços",
            "61. Deduções e Abatimentos das Compras",
            "61.01 Compras Anuladas",
            "61.02 Descontos Incondicionais Obtidos",
            "62. Lucro Bruto",
            "62.01 Lucro sobre Vendas"
        });

            CriarPainelExpansivel(mainPanel, "Apuração de Resultado", new List<string>
        {
            "70.01 Custo das Mercadorias Vendidas (CMV) e CSP Custo dos serviços prestados",
            "70.02 Lucro Bruto (LOB)",
            "70.03 Lucro Operacional",
            "71.01 Resultado do Exercício (RE)",
            "71.02 Lucro Líquido do Exercício"
        });
        }

        private void CriarPainelExpansivel(Panel mainPanel, string titulo, List<string> dados)
        {
            Panel painel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(185, 220, 200)
            };

            Button btnExpandir = new Button
            {
                Text = "˅",
                Dock = DockStyle.Left,
                Width = 30
            };
            btnExpandir.Click += (s, e) => TogglePanel(painel, btnExpandir, dados);

            Label lblTitulo = new Label
            {
                Text = titulo,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Yu Gothic UI", 9, FontStyle.Bold)
            };

            painel.Controls.Add(btnExpandir);
            painel.Controls.Add(lblTitulo);

            Panel contentPanel = new Panel
            {
                Dock = DockStyle.Left,
                Width = 0,
                Visible = false,
                Height = 300,
                BackColor = Color.White
            };

            DataGridView dataGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoGenerateColumns = false,
                ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
                RowHeadersVisible = false,
                Font = new Font("Yu Gothic UI", 9, FontStyle.Regular)
            };

            DataGridViewTextBoxColumn colunaCodigo = new DataGridViewTextBoxColumn
            {
                Name = "Codigo",
                HeaderText = "Códigos",
                DataPropertyName = "Codigo",
                Width = 250
            };
            dataGrid.Columns.Add(colunaCodigo);

            var dataSource = new BindingSource();
            var listaComLinhasExtras = dados.Select(d => new { Codigo = d }).ToList();

            while (listaComLinhasExtras.Count < 2)
            {
                listaComLinhasExtras.Add(new { Codigo = string.Empty });
            }

            dataSource.DataSource = listaComLinhasExtras;
            dataGrid.DataSource = dataSource;

            contentPanel.Controls.Add(dataGrid);
            painel.Controls.Add(contentPanel);

            mainPanel.Controls.Add(painel); // Agora adicionando ao painel principal
        }

        private void TogglePanel(Panel painel, Button btn, List<string> dados)
        {
            Panel contentPanel = painel.Controls[2] as Panel;
            Label lblTitulo = painel.Controls[1] as Label;

            if (contentPanel.Visible)
            {
                contentPanel.Visible = false;
                contentPanel.Width = 0;
                lblTitulo.Visible = true;
                btn.Text = "˅";
            }
            else
            {
                contentPanel.Visible = true;
                contentPanel.Width = 250;
                contentPanel.Height = 600;
                lblTitulo.Visible = false;
                btn.Text = "˄";
            }

            btn.BringToFront();
        }
    }

}