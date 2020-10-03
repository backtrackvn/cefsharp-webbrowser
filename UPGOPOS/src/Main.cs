using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.WinForms;

using UPGOPOS.src;

namespace UPGOPOS
{
    public partial class Main : Form
    {
        public ChromiumWebBrowser browser;
        public Main()
        {
            InitializeComponent();
            InitBrowser();
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

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            if (!args.IsLoading)
            {
                // Page has finished loading, do whatever you want here
                // browser.ShowDevTools();
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
