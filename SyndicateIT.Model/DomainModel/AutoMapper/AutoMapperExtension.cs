using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace SyndicateIT.Model.DomainModel
{
    [Serializable()]
    public static class AutoMapperExtension
    {
        /// <summary>
        /// An extension that ignore all non existing properties when mapping from a source to a destination
        /// </summary>
        /// <typeparam name="TSource">The source type in automapping</typeparam>
        /// <typeparam name="TDestination">The destination type in automapping</typeparam>
        /// <param name="expression">The Automapping expression to which the extension is added</param>
        /// <returns>IMappingExpression</returns>
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
        {
            var sourceType = typeof(TSource);
            var destinationType = typeof(TDestination);
            var existingMaps = Mapper.GetAllTypeMaps().First(x => x.SourceType.Equals(sourceType)
                && x.DestinationType.Equals(destinationType));
            List<string> properties = existingMaps.GetUnmappedPropertyNames().ToList();
            for (int i = 0; i < properties.Count; i++)
            {
                expression.ForMember(properties[i], opt => opt.Ignore());
            }
            return expression;
        }
    }
}
