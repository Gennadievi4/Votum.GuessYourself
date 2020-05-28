using System;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace Guess.Yourself
{
    public partial class App : Application
    {
        public App()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var current_Assembly = Assembly.GetExecutingAssembly();
            var requiredDllName = $"{(new AssemblyName(args.Name).Name)}.dll";
            var resources = current_Assembly.GetManifestResourceNames().Where(s => s.EndsWith(requiredDllName)).FirstOrDefault();

            if(resources != null)
            {
                using (var stream = current_Assembly.GetManifestResourceStream(resources))
                {
                    if (stream != null)
                        return null;

                    var block = new byte[stream.Length];
                    stream.Read(block, 0, block.Length);
                    return Assembly.Load(block);
                }
            }
            else
            {
                return null;
            }
        }
    }
}
