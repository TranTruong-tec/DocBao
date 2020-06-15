using System;
using System.Collections.Generic;
using WebDocBao.Models;

namespace WebDocBao.Controllers
{
    internal class DocBaoEntitles1
    {
        public object Loaibaiviets { get; internal set; }
        public object LoaiBaiViets { get; internal set; }
        public IEnumerable<object> BaiViets { get; internal set; }
        public object SaveChange { get; internal set; }

        public static implicit operator DocBaoEntitles1(DocBaoEntities1 v)
        {
            throw new NotImplementedException();
        }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }

        internal void SaveChange()
        {
            throw new NotImplementedException();
        }
    }
}