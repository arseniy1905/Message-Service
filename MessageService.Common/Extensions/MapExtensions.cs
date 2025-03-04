using AutoMapper;
using System;

namespace MessageService.Common.Extensions
{
    public static class MapExtensions
    {
        /// <summary>
        ///Extension, that used to execute a mapping from the current object to a NEW destination object using Automapper. 
        ///If destination type is Interface or Abstract class , proxy (mocked) class will be created by Automapper.
        /// </summary>
        /// <typeparam name="TDestination">Destination type to create</typeparam>
        /// <param name="source"></param>
        /// <param name="mapper">The mapper that used to map</param>
        /// <returns>New instance of destination type</returns>
        public static TDestination MapTo<TDestination>(this object source, IMapper mapper)
        {
            return mapper.Map<TDestination>(source);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="mapper"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static TDestination MapTo<TDestination>(this object source, IMapper mapper, Action<IMappingOperationOptions> opts)
        {
            return mapper.Map<TDestination>(source, opts);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSouce"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="mapper"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSouce, TDestination>(this TSouce source, TDestination destination, IMapper mapper)
        {
            return mapper.Map(source, destination);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSouce"></typeparam>
        /// <typeparam name="TDestination"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        /// <param name="mapper"></param>
        /// <param name="opts"></param>
        /// <returns></returns>
        public static TDestination MapTo<TSouce, TDestination>(this TSouce source, TDestination destination, IMapper mapper, Action<IMappingOperationOptions<TSouce, TDestination>> opts)
        {
            return mapper.Map<TSouce, TDestination>(source, destination, opts);
        }
    }
}
