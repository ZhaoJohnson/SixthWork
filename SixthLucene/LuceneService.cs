using Ruanmou.LuceneProject.Interface;
using Ruanmou.LuceneProject.Model;
using Ruanmou.LuceneProject.Service;
using SixthDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
            //BuildTools = new LuceneBuild();
        }

        protected ILuceneBulid BuildTools { get; set; }
        protected Type JdType { get; set; }
        protected int count;
        protected int PageSize { get { return 1000; } }
        private int Page { get; set; }
        private static CancellationTokenSource CTS = null;

        public void Run()
        {
            List<Task> taskList = new List<Task>();
            TaskFactory taskFactory = new TaskFactory();
            CTS = new CancellationTokenSource();
            bool isfirst = true;
            ParallelLoopResult pl = new ParallelLoopResult();
            for (int i = 1; i < Page; i++)
            {
                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism = 20;
                var list = GetSkipPage<Commodity>(JdType, PageSize, i);
                ILuceneBulid builder = new LuceneBulid();
                var loopresult = Parallel.ForEach(list, po, p =>
                {
                    builder.BuildIndex(list.ToList(), i.ToString("000"), isfirst);
                });
                //builder.BuildIndex(list.ToList(), i.ToString("000"), isfirst);
            }
            while (pl.IsCompleted)
            {
                Console.WriteLine("123123");
            }
            Console.ReadKey();
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
            return this.EfService.OffSetData<TResult>(type, pageSize, pageIndex);
        }
    }
}