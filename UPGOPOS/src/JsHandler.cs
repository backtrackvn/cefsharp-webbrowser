using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;
using BarcodeLib;
using UPGOPOS.src.models;
using System.Drawing;
using System.Drawing.Imaging;

namespace UPGOPOS.src
{
    class JsHandler
    {
        static string templateDir = ConfigurationManager.AppSettings["template_dir"];

      /* public void generateBarcode(string numberOrder)
        {
            Barcode barcodeAPI = new Barcode();

            // Define basic settings of the image
            int imageWidth = 290;
            int imageHeight = 120;
            Color foreColor = Color.Black;
            Color backColor = Color.Transparent;
           

            // Generate the barcode with your settings
            Image barcodeImage = barcodeAPI.Encode(TYPE.ISBN, numberOrder, foreColor, backColor, imageWidth, imageHeight);

            // Store image in some path with the desired format
           // barcodeImage.Save(@"C:\Users\sdkca\Desktop\isbn_example.png", ImageFormat.Png);

        }
      */

        public void printReturnOrder(dynamic returnorder)
        {
            MessageBox.Show(templateDir);
            string id = returnorder["id"];
            string salesorderNo = "";
            if (returnorder["salesorder_no"] != null)
            {
                salesorderNo = returnorder["salesorder_no"];
            }
            string barcode = "";
            if (returnorder["returnorder_no"] != null)
            {
                barcode = '*'+returnorder["returnorder_no"] +'*';

            }
           
            string returnorderNo = "";
            if (returnorder["returnorder_no"] != null)
            {
                returnorderNo = returnorder["returnorder_no"];

            }
            string orderTime = "";
            if (returnorder["order_datetime"] != null)
            {
                orderTime = returnorder["order_datetime"];
            }
          /*  float netAmount = returnorder["net_amount"];
            float amount = returnorder["amount"];
            float discount = returnorder["discount"];*/
            float rePayAmount = returnorder["repay_amount"];
            string contactName = "";
            if (returnorder["contact_name"] != null)
            {
                contactName = returnorder["contact_name"];

            }
            string contactPhone = "";
            if (returnorder["contact_phone"] != null)
            {
                contactPhone = returnorder["contact_phone"];
            }
            string cashier = returnorder["created_by_name"];
            List<ReturnOrderItem> items = new List<ReturnOrderItem>();

            foreach (var element in returnorder["items"])
            {
                ReturnOrderItem item = new ReturnOrderItem();
                item.id = element["id"];
                item.item_id = element["item_id"];
                item.item_name = element["item_name"];
                item.list_price = element["list_price"];
                item.quantity = element["quantity"];
                item.net_amount = element["net_amount"];
               // item.amount = element["amount"];
                //item.discount = element["discount"];
                
                items.Add(item);
            }

            var cr = new ReportDocument();
            cr.Load(templateDir + "\\ReturnOrderTemplate.rpt");

            cr.SetDataSource(items);
            cr.SetParameterValue("order_time", orderTime);
            cr.SetParameterValue("saleorder_no", salesorderNo);
            cr.SetParameterValue("return_order_no", returnorderNo);
            cr.SetParameterValue("cashier", cashier);
            cr.SetParameterValue("repay_amount", rePayAmount);
            cr.SetParameterValue("contact_name", contactName);
            cr.SetParameterValue("contact_phone", contactPhone);
            cr.SetParameterValue("barcode", barcode);
            cr.SetParameterValue("workstation_name", "LalaMart Văn Phú");
            cr.SetParameterValue("address", "09LK13 KĐT mới Văn Phú, Phường Phú La, Hà Đông");
            cr.SetParameterValue("hotline", "1900112233");

            System.Drawing.Printing.PrinterSettings printSetings = new System.Drawing.Printing.PrinterSettings();
            System.Drawing.Printing.PageSettings pageSetings = new System.Drawing.Printing.PageSettings();
            //  Console.WriteLine("====================Printer_Name: " + salesorder["printer"]["name"]);
            printSetings.PrinterName = "SLK-TS100";
            cr.PrintToPrinter(printSetings, pageSetings, false);
            //cr.Refresh();
            cr.Clone();
            cr.Dispose();

        }




