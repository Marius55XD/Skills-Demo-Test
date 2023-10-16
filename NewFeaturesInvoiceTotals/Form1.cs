using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace NewFeaturesInvoiceTotals
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int numberOfInvoices = 0;
        decimal totalInvoices = 0m;
        decimal invoicesAverage = 0m;

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void lblEnterSubtotal_Click(object sender, EventArgs e)
        {

        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
             decimal subtotal = 
                Convert.ToDecimal(txtEnterSubtotal.Text);
            decimal discountPercent = .25m;
            decimal discountAmount = Math.Round(subtotal * discountPercent, 2);

            decimal invoiceTotal = subtotal - discountAmount;

            txtSubtotal.Text = subtotal.ToString("c");
            txtDiscountPercent.Text = discountPercent.ToString("p1");
            txtDiscountAmount.Text = discountAmount.ToString("c");
            txtTotal.Text = invoiceTotal.ToString("c");

            numberOfInvoices++;
            totalInvoices += invoiceTotal;
            invoicesAverage = totalInvoices / numberOfInvoices;

            txtNumberOfInvoices.Text = numberOfInvoices.ToString();
            txtTotalOfInvoices.Text = totalInvoices.ToString("c");
            txtInvoiceAverage.Text = invoicesAverage.ToString("c");

            txtEnterSubtotal.Text = "";
            txtEnterSubtotal.Focus();
        }

        private void btnClearTotal_Click(object sender, EventArgs e)
        {
            numberOfInvoices = 0;
            totalInvoices = 0m;
            invoicesAverage = 0m;


            txtNumberOfInvoices.Text = "";
            txtTotalOfInvoices.Text = "";
            txtInvoiceAverage.Text = "";

            txtEnterSubtotal.Focus();
        }

        private void bntExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Implement code to save the current invoice details to a file or database.
            //You can use various methods depending on your storage preferences.

            //For Example, to save to a text file:
            string InvoiceDetails = $"{txtSubtotal.Text},{txtDiscountPercent.Text}, {txtDiscountAmount.Text}, {txtTotal.Text}";
            System.IO.File.AppendAllText("invoice.txt", InvoiceDetails + Environment.NewLine);

            //Optionlly, provide feedback to the user upon successful save.
            MessageBox.Show("Invoice details saved successfully.");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //Create a PrintDocument object.
            PrintDocument printDoc = new PrintDocument();


            //Set the event handler for printing.
            printDoc.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            //Display the PrintDialog to chose a printer.
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDoc;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {

            string InvoiceDetails = $"Subtotal: {txtSubtotal.Text}\n" +
                                    $"Discount Percent:{txtDiscountPercent.Text}\n" +
                                    $"Discount Amount:{txtDiscountAmount.Text}\n" +
                                    $"Total: {txtTotal.Text}\n";

            Font printFont = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);

            RectangleF printArea = e.MarginBounds;

            e.Graphics.DrawString(InvoiceDetails, printFont, brush, printArea);

            e.HasMorePages = false;


        }
    }
}
