using iTextSharp.text;

namespace CreatePDF
{
    public static class FontesPDF
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomeArquivo">informe o nome do arquivo</param>
        /// <param name="tamanho">1- para bobina termica</param>
        /// <returns></returns>
        public static iTextSharp.text.Font FontTitulo(int tamanhoDocumento)
        {
            var titulo = FontFactory.GetFont("Arial", 18, iTextSharp.text.Font.BOLD);

            switch (tamanhoDocumento)
            {
                case 1:
                    titulo = FontFactory.GetFont("Arial", 12, iTextSharp.text.Font.BOLD);
                    break;

                default:
                    break;
            }

            return titulo;
        }
    }
}
