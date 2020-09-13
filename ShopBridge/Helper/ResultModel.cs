using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridge.Helper
{
    public class ResultModel : IDisposable
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public string Msg { get; set; }
        public Object Data { get; set; }
        public string ErrorMsg { get; set; }
        public int StatusCode { get; set; }

        public void Dispose()
        {

        }
    }


    public class ReturnMessageModel
    {
        public string Id { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public string Msg { get; set; }
        public string ErrorMsg { get; set; }
    }
}