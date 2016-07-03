using System;
using System.IO;
using System.Web;
using Microsoft.ClearScript.V8;
using Resourcer;

namespace Strike.V8
{
    public class Markdownify : IDisposable
    {
        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Local
        public V8ScriptEngine Engine { get; private set; }

        public Markdownify():this(new Options(), new RenderMethods())
        {
        }

        public Markdownify(Options options, RenderMethods rendereMethods): this (options, rendereMethods, new V8ScriptEngine())
        {
        }

        public Markdownify(Options options, RenderMethods rendereMethods, V8ScriptEngine engine)
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
            var markedPath = Path.Combine(AssemblyLocation.CurrentDirectory, @"marked.js");
            if (File.Exists(markedPath))
            {
                return File.ReadAllText(markedPath);
            }
            return Resource.AsString("marked.js");
        }

        public string Transform(string input, string options = null)
        {
            input = HttpUtility.JavaScriptStringEncode(input);
            if (options == null)
            {
                options = "{}";
            }
            var code = $@"marked('{input}',{options})";
            return (string) Engine.Evaluate(code);
        }

        public void Dispose()
        {
        }
    }
}