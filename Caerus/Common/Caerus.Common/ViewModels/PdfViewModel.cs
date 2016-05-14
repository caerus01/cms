namespace Caerus.Common.ViewModels
{
    public class PdfViewModel :ReplyObject
    {
        public byte[] Attachment { get; set; }


        /// <summary>
        /// Gets or sets the attachment name (i.e. myattachment.pdf).
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
        /// <summary>
        /// Set the path if attachment stored somewhere on the server
        /// </summary>
        /// <value>
        /// The path.
        /// </value>
        public string Path { get; set; }
    }
}
