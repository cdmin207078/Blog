﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JIF.Core.Domain.Articles
{
    public partial class Category : BaseEntity
    {
        /// <summary>
        /// 分类名称
        /// </summary>
        public string Name { get; set; }
    }
}
