using SixthDAL.DbModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading;

namespace SixthDAL
{
    public abstract class BasicServcie<Tclass> : StockEntityFramework<WarmJDEntities>
        where Tclass : class
    {
        public virtual Tclass Add(Tclass t)
        {
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId + "当前ID:" + typeof(Tclass).Name);
            return ExecEntityJdData(ef => ef.Set<Tclass>().Add(t), true);
        }

        public virtual Tclass QuerySingle(object objectKey)
        {
            return ExecEntityJdData(ef => ef.Set<Tclass>().Find(objectKey));
        }

        public virtual Tclass AddorUpdate(Tclass T)
        {
            return ExecEntityJdData(ef =>
            {
                ef.Set<Tclass>().AddOrUpdate(T);
                return ef.Set<Tclass>().Find(T);
            }, true);
        }

        public virtual IList<Tclass> SkipTable(int pageSize, int pageIndex)
        {
            return ExecEntityJdData(ef =>
           {
               return ef.Set<Tclass>().Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
           });
        }
    }
}