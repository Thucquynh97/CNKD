using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiecnonkidieu
{
   public class Chuanhoachuoi
    {
        public string btchuanhoa(string x)
        {
            string name = x;
            char[] arrchar = { ' ', '\n', '\t' };
            String[] arr = name.Split(arrchar, StringSplitOptions.RemoveEmptyEntries);
            name = "";
            for (int i = 0; i < arr.Length; i++)
            {
                String a = arr[i].ToLower();
                name += a.Substring(0, 1).ToUpper() + a.Substring(1) + " ";
            }

            return name.TrimEnd();
        }
    }
}
