using iTextSharp.text;

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

    }
}
