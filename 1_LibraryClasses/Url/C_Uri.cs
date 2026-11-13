using _1_LibraryClasses.File;
using System.Text;

using _1_LibraryClasses;
namespace _1_LibraryClasses.Url
{

    public class C_Uri : I_Infoable
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

        public C_File File { get => this._url.Type == C_Url.E_UrlType.File ? new C_File(this._url.Uri) : null; }

        #region CONSTRUCTORS

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

        /// <summary>
        /// Creates a URI wrapper from an OS-agnostic <see cref="C_StringPath"/>.
        /// <para><i>[ES] Crea un wrapper URI desde un <see cref="C_StringPath"/>.</i></para>
        /// </summary>
        public C_Uri(C_StringPath stringPath)
            : this(stringPath?.ToUri() ?? throw new ArgumentNullException(nameof(stringPath)))
        {
        }

        public C_Uri(C_Uri? source)
            : this(source is null ? string.Empty : source.ToString())
        {
        }

        #endregion


        #region PUBLIC METHODS

        public C_Uri Clone()
            => new C_Uri(this);

        public string Info()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}

