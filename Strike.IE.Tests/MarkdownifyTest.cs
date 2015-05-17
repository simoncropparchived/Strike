using System.Diagnostics;
using ApprovalTests;
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
public class MarkdownifyTest
{
    [Test]
    public void Sample()
    {
        var input = @"
| Tables        | Are           | Cool  |
| ------------- |:-------------:| -----:|
| col 3 is      | right-aligned | $1600 |
| col 2 is      | centered      |   $12 |
";

        using (var markdownify = new Markdownify())
        {
            Debug.WriteLine(markdownify.Transform(input));
        }
    }

    [Test]
    public void TableTest()
    {
        var input = @"
| Tables        | Are           | Cool  |
| ------------- |:-------------:| -----:|
| col 3 is      | right-aligned | $1600 |
| col 2 is      | centered      |   $12 |
| zebra stripes | are neat      |    $1 |
";

        using (var markdownify = new Markdownify())
        {
            Approvals.Verify(markdownify.Transform(input).FixNewLines());
        }
    }

    [Test]
    public void InlineHtmlTest()
    {
        var input = @"
<table>
<thead>
<tr>
<th>Tables</th>
<th >Are</th>
<th >Cool</th>
</tr>
</thead>
<tbody>
<tr>
<td>col 3 is</td>
<td >right-aligned</td>
<td >$1600</td>
</tbody>
</table>";

        using (var markdownify = new Markdownify())
        {
            Approvals.Verify(markdownify.Transform(input).FixNewLines());
        }
    }


    [Test]
    public void InlineCodeTest()
    {
        var input = @"`the code`";
        using (var markdownify = new Markdownify())
        {
            var transform = markdownify.Transform(input);
            Assert.AreEqual("<p><code>the code</code></p>\n", transform);
        }
    }

    [Test]
    public void HeadingTest()
    {
        using (var markdownify = new Markdownify())
        {
            var transform = markdownify.Transform("# The Heading");
            Approvals.Verify(transform);
        }
    }


    [Test]
    public void MultiLineCodeTest()
    {
        var input = @"
```
the code
```";
        using (var markdownify = new Markdownify())
        {
            var transform = markdownify.Transform(input);
            Assert.AreEqual("<pre><code>the code\n</code></pre>", transform);
        }
    }


    [Test]
    public void MultiLineCodeWithhighlightTest()
    {
        var input = @"
```
the code
```";
        var options = new Options
        {
            Highlight = "function (code) {return 'before' + code + 'after';}"
        };
        using (var markdownify = new Markdownify(options, new RenderMethods()))
        {
            var transform = markdownify.Transform(input);
            Assert.AreEqual("<pre><code>beforethe codeafter\n</code></pre>", transform);
        }
    }

    [Test]
    public void InvokeRootMarkedJsMember()
    {
        var input = "![text](href.png \"text\")";
        
        var rendereMethods = new RenderMethods
        {
            Image = @"
function (href, title, text) {
    text = 'replaced text';
    return marked.Renderer.prototype.image.apply(this, arguments);
}"
        };
        using (var markdownify = new Markdownify(new Options(), rendereMethods))
        {
            var transform = markdownify.Transform(input);
            Assert.AreEqual("<p><img src=\"href.png\" alt=\"replaced text\" title=\"text\"></p>\n", transform);
        }
    }
}