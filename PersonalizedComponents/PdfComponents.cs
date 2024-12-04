using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace ConTime.PersonalizedComponents
{
    public static class PdfComponents
    {
        // Definições das cores em formato hexadecimal válido
        public static readonly string TableHeaderFColor = "#000000"; // Preto
        public static readonly string HeaderFColor = "#FFFFFF"; // Branco
        public static readonly string TableHeaderBGColor = "#b9dcc9"; // Verde Claro
        public static readonly string Branco = "#FFFFFF"; // Cor branca em hexadecimal
        public static readonly string Preto = "#000000"; // Cor preta em hexadecimal

        #region Colors List
        private const string CinzaClaro = "#dce8dc";
        private const string VerdeClaro1 = "#e7f7e8";
        private const string VerdeClaro2 = "#b9dcc9"; // Verde Claro
        private const string VerdeMarinho = "#048474";
        private const string VerdeEscuro = "#154219";
        private const string Verde = "#36a93f";
        #endregion

        public static IContainer TableHeader(IContainer container)
        {
            // Removido o fundo, agora será transparente
            return container
                .Border(1)
                .BorderColor(Preto) // Cor da borda agora é o preto
                .Background(Verde);  // Sem cor de fundo
        }

        public static IContainer Header(IContainer container)
        {
            // Cabeçalho sem cor de fundo, e agora usando a cor hexadecimal correta para a borda
            return container
                .Border(1)
                .BorderColor(Preto) // Cor da borda preta
                .Background(Verde); // Remover o fundo, definindo como null (transparente)
        }

        public static IContainer Cell(IContainer container)
        {
            // Células com borda preta e sem fundo
            return container
                .Border(1)
                .BorderColor(Preto) // Cor da borda preta
                .Background(Verde); // Sem fundo
        }
    }
}