![Icon](https://raw.github.com/SimonCropp/Strike/master/Icons/package_icon.png)

Wraps [Marked.js](https://github.com/chjj/marked/) to make it usable from .net.

## Nuget

Nuget package http://nuget.org/packages/Strike 

To Install from the Nuget Package Manager Console 
    
    PM> Install-Package Strike

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

## Icon 

<a href="http://thenounproject.com/term/lightning/6029/" target="_blank">Lightning</a> designed by <a href="http://thenounproject.com/tlb/" target="_blank">Thomas Le Bas</a> from The Noun Project
