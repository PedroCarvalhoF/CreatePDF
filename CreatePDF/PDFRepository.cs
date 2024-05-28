using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CreatePDF
{
    public class PDFRepository : IPDFRepository, IDisposable
    {
        int tamanho_documento = 1;
        private Document _pdf;
        private static PdfWriter _escritor;
        private FileStream arquivo;
        #region Dispose
        public void Dispose()
        {
            _pdf.Close();
            arquivo.Close();
            _escritor.Close();
        }
        #endregion
        #region PDF       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomeArquivo">informe o nome do arquivo</param>
        /// <param name="tamanho">1- para bobina termica</param>
        /// <returns></returns>
        public PDFRepository(string nomeArquivo, int tamanho)
        {
            tamanho_documento = tamanho;
            nomeArquivo = DateTime.Now.ToString(format: "ddMMyyyyffff_") + nomeArquivo;
            CriarPDF(nomeArquivo, tamanho);
        }
        public Document CriarPDF(string nomeArquivo, int tamanho)
        {
            switch (tamanho)
            {
                case 1:
                    return GerarPDF_BobinaTermica(nomeArquivo);

                default:
                    return null;
            }
        }
        public Document GetDocumentPdf()
        {
            return _pdf;
        }

        private Document GerarPDF_BobinaTermica(string nomeArquivo)
        {
            // Largura e altura da bobina em milímetros
            float larguraBobinaMm = 80;
            float alturaBobinaMm = 297; // altura pode ser ajustada conforme necessário
            float margemMm = 5; // margem em milímetros

            // Convertendo milímetros para pontos
            float larguraBobinaPts = larguraBobinaMm * 72 / 25.4F;
            float alturaBobinaPts = alturaBobinaMm * 72 / 25.4F;
            float margemPts = margemMm * 72 / 25.4F;

            // Criar uma nova página com o tamanho da bobina e margens
            _pdf = new Document(new iTextSharp.text.Rectangle(larguraBobinaPts, alturaBobinaPts), margemPts, margemPts, margemPts, margemPts);

            // Nome do arquivo
            arquivo = new FileStream(nomeArquivo, FileMode.Create);

            // Associar arquivo com o pdf
            _escritor = PdfWriter.GetInstance(_pdf, arquivo);
            return _pdf;
        }

        #endregion


        #region Escritas

        #endregion
        public void AdicinarTitulo(string titulo, iTextSharp.text.Font font = null)
        {
            if (font == null)
            {
                font = FontesPDF.FontTitulo(tamanho_documento);
            }

            Paragraph paragrafo = new Paragraph(titulo, font)
            {
                Alignment = Element.ALIGN_CENTER,
                SpacingAfter = 20
            };


            if (!_pdf.IsOpen())
                _pdf.Open();
            _pdf.Add(paragrafo);
        }
    }
}
