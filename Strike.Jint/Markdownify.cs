using System;
using System.IO;
using Jint;
using Resourcer;

namespace Strike.Jint
{
    public class Markdownify : IDisposable
    {

#if (Merged)
        Engine Engine;
#else
        public Engine Engine { get; private set; }
#endif

        public Markdownify():this(new Options(), new RenderMethods())
        {
        }

        public Markdownify(Options options, RenderMethods rendereMethods)
            : this(options, rendereMethods, new Engine())
        {
        }

        public Markdownify(Options options, RenderMethods rendereMethods, Engine engine)
        {
            Engine = engine;

            var markedJsText = GetMarkedJsText();
            engine.Execute(markedJsText);

            var js = GetContructionJs(options, rendereMethods);
            engine.Execute(js);
        }

        /// <summary>
        /// Get the js to construct a new marked renderer.
        /// </summary>
        public string GetContructionJs(Options options, RenderMethods renderMethods)
        {
            var renderExtensions = renderMethods.GetRenderExtensionsJs();

            var optionsAsJs = options.GetOptionsJs();
            return string.Format(@"
var renderer = new marked.Renderer();
{0}
marked.setOptions({1});", renderExtensions, optionsAsJs);
        }

        /// <summary>
        /// Get the content of marked.js
        /// By default marked.js is read from an embedded resource.
        /// </summary>
        public string GetMarkedJsText()
        {
            var markedPath = Path.Combine(AssemblyLocation.CurrentDirectory, @"marked.js");
            if (File.Exists(markedPath))
            {
                return File.ReadAllText(markedPath);
            }
            return Resource.AsString("marked.js");
        }

        public string Transform(string input)
        {
            return Engine.Invoke("marked",input).AsString();
        }

        public void Dispose()
        {
        }
    }
}