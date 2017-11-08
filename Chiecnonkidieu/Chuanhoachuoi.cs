using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiecnonkidieu
{
    class Chuanhoachuoi
    {
        public void btchuanhoa(string name)
        {
            char[] arrchar = { ' ', '\n', '\t' };
            String[] arr = name.Split(arrchar, StringSplitOptions.RemoveEmptyEntries);
            name = "";
            for (int i = 0; i < arr.Length; i++)
            {
                String a = arr[i].ToLower();
                name += a.Substring(0, 1).ToUpper() + a.Substring(1) + " ";
                name.TrimEnd();
            }

        }
    }
}
