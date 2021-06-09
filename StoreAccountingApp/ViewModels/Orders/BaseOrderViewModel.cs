using Microsoft.Office.Interop.Word;
using StoreAccountingApp.Commands;
using StoreAccountingApp.DTO.Abstracts;
using StoreAccountingApp.GeneralClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace StoreAccountingApp.ViewModels.Orders
{
    public class BaseOrderViewModel<DTOModel> : ViewModelBase
        where DTOModel : InvoiceDTO
    {
        private InvoiceToWord<DTOModel> invoiceToPrint;
        public InvoiceToWord<DTOModel> InvoiceToPrint
        {
            get { return invoiceToPrint; }
            set { invoiceToPrint = value; }
        }

        private RelayCommand printCommand;
        public RelayCommand PrintCommand
        {
            get { return printCommand; }
        }
        public BaseOrderViewModel()
        {
            printCommand = new RelayCommand(PrintInvoice);
        }
        protected void PrintInvoice()
        {
            if (InvoiceToPrint!=null && InvoiceToPrint.CurrentInvoice != null && InvoiceToPrint.ProductsList != null)
            {
                try
                {
                    //Create an instance for word app  
                    Microsoft.Office.Interop.Word.Application winword = new Microsoft.Office.Interop.Word.Application();

                    //Set animation status for word application  
                    winword.ShowAnimation = false;

                    //Set status for word application is to be visible or not.  
                    winword.Visible = false;

                    //Create a missing variable for missing value  
                    object missing = System.Reflection.Missing.Value;

                    //Create a new document  
                    Microsoft.Office.Interop.Word.Document document = winword.Documents.Add(ref missing, ref missing, ref missing, ref missing);

                    //Add header into the document  
                    foreach (Microsoft.Office.Interop.Word.Section section in document.Sections)
                    {
                        //Get the header range and add the header details.  
                        Microsoft.Office.Interop.Word.Range headerRange = section.Headers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        headerRange.Fields.Add(headerRange, Microsoft.Office.Interop.Word.WdFieldType.wdFieldPage);
                        headerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        headerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdBlue;
                        headerRange.Font.Size = 10;
                        headerRange.Text = InvoiceToPrint.CompanyName;
                    }

                    //Add the footers into the document  
                    foreach (Microsoft.Office.Interop.Word.Section wordSection in document.Sections)
                    {
                        //Get the footer range and add the footer details.  
                        Microsoft.Office.Interop.Word.Range footerRange = wordSection.Footers[Microsoft.Office.Interop.Word.WdHeaderFooterIndex.wdHeaderFooterPrimary].Range;
                        footerRange.Font.ColorIndex = Microsoft.Office.Interop.Word.WdColorIndex.wdDarkRed;
                        footerRange.Font.Size = 10;
                        footerRange.ParagraphFormat.Alignment = Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        footerRange.Text = InvoiceToPrint?.Footer;
                    }

                    ////adding text to document  
                    //document.Content.SetRange(0, 0);
                    //document.Content.Text = "This is test document " + Environment.NewLine;

                    //Add paragraph with Heading 1 style  
                    Microsoft.Office.Interop.Word.Paragraph para1 = document.Content.Paragraphs.Add(ref missing);
                    object styleHeading1 = "Kop 1";
                    para1.Range.set_Style(ref styleHeading1);
                    para1.Range.Text = String.Format("Invoice: {0}",InvoiceToPrint.CurrentInvoice.InvoiceNumber);
                    para1.Range.InsertParagraphAfter();

                    //Add paragraph with Heading 2 style  
                    //Microsoft.Office.Interop.Word.Paragraph para2 = document.Content.Paragraphs.Add(ref missing);
                    //object styleHeading2 = "Kop 2";
                    //para2.Range.set_Style(ref styleHeading2);
                    //para2.Range.Text = "Para 2 text";
                    //para2.Range.InsertParagraphAfter();


                    //Create a 5X5 table and insert some dummy record  

                    int rows = InvoiceToPrint.ProductsList.Count();
                    Table firstTable = document.Tables.Add(para1.Range, rows, 6, ref missing, ref missing);

                    firstTable.Borders.Enable = 1;
                    var products = InvoiceToPrint.ProductsList.ToArray();
                    int iRow = 0;
                    foreach (Row row in firstTable.Rows)
                    {
                        int iCol = 0;
                        foreach (Cell cell in row.Cells)
                        {
                            //Header row  
                            if (cell.RowIndex == 1)
                            {
                                string textToPrint = String.Empty;
                                switch (iCol)
                                {
                                    case 0: textToPrint = "Product"; break;
                                    case 1: textToPrint = "UnitPrice"; break;
                                    case 2: textToPrint = "Amount"; break;
                                    case 3: textToPrint = "Netto"; break;
                                    case 4: textToPrint = "VAT"; break;
                                    case 5: textToPrint = "Total"; break;
                                    default:break;
                                }

                                cell.Range.Text = textToPrint;
                                cell.Range.Font.Bold = 1;
                                //other format properties goes here  
                                cell.Range.Font.Name = "verdana";
                                cell.Range.Font.Size = 10;
                                //cell.Range.Font.ColorIndex = WdColorIndex.wdGray25;                              
                                cell.Shading.BackgroundPatternColor = WdColor.wdColorGray25;
                                //Center alignment for the Header cells  
                                cell.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                                cell.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;

                            }
                            //Data row  
                            else
                            {
                                string textToPrint = String.Empty;
                                switch(iCol)
                                {
                                    case 0: textToPrint = InvoiceToPrint.ProductsList[iRow - 1].Key; break;
                                    case 1: textToPrint = InvoiceToPrint.ProductsList[iRow - 1].UnitPrice.ToString(); break;
                                    case 2: textToPrint = InvoiceToPrint.ProductsList[iRow - 1].Amount.ToString(); break;
                                    case 3: textToPrint = InvoiceToPrint.ProductsList[iRow - 1].Netto.ToString(); break;
                                    case 4: textToPrint = InvoiceToPrint.ProductsList[iRow - 1].VAT.ToString(); break;
                                    case 5: textToPrint = InvoiceToPrint.ProductsList[iRow - 1].Total.ToString(); break;
                                    default: break;
                                }
                                cell.Range.Text = textToPrint;
                            }
                            iCol++;
                        }
                        iRow++;
                    }

                    //Save the document  
                    var invoice = Regex.Replace(InvoiceToPrint.CurrentInvoice.InvoiceNumber, "[^0-9a-zA-Z]+", "_"); 
                    object filename = String.Format(@"Y:\tempInvoices\invoice_{0}.docx",invoice);
                    document.SaveAs2(ref filename);
                    document.Close(ref missing, ref missing, ref missing);
                    document = null;
                    winword.Quit(ref missing, ref missing, ref missing);
                    winword = null;
                    MessageBox.Show("Document created successfully !");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }
    }
}
