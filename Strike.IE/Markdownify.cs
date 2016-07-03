using System;
using System.IO;
using MsieJavaScriptEngine;
using Resourcer;
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable ConvertToAutoPropertyWhenPossible

namespace Strike.IE
{
    public class Markdownify : IDisposable
    {
        MsieJsEngine engine;

#if (!Merged)
        public MsieJsEngine Engine
        {
            get { return engine; }
        }
#endif

        public Markdownify() : this(new Options(), new RenderMethods())
        {
        }

        public Markdownify(Options options, RenderMethods rendereMethods)
            : this(options, rendereMethods, new MsieJsEngine(new JsEngineSettings {EngineMode = JsEngineMode.Auto}))
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
            return $@"
var renderer = new marked.Renderer();
{renderExtensions}
marked.setOptions({optionsAsJs});";
        }

        /// <summary>
        /// Get the content of marked.js
        /// By default marked.js is read from an embedded resource.
        /// </summary>
        public string GetMarkedJsText()
        {
            var markedPath = Path.Combine(AssemblyLocation.CurrentDirectory, "marked.js");
            if (File.Exists(markedPath))
            {
                return File.ReadAllText(markedPath);
            }
            return Resource.AsString("marked.js");
        }

        public string Transform(string input)
        {
            return engine.CallFunction<string>("marked", input);
        }

        public void Dispose()
        {
        }
    }
}