#include "anime_parser.h"

#include "marshal.h"

namespace MeliMelo
{
    namespace Animes
    {
        AnimeParser::AnimeParser() : parser_(new anitomy::Anitomy())
        {}

        AnimeParser::~AnimeParser()
        {
            delete parser_;
        }

        Anime^ AnimeParser::Read(System::String^ string)
        {
            // Parse the given string
            parser_->Parse(Marshal::to<wchar_t*>(string));

            // Get all the elements
            auto& elements = parser_->elements();

            // Return a new anime entry with these elements
            return gcnew Anime(elements);
        }
    }
}
