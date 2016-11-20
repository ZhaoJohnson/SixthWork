using SixthDAL;
using SixthDAL.DbModel;
using SixthLucene;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkSixth
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //LuceneService LService = new LuceneService(new BasicServci());
            foreach (var item in init())
            {
                using (var Service = new LuceneService(new BasicServci(), item))
                {
                    Service.Run();
                }
            }
        }

        private static List<Type> init()
        {
            List<Type> result = new List<Type>
            {
                typeof(JD_Commodity_001)
            };
            return result;
        }
    }
}