            public void printSalesorder(dynamic salesorder)
        {
            MessageBox.Show(templateDir);
            string id = salesorder["id"];
            string salesorderNo = "";
            if (salesorder["salesorder_no"] != null)
            {
                salesorderNo = salesorder["salesorder_no"];
            }
            string barcode = "";
            if (salesorder["salesorder_no"] != null)
            {
                barcode = '*' + salesorder["salesorder_no"] + '*';

            }
            string orderTime = "";
            if (salesorder["order_time"] != null)
            {
                orderTime = salesorder["order_time"];
            }
           
            /*
            int orderYear = salesorder["order_year"];
            int orderMonth = salesorder["order_month"];
            int orderDay = salesorder["order_day"];
            int orderHour = salesorder["order_hour"];
            int orderMinute = salesorder["order_minute"];
            */
            float netAmount = salesorder["net_amount"];
            float amount = salesorder["amount"];
            //float voucherDiscount = salesorder["voucher_discount_amount"];
            //float itemDiscount = salesorder["item_discount_amount"];
            //float discountAmount = salesorder["discount_amount"];
            float totalDiscountAmount = salesorder["total_discount_amount"];
            //float totalDiscountPercent = salesorder["total_discount_percent"];
            //float taxAmount = salesorder["tax_amount"];
            //float shipAmount = salesorder["ship_amount"];
            string cashier = salesorder["created_by_name"];

            string contactId = "";
            if (salesorder["contact_id"] != null)
            {
                contactId = salesorder["contact_id"];
            }
            string contactName = "";
            if (salesorder["contact_name"] != null)
            {
                contactName = salesorder["contact_name"];
            }

            List<SalesorderItem> items = new List<SalesorderItem>();

            foreach (var element in salesorder["items"])
            {
                SalesorderItem item = new SalesorderItem();
                item.id = element["id"];
                item.item_id = element["item_id"];
                item.item_name = element["item_name"];
                item.list_price = element["list_price"];
                item.quantity = element["quantity"];
                item.net_amount = element["net_amount"];
                item.amount = element["amount"];
                item.discount = element["discount"];
                item.tax_amount = element["tax_amount"];
                item.tax_percent = element["tax_percent"];
                items.Add(item);
            }

            List<models.PaymentMethod> payments = new List<models.PaymentMethod>();
            foreach (var element in salesorder["payment_info"])
            {
                models.PaymentMethod pay = new models.PaymentMethod();
                pay.method = element["method"];
                pay.name = element["name"];
                pay.amount = element["amount"];
                pay.currency_id = element["currency_id"];
                payments.Add(pay);
            }

            var cr = new ReportDocument();
            cr.Load(templateDir + "\\SalesorderTemplate.rpt");

            cr.SetDataSource(items);
            cr.SetParameterValue("order_time", orderTime);
            cr.SetParameterValue("saleorder_no", salesorderNo);
            cr.SetParameterValue("net_amount", netAmount);
            cr.SetParameterValue("total_discount_amount", totalDiscountAmount);
            cr.SetParameterValue("amount", amount);
            cr.SetParameterValue("cashier", cashier);
            cr.SetParameterValue("contact_name", contactName);
            cr.SetParameterValue("barcode", barcode);
            cr.SetParameterValue("workstation_name", "LalaMart Văn Phú");
            cr.SetParameterValue("address", "09LK13 KĐT mới Văn Phú, Phường Phú La, Hà Đông");
            cr.SetParameterValue("hotline", "1900112233");

            System.Drawing.Printing.PrinterSettings printSetings = new System.Drawing.Printing.PrinterSettings();
            System.Drawing.Printing.PageSettings pageSetings = new System.Drawing.Printing.PageSettings();
            //  Console.WriteLine("====================Printer_Name: " + salesorder["printer"]["name"]);
            printSetings.PrinterName = "SLK-TS100";
            cr.PrintToPrinter(printSetings, pageSetings, false);
            //cr.Refresh();
            cr.Clone();
            cr.Dispose();
        }
    }
}
