using SixthDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthLucene
{
    public class LuceneService : LuceneBasic
    {
        public LuceneService(BasicServci _BasicServci, Type _Type) : base(_BasicServci)
        {
            this.JdType = _Type;
            count = GetCount(JdType);
            Page = GetPage();
        }

        protected object JdClass { get; set; }
        protected Type JdType { get; set; }
        protected int count;
        protected int PageSize { get { return 1000; } }
        private int Page { get; set; }

        public void Run()
        {
            TaskFactory taskFactory = new TaskFactory();
            //taskFactory.StartNew()
            for (int i = 1; i < Page; i++)
            {
                GetSkipPage<JdModel>(JdType, PageSize, i);
            }
        }

        private void BuildLucene(IEnumerable<JdModel> list)
        {
            ParallelLoopResult plResult = new ParallelLoopResult();

            Parallel.ForEach(list, p => p.Id = 2);
            ParallelOptions po = new ParallelOptions();
        }

        public virtual void GetData<TResult>(Type t) where TResult : class
        {
            var s = GetRead<TResult>(t);
            var inss = GetCount(t);
            // var s = this.EfService.Getdb(JdClass);
            //var ts = s.ToList();
            //GetPageData(1);
        }

        private void ReadyData()
        {
            // this.EfService.SkipTable(JdClass)
        }

        private int GetPage()
        {
            double result = Math.Ceiling(count / 1000.0);
            return Convert.ToInt32(result);
        }

        private IEnumerable<TResult> GetRead<TResult>(Type type)
        {
            return this.EfService.readTest<TResult>(type);
        }

        private int GetCount(Type type)
        {
            return this.EfService.GetCount(type);
        }

        private IEnumerable<TResult> GetSkipPage<TResult>(Type type, int pageSize, int pageIndex)
        {
            return this.EfService.SkipData<TResult>(type, pageSize, pageIndex);
        }

        //private IList<object> GetPageData(int index)
        //{
        //    //  return this.EfService.SkipTable(JdClass, pageSize, index);
        //}
    }
}