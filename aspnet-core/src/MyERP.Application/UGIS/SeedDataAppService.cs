using Abp.Domain.Repositories;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyERP.UGIS
{
    public class SeedDataAppService : MyERPAppServiceBase
    {
        IRepository<DicType> dicTypeRepository;

        public SeedDataAppService(IRepository<DicType> dicTypeRepository)
        {
            this.dicTypeRepository = dicTypeRepository;
        }

        public async Task Index()
        {
            await InitDicType();
        }

        private async Task InitDicType()
        {
            var count = await dicTypeRepository.CountAsync();
            if (count > 0)
            {
                return;
            }
            var list = new List<DicType>() {
                    new DicType(){
                        ExtensionData = JsonConvert.SerializeObject(new { name= "药品" }),
                        TypeCode = "yaopin",
                        OrderId = 1,
                        TypeName = "药品信息"
                    },
                    new DicType(){
                        TypeCode = "paifangfangshi",
                        ExtensionData = JsonConvert.SerializeObject(new { name= "排放方式" }),
                        OrderId = 2,
                        TypeName = "排放方式"
                    },
                    new DicType(){
                        TypeCode = "feishuileixing",
                        ExtensionData = JsonConvert.SerializeObject(new { name= "废水" }),
                        OrderId = 3,
                        TypeName = "废水类型"
                    },
                    new DicType(){
                        TypeCode = "dianweixinxi",
                        ExtensionData = JsonConvert.SerializeObject(new { name= "点位" }),
                        OrderId = 4,
                        TypeName = "点位信息"
                    } ,
                    new DicType(){
                        TypeCode = "yinzixinxi",
                        ExtensionData = JsonConvert.SerializeObject(new { name= "因子" }),
                        OrderId = 5,
                        TypeName = "因子信息"
                    },
                    new DicType(){
                        TypeCode = "shoujifangshi",
                        ExtensionData = JsonConvert.SerializeObject(new { name= "收集方式" }),
                        OrderId = 5,
                        TypeName = "收集方式"
                    }
                };
            list.ForEach(t =>
            {
                dicTypeRepository.Insert(t);
            });
        }

    }
}
