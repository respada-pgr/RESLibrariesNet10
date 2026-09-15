using System.Text;

namespace _1_LibraryClassesNet10.Url
{
    public class C_Url
    {
        public enum E_UrlType { ND = -1, File, Web }

        private readonly Uri _uri;
        private readonly string[] _hostParts = null;

        public Uri Uri => this._uri;
        public string AbsoluteUrl => this._uri.AbsoluteUri;
        public string RelativeUrl => this._uri.PathAndQuery + _uri.Fragment;
        public string LocalPath => this._uri.LocalPath;
        public string Protocol => this._uri.Scheme;
        public int Port => this._uri.Port;
        public string Path => this._uri.AbsolutePath;
        public string Query => this._uri.Query;
        public string Fragment => this._uri.Fragment;
        public string Host => this._uri.Host;
        public string[] HostParts => _hostParts;
        public string HostWithoutWww => this._uri.Host.StartsWith("www.") ? this._uri.Host.Substring(4) : this._uri.Host;
        public string HostWithoutWwwAndSubdomain 
        {
            get
            {
                string result = this.HostWithoutWww;
                string[] hostParts = result.Split('.');
                result = hostParts.Length > 2 ? $"{_hostParts[_hostParts.Length - 2]}.{_hostParts[_hostParts.Length - 1]}" : result;
                return result;
            }            
        }
        public string Supdomain => _hostParts.Length > 2 ? _hostParts[0] : string.Empty;
        public string Domain => _hostParts.Length > 2 ? _hostParts[1] : _hostParts[0];
        public string TLD => this._hostParts.Length > 1 ? this._hostParts[_hostParts.Length - 1] : string.Empty;
        public E_UrlType Type
        {
            get
            {
                string strType = _uri.Scheme.ToLower();
                switch (strType)
                {
                    case "file":
                        return E_UrlType.File;
                    case "http":
                    case "https":
                        return E_UrlType.Web;
                    default:
                        return E_UrlType.ND;
                }
            }
        }

        public C_Url(string url)
        {
            if (url == null)
                throw new ArgumentNullException("The url parameter must not be null.");

            if (!Uri.TryCreate(url, UriKind.Absolute, out _uri))
                throw new ArgumentException("Url invalid", nameof(url));
            this._hostParts = this._uri.Host.Split('.');
        }

        public C_Url(Uri uri)
        {
            this._uri = uri ?? throw new ArgumentNullException(nameof(uri), "The uri parameter must not be null.");
            this._hostParts = this._uri.Host.Split('.');
        }

        public C_Url(C_Url url)
        {
            if (url == null)
                throw new ArgumentNullException(nameof(url), "The url parameter must not be null.");

            this._uri = url.Uri;
            if (this._uri != null)
                this._hostParts = this._uri.Host.Split('.');
        }

        public override string ToString() => AbsoluteUrl;

        public string Info()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine($"Type: {this.Type.ToString()}");
            result.AppendLine($"Absolute Url: {this.AbsoluteUrl}");
            result.AppendLine($"Host: {this.Host}");
            result.AppendLine($"Host without www: {this.HostWithoutWww}");
            result.AppendLine($"Host without www and subdomain: {this.HostWithoutWwwAndSubdomain}");
            result.AppendLine($"Protocol: {this.Protocol}");
            result.AppendLine($"Subdomain: {this.Supdomain}");
            result.AppendLine($"Domain: {this.Domain}");
            result.AppendLine($"TLD: {this.TLD}");
            result.AppendLine($"Port: {this.Port}");
            result.AppendLine($"Relative Url: {this.RelativeUrl}");
            result.AppendLine($"Path: {this.Path}");
            result.AppendLine($"Query: {this.Query}");
            result.AppendLine($"Fragment: {this.Fragment}");
            result.AppendLine($"Local path: {this.LocalPath}");
            return result.ToString();
        }
    }
}
