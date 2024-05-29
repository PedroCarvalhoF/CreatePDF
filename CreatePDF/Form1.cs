using iTextSharp.text;
using System.Diagnostics;

namespace CreatePDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Document pdf_criado;
            string nome_arquivo = "relatorio.pdf";
            nome_arquivo = DateTime.Now.ToString(format: "ddMMyyyyffff_") + nome_arquivo;

            string pathStartup = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(pathStartup, nome_arquivo);


            using (PDFRepository pdf = new PDFRepository(nome_arquivo, 1))
            {
                pdf.AdicinarTitulo("Easy Cashier");

                var tabela = pdf.CriarTabela(2);// cria a tabela
                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela("Abertura:"), tabela);// com retorno da celula criada ja adiciona a mesma na tebela passada no parametro
                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela(DateTime.Now.ToString(), horizontalAlignment: 2), tabela);

                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela("Encerramento:"), tabela);
                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela(DateTime.Now.AddHours(6).ToString(), horizontalAlignment: 2), tabela);

                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela("Situação:"), tabela);
                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela("Fechado", horizontalAlignment: 2), tabela);

                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela("Responsável Abertura:"), tabela);
                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela("Pedro Adolfo", horizontalAlignment: 2), tabela);

                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela("Responsável Operador:"), tabela);
                pdf.AdicinarCelulaTabela(pdf.CriarCelulaTabela("Pedro Adolfo", horizontalAlignment: 2), tabela);

                pdf.AdicionarTabelaPDF(tabela);

                pdf.DivPDF();



                pdf_criado = pdf.GetDocumentPdf();
            }

            using (Process process = new Process())
            {
                process.StartInfo.FileName = filePath;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
        }
    }
}
