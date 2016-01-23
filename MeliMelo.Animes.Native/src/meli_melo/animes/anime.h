#ifndef MELIMELO_ANIMES_ANIME_H_
#define MELIMELO_ANIMES_ANIME_H_

#include "anitomy/element.h"
#include "windows.h"

namespace MeliMelo
{
    namespace Animes
    {
        // Anime class
        public ref class Anime
        {
        private:
            // The underlying elements holder
            anitomy::Elements* elements_;

        public:
            // Constructor
            Anime(const anitomy::Elements& elements);

            // Constructor
            Anime(System::String^ file_name);

            // Destructor
            virtual ~Anime();

            // All the getters
            bool IsValid();
            System::String^ AnimeSeason();
            System::String^ AnimeSeasonPrefix();
            System::String^ AnimeTitle();
            System::String^ AnimeType();
            System::String^ AnimeYear();
            System::String^ AudioTerm();
            System::String^ DeviceCompatibility();
            System::String^ EpisodeNumber();
            System::String^ EpisodePrefix();
            System::String^ FileChecksum();
            System::String^ FileExtension();
            System::String^ FileName();
            System::String^ Language();
            System::String^ Other();
            System::String^ ReleaseGroup();
            System::String^ ReleaseInformation();
            System::String^ ReleaseVersion();
            System::String^ Source();
            System::String^ Subtitles();
            System::String^ VideoResolution();
            System::String^ VideoTerm();
        };
    }
}

#endif // MELIMELO_ANIMES_ANIME_H_
