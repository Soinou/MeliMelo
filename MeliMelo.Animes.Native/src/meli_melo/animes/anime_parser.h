#ifndef MELIMELO_ANIMES_ANIME_PARSER_H_
#define MELIMELO_ANIMES_ANIME_PARSER_H_

#include "anitomy/anitomy.h"
#include "anime.h"
#include "windows.h"

namespace MeliMelo
{
    namespace Animes
    {
        // Wrapper around anitomy parser
        public ref class AnimeParser
        {
        private:
            // The underlying parser
            anitomy::Anitomy* parser_;

        public:
            // Constructor
            AnimeParser();

            // Destructor
            virtual ~AnimeParser();

            // Reads the given string into an anime entry
            Anime^ Read(System::String^ string);
        };
    }
}

#endif // MELIMELO_ANIMES_ANIME_PARSER_H_
