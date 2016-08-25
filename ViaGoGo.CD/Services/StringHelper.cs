using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViaGoGo.CD.Services
{
    public class StringHelper
    {

        public string substringLastChar(string str, string x)
        {
            int indexOfstring = str.LastIndexOf(x);
            indexOfstring++;
            string retStr = str.Substring(indexOfstring, str.Length - indexOfstring);

            return retStr;
        }
    }
}