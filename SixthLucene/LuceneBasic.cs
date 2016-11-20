using SixthDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthLucene
{
    public abstract class LuceneBasic : IDisposable
    {
        protected BasicServci EfService;

        public LuceneBasic(BasicServci _BasicServcie)
        {
            this.EfService = _BasicServcie;
        }

        public void Dispose()
        {
        }
    }
}