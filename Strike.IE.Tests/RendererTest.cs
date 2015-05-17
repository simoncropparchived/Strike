using NUnit.Framework;
using Strike;
#if (IE)
using Strike.IE;
#endif
#if (V8)
using Strike.V8;
#endif
#if (Jint)
using Strike.Jint;
#endif

[TestFixture]
public class RendererTest
{
    [Test]
    public void Code()
    {
        var input = 
@"```
the code
```";

        var rendereMethods = new RenderMethods
        {
            Code = @"function(code,language) {  return '<mycode>' + code + '</mycode>';};",
        };
        using (var markdownify = new Markdownify(new Options(), rendereMethods))
        {
            var transform = markdownify.Transform(input);
            Assert.AreEqual("<mycode>the code</mycode>", transform);
        }
    }

    [Test]
    public void Hr()
    {
        var rendereMethods = new RenderMethods
        {
            Hr = "function(){ return '<myhr>';}"
        };
        using (var markdownify = new Markdownify(new Options(), rendereMethods))
        {
            var transform = markdownify.Transform("---");
            Assert.AreEqual("<myhr>", transform);
        }
    }

    [Test]
    public void Text()
    {
        var methods = new RenderMethods
        {
            Text = @"function(text) {return '<b>' + text + '</b>';};"
        };
        using (var markdownify = new Markdownify(new Options(), methods))
        {
            var transform = markdownify.Transform("the text");
            Assert.AreEqual("<p><b>the text</b></p>\n", transform);
        }
    }

    [Test]
    public void Image()
    {
        var input = "![text](href.png \"text\")";
        
        var rendereMethods = new RenderMethods
        {
            Image = @"
function (href, title, text) {
    return '<a><img src=""' + href + '"" alt=""' + text + '"" title=""' + text +'""></a>';
}"
        };
        using (var markdownify = new Markdownify(new Options(), rendereMethods))
        {
            var transform = markdownify.Transform(input);
            Assert.AreEqual("<p><a><img src=\"href.png\" alt=\"text\" title=\"text\"></a></p>\n", transform);
        }
    }
}