using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SixthDAL.DbModel
{
    public class JdBaseModel
    {
        public int Id { get; set; }
        public Nullable<long> ProductId { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public string Title { get; set; }
        public Nullable<decimal> Price { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
    }
}