using Microsoft.EntityFrameworkCore;
using MyERP.UGIS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.EntityFrameworkCore.Seed.Data
{
    /// <summary>
    /// 字典类型种子数据
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class GroupTypeSeed<TContext> where TContext : DbContext
    {
        private TContext context;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        public GroupTypeSeed(TContext context)
        {
            this.context = context;
        }

        public async Task Init()
        {
            var count = await context.Set<DicType>().CountAsync();
            if (count == 0)
            {
                var list = new List<DicType>() {
                    new DicType(){
                        Id = 1,
                        ConfigJson = JsonConvert.SerializeObject(new { name= "药品" }),
                        TypeCode = "yaopin",
                        OrderId = 1,
                        TypeName = "药品信息"
                    },
                    new DicType(){
                        Id = 2,
                        TypeCode = "paifangfangshi",
                        ConfigJson = JsonConvert.SerializeObject(new { name= "排放方式" }),
                        OrderId = 2,
                        TypeName = "排放方式"
                    },
                    new DicType(){
                        Id = 3,
                        TypeCode = "feishuileixing",
                        ConfigJson = JsonConvert.SerializeObject(new { name= "废水" }),
                        OrderId = 3,
                        TypeName = "废水类型"
                    },
                    new DicType(){
                        Id = 4,
                        TypeCode = "dianweixinxi",
                        ConfigJson = JsonConvert.SerializeObject(new { name= "点位" }),
                        OrderId = 4,
                        TypeName = "点位信息"
                    } ,
                    new DicType(){
                        Id = 5,
                        TypeCode = "yinzixinxi",
                        ConfigJson = JsonConvert.SerializeObject(new { name= "因子" }),
                        OrderId = 5,
                        TypeName = "因子信息"
                    }
                };
            }
        }

    }
}
