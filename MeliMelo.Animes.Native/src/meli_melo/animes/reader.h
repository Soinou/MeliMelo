#ifndef MELIMELO_ANIMES_READER_H_
#define MELIMELO_ANIMES_READER_H_

#include "anitomy/anitomy.h"
#include "anime.h"
#include "windows.h"

namespace MeliMelo
{
    namespace Animes
    {
        // Wrapper around anitomy parser
        public ref class Reader
        {
        private:
            // The underlying parser
            anitomy::Anitomy* parser_;

        public:
            // Constructor
            Reader();

            // Destructor
            virtual ~Reader();

            // Reads the given string into an anime entry
            Anime^ Read(System::String^ string);
        };
    }
}

#endif // MELIMELO_ANIMES_READER_H_
