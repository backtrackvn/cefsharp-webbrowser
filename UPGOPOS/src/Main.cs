using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

using CefSharp;
using CefSharp.WinForms;

using UPGOPOS.src;
using System.Threading;
using BarcodeLib;
using System.Drawing.Imaging;

namespace UPGOPOS
{
    public partial class Main : Form
    {
        public ChromiumWebBrowser browser;
        public Main()
        {
            generateBarcode("1837463528");
            InitializeComponent();
            InitBrowser();
            String name = "Nestcafe hoa tan 3in1";
            String product = name.Substring(0, 13);
            String productName = "";
            
            for (int i = 0; i < name.Length; i++)
            {
                if (name[13] == ' ')
                {
                    if (i <= 12)
                    {
                        productName = productName + name[i];
                    }

                }
                else
                {
                    int idx = product.LastIndexOf(' ');
                    if (idx != -1)
                    {
                        productName = product.Substring(0, idx);
                    }
                    break;
                }

            }
            Console.WriteLine("===" + productName);

            String price = "324342";
            String quantity = "100";
           
            InitDisplayCustomer(productName, price, quantity);
            
        }
        public void generateBarcode(string numberOrder)
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
            barcodeImage.Save(@"C:\Users\Public\Downloads\isbn_example.png", ImageFormat.Png);
            Console.WriteLine("ok");
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Name = "Main";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.ResumeLayout(false);

        }

        public void Main_Load(object sender, EventArgs e)
        {

        }

        public void InitBrowser()
        {
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            browser = new ChromiumWebBrowser("http://localhost:8888");
            // add browser to form
            this.Controls.Add(browser);
            // Make the browser fill the form
            browser.Dock = DockStyle.Fill;
            browser.LoadingStateChanged += OnLoadingStateChanged;

            browser.RegisterJsObject("jsHandler", new JsHandler());
        }
        public void InitDisplayCustomer(String product,String price,String quantity)
        {
            SerialPort sp = new SerialPort();

            sp.PortName = "COM3";
            sp.BaudRate = 9600;
            sp.Parity = Parity.None;
            sp.DataBits = 8;
            sp.StopBits = StopBits.One;
            sp.Open();
           
            System.Globalization.CultureInfo cul = System.Globalization.CultureInfo.GetCultureInfo("vi-VN");   // try with "en-US"
            string a = double.Parse(price).ToString("#,###", cul.NumberFormat);
            string str = new string(' ', 10 - a.Length);
       
            string q = new string(' ', 4 - quantity.Length);
      

            sp.Write(Convert.ToString((char)12));
            sp.WriteLine("x"+ quantity+q+ " "+ product);

            sp.WriteLine((char)13 + "Tong" + "   " + str + a + "VND" + "\n");
            
           
            sp.Close();
            sp.Dispose();
            sp = null;
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            
            if (e.CloseReason == CloseReason.WindowsShutDown)
            {
                //do not show the dialog
                Cef.Shutdown();
            }
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            if (!args.IsLoading)
            {
                // Page has finished loading, do whatever you want here
                browser.ShowDevTools();
            }
        }

    }
}
