using Lucene.Net.Analysis.PanGu;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using SixthDAL;
using SixthLucene.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LuceneIO = Lucene.Net.Store;

namespace SixthLucene.Service
{
    public class LuceneBuild : ILuceneBulid
    {
        private static string Path = "D:\\";
        /// <summary>

        /// 建立索引模型
        /// </summary>
        /// <param name="ciList"></param>
        /// <param name="pathSuffix"></param>
        /// <param name="isCreate"></param>
        public void BuildIndex(List<JdModel> ciList, string pathSuffix = "", bool isCreate = false)
        {
            IndexWriter writer = null;
            try
            {
                if (ciList == null || ciList.Count == 0)
                {
                    return;
                }

                string rootIndexPath = Path;
                string indexPath = string.IsNullOrWhiteSpace(pathSuffix) ? rootIndexPath : string.Format("{0}\\{1}", rootIndexPath, pathSuffix);

                DirectoryInfo dirInfo = Directory.CreateDirectory(indexPath);
                LuceneIO.Directory directory = LuceneIO.FSDirectory.Open(dirInfo);
                writer = new IndexWriter(directory, new PanGuAnalyzer(), isCreate, IndexWriter.MaxFieldLength.LIMITED);
                //writer = new IndexWriter(directory, CreateAnalyzerWrapper(), isCreate, IndexWriter.MaxFieldLength.LIMITED);
                writer.SetMaxBufferedDocs(100);//控制写入一个新的segent前内存中保存的doc的数量 默认10
                writer.MergeFactor = 100;//控制多个segment合并的频率，默认10
                writer.UseCompoundFile = true;//创建符合文件 减少索引文件数量

                ciList.ForEach(c => CreateCIIndex(writer, c));
            }
            finally
            {
                if (writer != null)
                {
                    //writer.Optimize(); 创建索引的时候不做合并  merge的时候处理
                    writer.Close();
                }
            }
        }

        public void DeleteIndex(JdModel ci)
        {
            throw new NotImplementedException();
        }

        public void DeleteIndexMuti(List<JdModel> ciList)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public void InsertIndex(JdModel ci)
        {
            throw new NotImplementedException();
        }

        public void InsertIndexMuti(List<JdModel> ciList)
        {
            throw new NotImplementedException();
        }

        public void MergeIndex(string[] sourceDirs)
        {
            throw new NotImplementedException();
        }

        public void UpdateIndex(JdModel ci)
        {
            throw new NotImplementedException();
        }

        public void UpdateIndexMuti(List<JdModel> ciList)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="analyzer"></param>
        /// <param name="title"></param>
        /// <param name="content"></param>
        private void CreateCIIndex(IndexWriter writer, JdModel ci)
        {
            try
            {
                writer.AddDocument(ParseCItoDoc(ci));
            }
            catch (Exception ex)
            {
                // logger.Error("CreateCIIndex异常", ex);
                throw ex;
            }
        }

        /// <summary>
        /// 将Commodity转换成doc
        /// </summary>
        /// <param name="ci"></param>
        /// <returns></returns>
        private Document ParseCItoDoc(JdModel ci)
        {
            Document doc = new Document();

            doc.Add(new Field("id", ci.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("title", ci.Title, Field.Store.YES, Field.Index.ANALYZED));//盘古分词
            doc.Add(new Field("productid", ci.ProductId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("categoryid", ci.CategoryId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("imageurl", ci.ImageUrl, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new Field("url", ci.Url, Field.Store.YES, Field.Index.NOT_ANALYZED));
            doc.Add(new NumericField("price", Field.Store.YES, true).SetFloatValue((float)ci.Price));
            return doc;
        }
    }
}