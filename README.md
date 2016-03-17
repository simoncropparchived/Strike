![Icon](https://raw.github.com/SimonCropp/Strike/master/Icons/package_icon.png)

Wraps [Marked.js](https://github.com/chjj/marked/) to make it usable from .net.

## Nuget

The project is shipped in several nugets. You only need to pick one.

There are now five packages to choose from 

### Strike.IE [![NuGet Status](http://img.shields.io/nuget/v/Strike.IE.svg?style=flat)](https://www.nuget.org/packages/Strike.IE/)

Uses the MSIE engine and has a dependency on the MSIE package. http://nuget.org/packages/Strike.IE

    PM> Install-Package Strike.IE
    
### Strike.IE.Merged [![NuGet Status](http://img.shields.io/nuget/v/Strike.IE.Merged.svg?style=flat)](https://www.nuget.org/packages/Strike.IE.Merged/)

Uses the MSIE engine and merges the MSIE assembly. http://nuget.org/packages/Strike.IE.Merged

    PM> Install-Package Strike.IE.Merged

### Strike.Jint [![NuGet Status](http://img.shields.io/nuget/v/Strike.Jint.svg?style=flat)](https://www.nuget.org/packages/Strike.Jint/)

Uses the Jint engine and has a dependency on the Jint package. http://nuget.org/packages/Strike.Jint

    PM> Install-Package Strike.Jint
    
### Strike.Jint.Merged [![NuGet Status](http://img.shields.io/nuget/v/Strike.Jint.Merged.svg?style=flat)](https://www.nuget.org/packages/Strike.Jint.Merged/)

Uses the Jint engine and merges the Jint assembly. http://nuget.org/packages/Strike.Jint.Merged

    PM> Install-Package Strike.Jint.Merged
    
### Strike.V8 [![NuGet Status](http://img.shields.io/nuget/v/Strike.V8.svg?style=flat)](https://www.nuget.org/packages/Strike.V8/)

Uses the V8 engine and has a dependency on the ClearScript V8 package. http://www.nuget.org/packages/Strike.V8/
    
    PM> Install-Package Strike.V8

## Usage

So if you run this 

```
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
```

The output will be this 

```
<table>
	<thead>
		<tr>
			<th>Tables</th>
			<th style="text-align:center">Are</th>
			<th style="text-align:right">Cool</th>
		</tr>
	</thead>
	<tbody>
		<tr>
			<td>col 3 is</td>
			<td style="text-align:center">right-aligned</td>
			<td style="text-align:right">$1600</td>
		</tr>
		<tr>
			<td>col 2 is</td>
			<td style="text-align:center">centered</td>
			<td style="text-align:right">$12</td>
		</tr>
	</tbody>
</table>
```

**Note:** The indentation is added for clarity.  


## MarkedJS

The binary ships with a resource merged version of [MarkedJS](https://github.com/chjj/marked/). Also see the [License](https://github.com/chjj/marked/blob/master/LICENSE).

### The Marked.js version

The version included in the stable version of Strike is v0.3.2. 

The version included in the unstable version of Strike is the version as at [2015-05-08](https://github.com/chjj/marked/blob/49b7eaca76d14ccd91f87decc11bbe213a6e0af4/lib/marked.js). 

If you feel a newer version should be included at any point in time please raise an issue.

### Running a custom version

If you want to run a custom version of MarkedJS simply place the custom `marked.js` in the current running directory and that file will be used instead of the merged version. The newest MarkedJS file can be obtained here https://github.com/chjj/marked/tree/master/lib.

### Controlling Marked

The `Markdownify` class takes two parameters 

#### Options

Represents [Marked Options](https://github.com/chjj/marked#options-1)
 
    public class Options
    {
        /// <summary>
        /// Enable GitHub flavored markdown.
        /// https://github.com/chjj/marked#gfm
        /// </summary>
        public bool GitHubFlavor = true;

        /// <summary>
        /// https://github.com/chjj/marked#tables
        /// Enable GFM tables. This option requires the gfm option to be true.
        /// </summary>
        public bool Tables = true;

        /// <summary>
        /// Enable GFM line breaks. This option requires the gfm option to be true.
        /// https://github.com/chjj/marked#breaks
        /// </summary>
        public bool Breaks;

        /// <summary>
        /// Conform to obscure parts of markdown.pl as much as possible. Don't fix any of the original markdown bugs or poor behavior.
        /// https://github.com/chjj/marked#pedantic
        /// </summary>
        public bool Pedantic;

        /// <summary>
        /// Sanitize the output. Ignore any HTML that has been input.
        /// Default: false
        /// https://github.com/chjj/marked#sanitize
        /// </summary>
        public bool Sanitize;

        /// <summary>
        /// Use smarter list behavior than the original markdown. 
        /// Default: true
        /// https://github.com/chjj/marked#smartlists
        /// </summary>
        public bool SmartLists = true;

        /// <summary>
        /// Use "smart" typograhic punctuation for things like quotes and dashes.
        /// Default: false
        /// https://github.com/chjj/marked#smartypants
        /// </summary>
        public bool SmartyPants;

        /// <summary>
        /// Use "Xhtml" rendering.
        /// Default: false
        /// https://github.com/chjj/marked#xhtml
        /// </summary>
        public bool Xhtml;

        /// <summary>
        /// A function to highlight code blocks. 
        /// https://github.com/chjj/marked#highlight
        /// Default: "function (code) {return code;}"
        /// </summary>
        public string Highlight = "function (code) {return code;}";
    }
    
#### RenderMethods

Represents [Block level render methods](https://github.com/chjj/marked#block-level-renderer-methods) and [Inline level render methods](https://github.com/chjj/marked#inline-level-renderer-methods) 

    public class RenderMethods
    {
        public string Code;
        public string BlockQuote;
        public string Html;
        public string Heading;
        public string Hr;
        public string List;
        public string ListItem;
        public string Paragraph;
        public string Table;
        public string TableRow;
        public string TableCell;
        public string Strong;
        public string Em;
        public string Codespan;
        public string Br;
        public string Del;
        public string Link;
        public string Text;
        public string Image;
    }

## Performance

Using [John Grubers Markdown Test Suite](https://daringfireball.net/projects/markdown/) as a document source.

| Engine | Warm up | Construction |  Bulk (304 docs) | Average per doc |
|:-------|:-------:|:------------:|:---------------:|:---------------:|
|MarkdownSharp|0 ms|0 ms|479 ms|**1.58 ms**|
|MarkdownDeep|0 ms|0 ms|35 ms|**0.12 ms**|
|Strike.IE|4 ms|1 ms|187 ms|**0.62 ms**|
|Strike.Jint|7 ms|11 ms|5693 ms|**18.73 ms**|
|Strike.V8|30 ms|4 ms|93 ms|**0.31 ms**|

### So why use this library

So this raises the question of
 
> why use this library over MarkdownDeep

And the answer is 

> It is not only about performance

Other points to consider

* MarkedJS rendering most closely matches GitHubs markdown rendering
* MarkedJS is an active project with bugs fixed promptly and features being added 

## Icon 

<a href="http://thenounproject.com/term/lightning/6029/" target="_blank">Lightning</a> designed by <a href="http://thenounproject.com/tlb/" target="_blank">Thomas Le Bas</a> from The Noun Project
