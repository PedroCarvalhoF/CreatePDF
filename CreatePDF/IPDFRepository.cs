using iTextSharp.text;
using iTextSharp.text.pdf;

namespace CreatePDF
{
    public interface IPDFRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="nomeArquivo">informe o nome do arquivo</param>
        /// <param name="tamanho">1- para bobina termica</param>
        /// <returns></returns>
        Document CriarPDF(string nomeArquivo, int tamanho);
        Document GetDocumentPdf();

        //escritas
        void AdicinarTitulo(string titulo, iTextSharp.text.Font font = null);
        PdfPTable CriarTabela(int qtd_colunas);
        PdfPCell CriarCelulaTabela(string textoCelula, int horizontalAlignment = 0, int verticalAlingnment = 1, iTextSharp.text.Font fonteCelula = null);
        PdfPTable AdicinarCelulaTabela(PdfPCell celula, PdfPTable tabela);

        void DivPDF();
    }
}
