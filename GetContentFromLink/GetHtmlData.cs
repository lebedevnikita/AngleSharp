using System;


using System.Text;

using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Data;
using Microsoft.SqlServer.Server;
using System.Data.SqlTypes;
using System.Collections;

namespace GetContentFromLink
{
    public class GetHtmlData
    {

       [Microsoft.SqlServer.Server.SqlFunction(FillRowMethodName = "FillRow", TableDefinition = "part NVARCHAR(MAX)")]
        public static IEnumerator GetHtml(string _link)
        // public static void Main(string _HTTP, string _Selector) 
        {
            string text = "";
 
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(_link);
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            Stream stream = resp.GetResponseStream();

            var sr = new StreamReader(req.GetResponse().GetResponseStream(), Encoding.UTF8); //Encoding.UTF8, Encoding.Unicode, Encoding.ASCII .... 
            text = sr.ReadToEnd();

            yield return text;
            //return _return_string;

        }
        public static void FillRow(Object obj, out string stringElement)
        {
            stringElement = obj.ToString();//Возвращает в таблицу строку
        }



        [Microsoft.SqlServer.Server.SqlFunction(FillRowMethodName = "FillRow2", TableDefinition = "part NVARCHAR(MAX)")]
        public static IEnumerator TextContent(string _text, string _Selector)
        // public static void Main(string _HTTP, string _Selector) 
        {           
            string _return_string = "";


            var parser = new AngleSharp.Parser.Html.HtmlParser();
            var document = parser.Parse(_text);
            //var blueListItemsCSS = document.QuerySelectorAll("p");
            var blueListItemsCSS = document.QuerySelectorAll(_Selector);
            //var blueListItemsCSS = document.QuerySelectorAll("p").Where(item => item.ClassName != null && item.ClassName.Contains("post__title_link"));



            foreach (var item in blueListItemsCSS)
            {
                if (item.TextContent != null) { _return_string = _return_string + item.TextContent; }
            }

            yield return _return_string;
            //return _return_string;







        }



        public static void FillRow2(Object obj, out string stringElement)
        {
            stringElement = obj.ToString();//Возвращает в таблицу строку
        }



        [Microsoft.SqlServer.Server.SqlFunction(FillRowMethodName = "FillRow3", TableDefinition = "part NVARCHAR(MAX)")]
        public static IEnumerator GetAttribute(string _text, string _Selector, string _Attribute)
        // public static void Main(string _HTTP, string _Selector) 
        {
           
            var parser = new AngleSharp.Parser.Html.HtmlParser();
            var document = parser.Parse(_text);
            var blueListItemsCSS = document.QuerySelectorAll(_Selector);
           
            foreach (var item in blueListItemsCSS)
            {

                if (item.GetAttribute(_Attribute) != null) { yield return item.GetAttribute("href"); }

            }


        }



        public static void FillRow3(Object obj, out string stringElement)
        {
            stringElement = obj.ToString();//Возвращает в таблицу строку
        }



    }















}












