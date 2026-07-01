using Serilog;

namespace PrimeiroProjeto.Configurations
{
    public static class LoggingConfig
    {
        public static void AddSerilogLogging
            (this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration
                (builder.Configuration)
                //na linha acima le o appsettings e 
                //define o nivel dos logs
                .Enrich.FromLogContext()
                //mostra informações extras aos seus logs
                // como tempo ocorrido, leve, action ID 
                // action name e etc.
                .WriteTo.Console()
                //envia os logs para a janela de console
                .WriteTo.Debug()
                //envia os logs para a janela de output ou
                //debug
                .CreateLogger();
            //finaliza as configurações e cria instancia
            //do logger com tudo definido
            builder.Host.UseSerilog();
            //usa o provedor padrão da microsoft e usa o 
            //serilog que acabou de configurar para 
            //gerenciar todos os logs internos do framework
        }
    }
}
