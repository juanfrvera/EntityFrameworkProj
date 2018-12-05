using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManager.Domain
{
    public class Document
    {
        #region Declarations
        public enum DocumentType{DNI,CUIL,LE,LC}
        #endregion
        private String number;

        public String Number
        {
            get { return number; }
            set { number = value; }
        }
    }
}