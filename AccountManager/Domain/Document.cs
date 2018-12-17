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
        public enum DocumentType{DNI ,CUIL, LE,LC}
        #endregion
        private DocumentType type;
        private String number;

        public DocumentType Type
        {
            get { return type;  }
            private set { type = value; }
        }
        public String Number
        {
            get { return number; }
            private set { number = value; }
        }

        //Constructor
        public Document(string pType, string pNumber)
        {
            this.Type = (DocumentType)Enum.Parse(typeof(DocumentType),pType);
            this.Number = pNumber;
        }
    }
}