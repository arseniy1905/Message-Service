using Microsoft.AspNetCore;
using MessageService.Server;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        catch (Exception ex)
        {
            //logger.Error(ex, $"Stopped program - { ex.Message }");
            throw (ex);
        }
       
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
   
  
}

