using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using ABNLookup.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using ABNLookup.Settings;
using ABNLookup.Domain.Models;
using ABNLookup.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace ABNLookup
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = WebHost
                .CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
           
            using var scope = host.Services.CreateScope();
            var settings = scope.ServiceProvider.GetService<AppSettings>();
            var abnContext = scope.ServiceProvider.GetService<AbnContext>();
            if (await abnContext.Database.EnsureCreatedAsync())
            {               
                var jsonString = await File.ReadAllTextAsync(settings.AbnJsonFilePath);
                var abnList = JsonSerializer.Deserialize<IReadOnlyList<AbnJsonFileModel>>(jsonString);
                await abnContext.Abns.AddRangeAsync(abnList.Select(abn => new Abn
                    (abn.ClientInternalId, abn.ABNidentifierValue,
                        abn.ACNidentifierValue, abn.mainNameorganisationName)));

                jsonString = await File.ReadAllTextAsync(settings.ProcessMessagesJsonFilePath);
                var jsonObjects = JsonSerializer.Deserialize<IReadOnlyList<ProcessMessagesFileModel>>(jsonString);
                await abnContext.MessageCodes.AddRangeAsync(jsonObjects.Select(code =>
                        new MessageCode(code.Code, code.Description)));

                _ = await abnContext.SaveChangesAsync();
            }

            // Make sure the sqlite db file is writable.
            string filePath = settings.SqLiteDbFilePath;
            FileAttributes attributes = File.GetAttributes(filePath);
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                File.SetAttributes(filePath, attributes & ~FileAttributes.ReadOnly);

            await host.RunAsync();
        }
    }
}