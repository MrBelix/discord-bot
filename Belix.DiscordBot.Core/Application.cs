using Belix.DiscordBot.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Belix.DiscordBot.Core
{
    public sealed class Application<T> where T : IStartup
    {
        private readonly IServiceProvider _services;
        private readonly IEnumerable<Type>? _extensions;

        public Application(IServiceProvider services, IEnumerable<Type>? extensions = null)
        {
            _services = services;
            _extensions = extensions;
        }

        public void Run() => RunAsync().GetAwaiter().GetResult();

        private async Task RunAsync()
        {

            if (_extensions != null) {
                foreach (Type type in _extensions)
                {
                    var extension = (IExtension)ActivatorUtilities.CreateInstance(_services, type);
                    extension.Register();
                }
            }

            var startup = (IStartup)ActivatorUtilities.CreateInstance(_services, typeof(T));
            await startup.RunAsync();

            await Task.Delay(Timeout.Infinite);
        }
    }
}
