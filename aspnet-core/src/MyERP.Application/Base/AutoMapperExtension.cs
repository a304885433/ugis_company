using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MyERP.Base
{
    /// <summary>
    /// AutoMapper拓展
    /// </summary>
    public static class AutoMapperExtension
    {
        public static TDestination MapTo<TDestination>(this object source, IMapper mapper)
        {
            return mapper.Map<TDestination>(source);
        }

        public static TDestination MapFrom<TDestination, TSource>(this TDestination destination, TSource source, IMapper mapper)
        {
            return mapper.Map(source, destination);
        }

        public static TDestination MapFrom<TDestination, TSource>(this TDestination destination, TSource source)
        {
            return Mapper.Map(source, destination);
        }

        public static TDestination MapFrom<TDestination, TSource>(this TDestination destination, params TSource[] sources)
        {
            if (sources.Length == 0)
            {
                return default(TDestination);
            }

            var initialSource = sources[0];
            var mappingResult = Mapper.Map(initialSource, destination);
            foreach (var source in sources.Skip(1))
            {
                mappingResult = Mapper.Map(source, destination);
            }

            return mappingResult;
        }
    }
}
