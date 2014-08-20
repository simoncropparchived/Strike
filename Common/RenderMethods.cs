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
                renderExtensions += "renderer.code =" + Code + "\r\n";
            }
            if (BlockQuote != null)
            {
                renderExtensions += "renderer.blockquote =" + BlockQuote + "\r\n";
            }
            if (Heading != null)
            {
                renderExtensions += "renderer.heading =" + Heading + "\r\n";
            }
            if (Hr != null)
            {
                renderExtensions += "renderer.hr =" + Hr + "\r\n";
            }
            if (Html != null)
            {
                renderExtensions += "renderer.html =" + Html + "\r\n";
            }
            if (List != null)
            {
                renderExtensions += "renderer.list =" + List + "\r\n";
            }
            if (ListItem != null)
            {
                renderExtensions += "renderer.listitem =" + ListItem + "\r\n";
            }
            if (Paragraph != null)
            {
                renderExtensions += "renderer.paragraph =" + Paragraph + "\r\n";
            }
            if (Table != null)
            {
                renderExtensions += "renderer.table =" + Table + "\r\n";
            }
            if (TableRow != null)
            {
                renderExtensions += "renderer.tablerow =" + TableRow;
            }
            if (TableCell != null)
            {
                renderExtensions += "renderer.tablecell =" + TableCell + "\r\n";
            }
            if (Strong != null)
            {
                renderExtensions += "renderer.strong =" + Strong + "\r\n";
            }
            if (Em != null)
            {
                renderExtensions += "renderer.em =" + Em + "\r\n";
            }
            if (Codespan != null)
            {
                renderExtensions += "renderer.codespan =" + Codespan + "\r\n";
            }
            if (Br != null)
            {
                renderExtensions += "renderer.br =" + Br + "\r\n";
            }
            if (Del != null)
            {
                renderExtensions += "renderer.del =" + Del + "\r\n";
            }
            if (Link != null)
            {
                renderExtensions += "renderer.link =" + Link + "\r\n";
            }
            if (Image != null)
            {
                renderExtensions += "renderer.image =" + Image + "\r\n";
            }
            return renderExtensions;
        }
    }
}