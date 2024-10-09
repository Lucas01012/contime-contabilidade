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
        public static readonly string TableHeaderFColor = Preto; 
        public static readonly string HeaderFColor = Branco;
        public static readonly string TableHeaderBGColor = VerdeClaro2;

        #region Colors List
        private const string CinzaClaro = "#dce8dc";
        private const string VerdeClaro1 = "#e7f7e8";
        private const string VerdeClaro2 = "#b9dcc9";
        private const string VerdeMarinho = "#048474";
        private const string VerdeEscuro = "#154219";
        private const string Verde = "#36a93f";
        private const string Branco = "#ffffff";
        private const string Preto = "#000000";
        #endregion

        public static IContainer TableHeader(IContainer container)
        {
            return container
                .Border(1)
                .BorderColor(Branco)
                .Background(TableHeaderBGColor);
        }
        public static IContainer Header(IContainer container)
        {
            return container
                .Border(1)
                .BorderColor(Branco)
                .Background(Verde);
        }
        public static IContainer Cell(IContainer container)
        {
            return container
                .Border(1)
                .BorderColor("#000000")
                .Background("#ffffff");
        }
    }
}
