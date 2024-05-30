using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using webApiTcc.Repository;

namespace webApiTcc.Application.DAL
{
    public class SuporteDal
    {
        private readonly webApiTccContext _context;
        public SuporteDal(webApiTccContext context)
        {
            _context = context;
        }
        
        /// <summary>
        /// Pesquisa os registros com o filtro desejádo.
        /// </summary>
        /// <param name="where">Cláusula do filtro</param>
        /// <param name="includes">Nomes das tabelas que você deseja trazer junto com a pesquisa</param>
        /// <returns>Retorna os Objetos encontrados</returns>
        public T[] Pesquisar<T>(Expression<Func<T, bool>> where, params string[] includes) where T : class
        {
            return Pesquisar<T>(where, _context, includes);
        }

        /// <summary>
        /// Pesquisa os registros com o filtro desejádo, precisando de um contexto.
        /// </summary>
        /// <param name="where">Cláusula do filtro</param>
        /// <param name="ctx">Contexto do Banco de Dados</param>
        /// <param name="includes">Nomes das tabelas que você deseja trazer junto com a pesquisa</param>
        /// <returns>Retorna os Objetos encontrados</returns>
        private T[] Pesquisar<T>(Expression<Func<T, bool>> where, webApiTccContext ctx, params string[] includes) where T : class
        {
            var query = ctx.Set<T>().Where(where);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.ToArray();
        }

        /// <summary>
        /// Pesquisa o registros com o código desejádo, precisando de um contexto.
        /// </summary>
        /// <param name="codigo">Código do registro que você deseja Pesquisar</param>
        /// <param name="ctx">Contexto do Banco de Dados</param>
        /// <returns>Retorna o Objeto encontrado</returns>
        public static T PesquisarPorCodigo<T>(long codigo, webApiTccContext ctx) where T : class
        {
            var dbSet = ctx.Set<T>();
            return dbSet.Find(codigo);
        }

        /// <summary>
        /// Pesquisa o primeiro registro com o filtro desejádo, precisando de um contexto.
        /// </summary>
        /// <param name="where">Cláusula do filtro</param>
        /// <param name="ctx">Contexto do Banco de Dados</param>
        /// <param name="includes">Nomes das tabelas que você deseja trazer junto com a pesquisa</param>
        /// <returns>Retorna o primeiro Objeto encontrado</returns>
        public static T PesquisarFirstOrDefault<T>(Expression<Func<T, bool>> where, webApiTccContext ctx, params string[] includes) where T : class
        {
            IQueryable<T> dbSet = ctx.Set<T>();

            foreach (string include in includes)
                dbSet = dbSet.Include(include);

            return dbSet.AsNoTracking().FirstOrDefault<T>(where);
        }

        /// <summary>
        /// Pesquisa por qualquer registro com o filtro desejádo, precisando de um contexto.
        /// </summary>
        /// <param name="where">Cláusula do filtro</param>
        /// <param name="ctx">Contexto do Banco de Dados</param>
        /// <param name="includes">Nomes das tabelas que você deseja trazer junto com a pesquisa</param>
        /// <returns>Retorna o primeiro Objeto encontrado</returns>
        public static bool ExisteRegistro<T>(webApiTccContext ctx, Expression<Func<T, bool>> where, params string[] includes) where T : class
        {
            IQueryable<T> query = ctx.Set<T>();

            foreach (string include in includes)
            {
                query = query.Include(include);
            }

            return query.AsNoTracking().Any(where);
        }

        /// <summary>
        /// Lista todos os Registros da tabela, precisando de um contexto.
        /// </summary>
        /// <param name="ctx">Contexto do Banco de Dados</param>
        /// <returns>Retorna os Objetos listados</returns>
        public static List<T> Listar<T>(webApiTccContext ctx) where T : class
        {
            var dbSet = ctx.Set<T>();
            return dbSet.AsNoTracking().ToList<T>();
        }

        /// <summary>
        /// Lista todos os Registros da tabela através de um codigo ou parâmetro adicional, precisando de um contexto.
        /// </summary>
        /// <param name="ctx">Contexto do Banco de Dados</param>
        /// <returns>Retorna os Objetos listados</returns>
        public static List<T> ListarPorCodigo<T>(Expression<Func<T, bool>> where, webApiTccContext ctx) where T : class
        {
            return ctx.Set<T>().AsNoTracking().Where(where).ToList();
        }
        
        /// <summary>
        /// Valida se existe algum item através de um codigo ou parâmetro adicional, precisando de um contexto.
        /// </summary>
        /// <param name="ctx">Contexto do Banco de Dados</param>
        /// <returns>Retorna um booleano</returns>
        public static bool ValidarPorQualquer<T>(webApiTccContext ctx, Expression<Func<T, bool>> any) where T : class
        {
            if (ctx.Set<T>().AsNoTracking().Any(any))
                return true;
            return false;
        }
    }
}
