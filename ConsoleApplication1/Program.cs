using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Index;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuceneIO = Lucene.Net.Store;

namespace ConsoleApplication1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //string indexPath = @"F:\111\";
            ////System.IO.DirectoryInfo dirInfo = Directory.CreateDirectory(indexPath);
            ////dirInfo.Create();
            //LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
            ////LuceneIO.Lock writelock = directory.MakeLock(IndexWriter.WRITE_LOCK_NAME);

            //IndexWriter writer = new IndexWriter(directory, new PanGuAnalyzer(), true, IndexWriter.MaxFieldLength.LIMITED);
        }
    }
}