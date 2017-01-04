using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using JIF.Core.Domain.Articles;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace JIF.Core.Test
{
    [TestClass]
    public class RecursionObjectTest
    {
        [TestMethod]
        public void GetCategoriesTree_Test()
        {
            var source = new List<ArticleCategory>()
            {
                new ArticleCategory { Id = 1, Name = "图书", ParentId = 0 },
                new ArticleCategory { Id = 2, Name = "电子", ParentId = 0 },
                new ArticleCategory { Id = 3, Name = "建材", ParentId = 0 },
                new ArticleCategory { Id = 4, Name = "服装", ParentId = 0 },

                new ArticleCategory { Id = 5, Name = "计算机图书", ParentId = 1 },
                new ArticleCategory { Id = 6, Name = "音乐教辅", ParentId = 1 },
                new ArticleCategory { Id = 7, Name = "小说名著", ParentId = 1 },
                new ArticleCategory { Id = 8, Name = "手机", ParentId = 2 },
                new ArticleCategory { Id = 9, Name = "电脑", ParentId = 2 },
                new ArticleCategory { Id = 10, Name = "数码相机", ParentId = 2 },
                new ArticleCategory { Id = 11, Name = "地板", ParentId = 3 },
                new ArticleCategory { Id = 12, Name = "灯具", ParentId = 3 },
                new ArticleCategory { Id = 13, Name = "家具", ParentId = 3 },
                new ArticleCategory { Id = 14, Name = "男装", ParentId = 4 },
                new ArticleCategory { Id = 15, Name = "女装", ParentId = 4 },
                new ArticleCategory { Id = 16, Name = "童装", ParentId = 4 },

                new ArticleCategory { Id = 17, Name = "编程语言", ParentId = 5 },
                new ArticleCategory { Id = 18, Name = "网页设计", ParentId = 5 },
                new ArticleCategory { Id = 19, Name = "操作系统", ParentId = 5 },
                new ArticleCategory { Id = 20, Name = "笔记本电脑", ParentId = 9 },
                new ArticleCategory { Id = 21, Name = "台式电脑", ParentId = 9 },
                new ArticleCategory { Id = 22, Name = "平板电脑", ParentId = 9 },
                new ArticleCategory { Id = 23, Name = "羽绒服", ParentId = 14 },
                new ArticleCategory { Id = 24, Name = "棉服", ParentId = 14 },
                new ArticleCategory { Id = 25, Name = "夹克", ParentId = 14 },
                new ArticleCategory { Id = 26, Name = "羊毛衫", ParentId = 14 },
            };

            var tar = source.DeepClone();


            Console.WriteLine(JsonConvert.SerializeObject(source));

            var result = source.Where(d => d.ParentId == 0).DeepClone();

            source.RemoveAll(d => d.ParentId == 0);

            Console.WriteLine(JsonConvert.SerializeObject(source));

            Console.WriteLine(JsonConvert.SerializeObject(result));

        }
    }
}
