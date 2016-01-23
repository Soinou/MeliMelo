#include "reader.h"

#include "marshal.h"

namespace MeliMelo
{
    namespace Animes
    {
        Reader::Reader() : parser_(new anitomy::Anitomy())
        {}

        Reader::~Reader()
        {
            delete parser_;
        }

        Anime^ Reader::Read(System::String^ string)
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
