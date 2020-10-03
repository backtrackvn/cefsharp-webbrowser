using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UPGOPOS.src
{
    class JsHandler
    {

        public void test(dynamic data)
        {
            Console.WriteLine(">>>>>>>> ");
            Console.WriteLine(data["id"]);
            Console.WriteLine(data["salesorder_no"]);
            MessageBox.Show("TESTED");
        }
    }
}
