using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTake.DAL.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockTake.IOC
{
    public static class Dependencia
    {
        //recibe un servicio de colecciones, metodo de extension. Dentro del servicio IServiceColecction se agrega el metodo InyectarDependencias
        //Metodo de extension
        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DbstockTakeContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("stringSQL"));
            });
        }
    }
}
