using System;
using System.IO;
using MsieJavaScriptEngine;
using Resourcer;

namespace Strike.IE
{
    public class Markdownify : IDisposable
    {
        MsieJsEngine engine;

        public Markdownify():this(new Options(), new RenderMethods())
        {
        }

        public Markdownify(Options options, RenderMethods rendereMethods)
            : this(options, rendereMethods, new MsieJsEngine(JsEngineMode.Auto))
        {
        }
#if (!Merged)
        public 
#endif
            Markdownify(Options options, RenderMethods renderMethods, MsieJsEngine engine)
        {
            this.engine = engine;
            var markedJsText = GetMarkedJsText();
            engine.Execute(markedJsText);

            var js = GetContructionJs(options, renderMethods);
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
            return (string)engine.CallFunction("marked", input);
        }

        public void Dispose()
        {
        }
    }
}
