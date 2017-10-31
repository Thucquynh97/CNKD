using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chiecnonkidieu
{
   public class Question
    {
        int id;
        string cauhoi;
        string cautraloi;
        string giaithich;
        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public string CauHoi
        {
            get { return cauhoi; }
            set { cauhoi = value; }
        }
        public string CauTraLoi
        {
            get { return cautraloi; }
            set { cautraloi = value; }
        }
        public string GiaiThich
        {
            get { return giaithich; }
            set { giaithich = value; }
        }
        public Question(int id, string cauhoi, string cautraloi, string giaithich)
        {
            this.id = id;
            this.cauhoi = cauhoi;
            this.cautraloi = cautraloi;
            this.giaithich = giaithich;
        }

    }
}
