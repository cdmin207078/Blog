using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Util.Cache
{
    public interface ICache
    {
        /// <summary>
        /// 数据加入缓存，并使用全局配置的过期时间
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        void Put(string key, object obj);

        /// <summary>
        /// 数据加入缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="obj"></param>
        /// <param name="timeout">缓存失效时间,单位 分钟(minutes)</param>
        void Put(string key, object obj, int timeout);

        /// <summary>
        /// 获取缓存数据,未找到则返回 null
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object Get(string key);

        /// <summary>
        /// 删除缓存数据
        /// </summary>
        /// <param name="key"></param>
        void Delete(string key);
    }
}
