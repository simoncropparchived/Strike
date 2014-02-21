using System;
using System.IO;
using Microsoft.ClearScript.V8;
using Resourcer;

namespace Strike.V8
{
    public class Markdownify : IDisposable
    {
        V8ScriptEngine engine;

        public Markdownify():this(new Options(), new RenderMethods())
        {
        }

        public Markdownify(Options options, RenderMethods rendereMethods)
        {
            engine = new V8ScriptEngine();

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
            var renderExtensions = GetRenderExtensionsJs(renderMethods);

            var optionsAsJs = GetOptionsJs(options);
            return string.Format(@"
var renderer = new marked.Renderer();
{0}
marked.setOptions({1});", renderExtensions, optionsAsJs);
        }

        string GetRenderExtensionsJs(RenderMethods renderMethods)
        {
            var renderExtensions = "";
            if (renderMethods.Code != null)
            {
                renderExtensions += "renderer.code =" + renderMethods.Code;
            }
            if (renderMethods.BlockQuote != null)
            {
                renderExtensions += "renderer.blockquote =" + renderMethods.BlockQuote;
            }
            if (renderMethods.Heading != null)
            {
                renderExtensions += "renderer.heading =" + renderMethods.Heading;
            }
            if (renderMethods.Hr != null)
            {
                renderExtensions += "renderer.hr =" + renderMethods.Hr;
            }
            if (renderMethods.Html != null)
            {
                renderExtensions += "renderer.html =" + renderMethods.Html;
            }
            if (renderMethods.List != null)
            {
                renderExtensions += "renderer.list =" + renderMethods.List;
            }
            if (renderMethods.ListItem != null)
            {
                renderExtensions += "renderer.listitem =" + renderMethods.ListItem;
            }
            if (renderMethods.Paragraph != null)
            {
                renderExtensions += "renderer.paragraph =" + renderMethods.Paragraph;
            }
            if (renderMethods.Table != null)
            {
                renderExtensions += "renderer.table =" + renderMethods.Table;
            }
            if (renderMethods.TableRow != null)
            {
                renderExtensions += "renderer.tablerow =" + renderMethods.TableRow;
            }
            if (renderMethods.TableCell != null)
            {
                renderExtensions += "renderer.tablecell =" + renderMethods.TableCell;
            }
            if (renderMethods.Strong != null)
            {
                renderExtensions += "renderer.strong =" + renderMethods.Strong;
            }
            if (renderMethods.Em != null)
            {
                renderExtensions += "renderer.em =" + renderMethods.Em;
            }
            if (renderMethods.Codespan != null)
            {
                renderExtensions += "renderer.codespan =" + renderMethods.Codespan;
            }
            if (renderMethods.Br != null)
            {
                renderExtensions += "renderer.br =" + renderMethods.Br;
            }
            if (renderMethods.Del != null)
            {
                renderExtensions += "renderer.del =" + renderMethods.Del;
            }
            if (renderMethods.Link != null)
            {
                renderExtensions += "renderer.link =" + renderMethods.Link;
            }
            if (renderMethods.Image != null)
            {
                renderExtensions += "renderer.image =" + renderMethods.Image;
            }
            return renderExtensions;
        }

        string GetOptionsJs(Options options)
        {
            return string.Format(@"{{gfm: {0}, tables: {1}, breaks: {2}, sanitize: {3}, smartLists: {4}, pedantic: {5}, smartypants: {6}, renderer: renderer}}",
                    options.GitHubFlavor.AsJs(),
                    options.Tables.AsJs(),
                    options.Breaks.AsJs(),
                    options.Sanitize.AsJs(),
                    options.SmartLists.AsJs(),
                    options.Pedantic.AsJs(),
                    options.SmartyPants.AsJs());
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
            return (string)engine.Script.marked(input);
        }

        public void Dispose()
        {
        }
    }
}