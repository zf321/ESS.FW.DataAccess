/*
 * This code is provided as is with no warranty. If you find a bug please report it on github.
 * If you would like to use the code please leave this comment at the top of the page
 * License MIT (c) Brent McKendrick 2012
 */

using System;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace ESS.FW.DataAccess.EF.GraphDiff
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Merges a graph of entities with the data store.
        /// </summary>
        /// <typeparam name="T">The type of the root entity</typeparam>
        /// <param name="context">The database context to attach / detach.</param>
        /// <param name="entity">The root entity.</param>
        /// <param name="mapping">The mapping configuration to define the bounds of the graph</param>
        /// <returns>The attached entity graph</returns>
	    public static T UpdateGraph<T>(this DbContext context, T entity, Expression<Func<IUpdateConfiguration<T>, object>> mapping = null) where T : class, new()
        {
            //var root = mapping == null ? new GraphNode() : new ConfigurationVisitor<T>().GetNodes(mapping);
            //var graphDiffer = new GraphDiffer<T>(root);
            //return graphDiffer.Merge(context, entity);
            return null;
        }

        /// <summary>
        /// Merges a graph of entities with the data store.
        /// support owncollection only
        /// </summary>
        /// <typeparam name="T">The type of the root entity</typeparam>
        /// <param name="context">The database context to attach / detach.</param>
        /// <param name="entity">The root entity.</param>
        /// <param name="mapping">The mapping configuration to define the bounds of the graph</param>
        /// <returns>The attached entity graph</returns>
	    public static T UpdateGraph<T>(this DbContext context, T entity, string[] mapping = null) where T : class, new()
        {
            //var root = mapping == null || !mapping.Any() ? new GraphNode() : new StringConfigurationVisitor<T>().GetNodes(mapping);
            //var graphDiffer = new GraphDiffer<T>(root);
            //return graphDiffer.Merge(context, entity);
            return null;
        }
    }
}
