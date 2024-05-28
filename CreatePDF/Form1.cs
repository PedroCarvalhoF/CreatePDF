using iTextSharp.text;

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

            using (PDFRepository pdf = new PDFRepository("relatorio.pdf", 1))
            {
                pdf.AdicinarTitulo("Easy Cashier");

                pdf_criado = pdf.GetDocumentPdf();
            }
        }
    }
}
