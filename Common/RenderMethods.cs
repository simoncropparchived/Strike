namespace Strike
{
    /// <summary>
    /// Block level renderer methods
    /// https://github.com/chjj/marked#block-level-renderer-methods
    /// </summary>
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
        public string Image;

        public string GetRenderExtensionsJs()
        {
            var renderExtensions = "";
            if (Code != null)
            {
                renderExtensions += "renderer.code =" + Code;
            }
            if (BlockQuote != null)
            {
                renderExtensions += "renderer.blockquote =" + BlockQuote;
            }
            if (Heading != null)
            {
                renderExtensions += "renderer.heading =" + Heading;
            }
            if (Hr != null)
            {
                renderExtensions += "renderer.hr =" + Hr;
            }
            if (Html != null)
            {
                renderExtensions += "renderer.html =" + Html;
            }
            if (List != null)
            {
                renderExtensions += "renderer.list =" + List;
            }
            if (ListItem != null)
            {
                renderExtensions += "renderer.listitem =" + ListItem;
            }
            if (Paragraph != null)
            {
                renderExtensions += "renderer.paragraph =" + Paragraph;
            }
            if (Table != null)
            {
                renderExtensions += "renderer.table =" + Table;
            }
            if (TableRow != null)
            {
                renderExtensions += "renderer.tablerow =" + TableRow;
            }
            if (TableCell != null)
            {
                renderExtensions += "renderer.tablecell =" + TableCell;
            }
            if (Strong != null)
            {
                renderExtensions += "renderer.strong =" + Strong;
            }
            if (Em != null)
            {
                renderExtensions += "renderer.em =" + Em;
            }
            if (Codespan != null)
            {
                renderExtensions += "renderer.codespan =" + Codespan;
            }
            if (Br != null)
            {
                renderExtensions += "renderer.br =" + Br;
            }
            if (Del != null)
            {
                renderExtensions += "renderer.del =" + Del;
            }
            if (Link != null)
            {
                renderExtensions += "renderer.link =" + Link;
            }
            if (Image != null)
            {
                renderExtensions += "renderer.image =" + Image;
            }
            return renderExtensions;
        }
    }
}