using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Models
{
    public interface PokedexError { }
    public struct HTTPError : PokedexError
    {
        public HttpStatusCode StatusCode;
        public HttpContent Content;

    }
    public struct SerialisingError : PokedexError
    {
        public Exception Exception { get; set; }
    }
    public struct NotFoundError : PokedexError { }
}
