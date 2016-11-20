using SixthDAL;
using SixthLucene.Interface;
using SixthLucene.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            BuildTools = new LuceneBuild();
        }

        protected ILuceneBulid BuildTools { get; set; }
        protected object JdClass { get; set; }
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

            ParallelLoopResult pl = new ParallelLoopResult();
            for (int i = 1; i < Page; i++)
            {
                ParallelOptions po = new ParallelOptions();
                po.MaxDegreeOfParallelism = 20;
                var list = GetSkipPage<JdModel>(JdType, PageSize, i);
                pl = Parallel.ForEach(list, po, p =>
                {
                    GetBuild(list, i);
                });
            }
            while (pl.IsCompleted)
            {
                Console.WriteLine("123123");
            }
            Console.ReadKey();
        }

        private void BuildLucene(IEnumerable<JdModel> list)
        {
            // ParallelLoopResult plResult = new ParallelLoopResult();
            ParallelOptions po = new ParallelOptions();
            po.MaxDegreeOfParallelism = 20;
            var loopresult = Parallel.ForEach(list, po, p =>
              {
                  //  GetBuild(list,i);
              });
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

        private void GetBuild(IEnumerable<JdModel> list, int i)
        {
            BuildTools.BuildIndex(list.ToList(), i.ToString("000"), true);
        }

        private void test2(ParallelQuery<JdModel> plist)
        {
        }

        //private IList<object> GetPageData(int index)
        //{
        //    //  return this.EfService.SkipTable(JdClass, pageSize, index);
        //}
    }
}