using SixthDAL.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthDAL
{
    public class TestDb<T> : DataListController<T> where T : class
    {
        public TestDb(WarmJDEntities repository) : base(repository)
        {
        }

        protected override string tableName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}