using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.draw;

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
            //futuramente teremos outros padroes de tamanha , como por exemplo A4, etc.
            //criarei enum
            tamanho_documento = tamanho;

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
            
            float larguraBobinaMm = 80; // Largura e altura da bobina em milimetros
            float alturaBobinaMm = 297; // altura pode ser ajustaada conforme necessario
            float margemMm = 5; // margem em milimetros

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


        public PdfPTable CriarTabela(int qtd_colunas)
        {
            // Cria uma tabela com o número de colunas especificado
            PdfPTable tabela = new PdfPTable(qtd_colunas);

            // Define a largura da tabela como 100% da largura da página
            tabela.WidthPercentage = 100;

            // Definindo margens entre as células
            tabela.SpacingBefore = 5f; // Espaço antes da tabela
            tabela.SpacingAfter = 5f; // Espaço após a tabela

            return tabela;
        }

        /// <summary>
        /// ALIGN_CENTER = 1;
        /// ALIGN_LEFT = 0;
        /// ALIGN_RIGHT = 2;
        /// ALIGN_JUSTIFIED_ALL = 8
        /// 
        /// </summary>
        /// <param name="horizontalAlignment"></param>
        /// <param name="verticalAlingnment"></param>
        /// <param name="fonteCelula">campo obrigatorio</param>

        public PdfPCell CriarCelulaTabela(string textoCelula, int horizontalAlignment = 0, int verticalAlingnment = 1, iTextSharp.text.Font fonteCelula = null)
        {
            if (fonteCelula == null)
                fonteCelula = FontesPDF.FontCelulaTabela(tamanho_documento);

            PdfPCell celula = new PdfPCell(new Phrase(textoCelula, fonteCelula));
            celula.HorizontalAlignment = horizontalAlignment;
            celula.VerticalAlignment = verticalAlingnment;

            celula.BorderWidth = 0.2f;

            celula.BorderColor = iTextSharp.text.BaseColor.Black;
            celula.FixedHeight = 15;

            return celula;
        }
        public PdfPTable AdicinarCelulaTabela(PdfPCell celula, PdfPTable tabela)
        {
            tabela.AddCell(celula);
            AtualizarTamanhoColunas(tabela);
            return tabela;
        }
        private void AtualizarTamanhoColunas(PdfPTable tabela)
        {
            // Percorrer todas as células da tabela para determinar a maior largura em cada coluna
            float[] largurasColunas = new float[tabela.NumberOfColumns];
            foreach (PdfPRow linha in tabela.Rows)
            {
                for (int i = 0; i < linha.GetCells().Length; i++)
                {
                    PdfPCell celula = linha.GetCells()[i];
                    float larguraCelula = GetLarguraCelula(celula); // Obter a largura da célula
                    if (larguraCelula > largurasColunas[i])
                    {
                        largurasColunas[i] = larguraCelula;
                    }
                }
            }

            // Definir a largura de cada coluna de acordo com a maior largura em cada coluna
            tabela.SetWidths(largurasColunas);
        }
        private float GetLarguraCelula(PdfPCell celula)
        {
            // Calcular a largura do conteúdo da célula
            Phrase conteudo = celula.Phrase;
            if (conteudo == null)
                return 0;

            float larguraConteudo = 0;
            foreach (Chunk chunk in conteudo.Chunks)
            {
                BaseFont fonte = chunk.Font.GetCalculatedBaseFont(false);
                larguraConteudo += fonte.GetWidthPoint(chunk.Content, chunk.Font.Size);
            }

            // Adicionar padding
            larguraConteudo += celula.PaddingLeft + celula.PaddingRight;

            return larguraConteudo;
        }
        public void AdicionarTabelaPDF(PdfPTable tabela)
        {
            if (!_pdf.IsOpen())
                _pdf.Open();
            _pdf.Add(tabela);
        }


        public void DivPDF()
        {
            if (!_pdf.IsOpen())
                _pdf.Open();
            LineSeparator linha = new LineSeparator(1f, 100f, BaseColor.Black, Element.ALIGN_CENTER, -1);
            _pdf.Add(linha);
        }
    }
}
