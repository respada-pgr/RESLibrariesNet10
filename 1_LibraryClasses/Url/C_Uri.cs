using _1_LibraryClassesNet10.Files;
using System.Text;

namespace _1_LibraryClassesNet10.Url
{
    
    public class C_Uri
    {
        public enum E_UriType { ND = -1, File, Web }

        private C_Url _url;

        public Uri Uri { get => this._url.Uri; }

        public E_UriType Type
        {
            get
            {
                E_UriType result = E_UriType.ND;
                switch (this._url.Type)
                {
                    case C_Url.E_UrlType.File:
                        result = E_UriType.File;
                        break;
                    case C_Url.E_UrlType.Web:
                        result = E_UriType.Web;
                        break;
                }
                return result;
            }
        }

        public C_Url Url { get => this._url.Type == C_Url.E_UrlType.Web ? this._url : null; }

        public C_FilePath File { get => this._url.Type == C_Url.E_UrlType.File ? new C_FilePath(this._url.Uri) : null; }

        public C_Uri()
        {
        }

        public C_Uri(string uri)
        {
            this._url = new C_Url(uri);
        }

        public C_Uri(Uri uri)
        {
            this._url = new C_Url(uri);
        }

        public string Info()
        {
            StringBuilder result = new StringBuilder();
            switch (this.Type)
            {
                case E_UriType.Web:
                    result.AppendLine(this.Url.Info());
                    break;
                case E_UriType.File:
                    result.AppendLine($"Type: {this.Type.ToString()}");
                    result.AppendLine(this.File.Info());
                    break;
                default:
                    result.AppendLine($"Type: {this.Type.ToString()}");
                    break;
            }
            return result.ToString();
        }
    }
}
