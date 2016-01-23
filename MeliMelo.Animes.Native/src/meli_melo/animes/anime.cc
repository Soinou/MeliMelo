#include "anime.h"

#include "anitomy/anitomy.h"
#include "marshal.h"

using anitomy::Anitomy;
using anitomy::Elements;

namespace MeliMelo
{
    namespace Animes
    {
        Anime::Anime(const anitomy::Elements& elements) : elements_(new anitomy::Elements(elements))
        {}

        Anime::Anime(System::String^ file_name) : elements_(nullptr)
        {
            // Create a new anitomy parser
            anitomy::Anitomy parser;

            // Parse the given file name
            parser.Parse(Marshal::to<wchar_t*>(file_name));

            // Copy the elements we got
            elements_ = new anitomy::Elements(parser.elements());
        }

        Anime::~Anime()
        {
            delete elements_;
        }

        bool Anime::IsValid()
        {
            // If the episode number is empty or the release group is empty, the anime is not valid
            return elements_->get(anitomy::kElementEpisodeNumber).empty()
                || elements_->get(anitomy::kElementReleaseGroup).empty();
        }

        System::String^ Anime::AnimeSeason()
        {
            return gcnew System::String(elements_->get(anitomy::kElementAnimeSeason).c_str());
        }

        System::String^ Anime::AnimeSeasonPrefix()
        {
            return gcnew System::String(elements_->get(anitomy::kElementAnimeSeasonPrefix).c_str());
        }

        System::String^ Anime::AnimeTitle()
        {
            return gcnew System::String(elements_->get(anitomy::kElementAnimeTitle).c_str());
        }

        System::String^ Anime::AnimeType()
        {
            return gcnew System::String(elements_->get(anitomy::kElementAnimeType).c_str());
        }

        System::String^ Anime::AnimeYear()
        {
            return gcnew System::String(elements_->get(anitomy::kElementAnimeYear).c_str());
        }

        System::String^ Anime::AudioTerm()
        {
            return gcnew System::String(elements_->get(anitomy::kElementAudioTerm).c_str());
        }

        System::String^ Anime::DeviceCompatibility()
        {
            return gcnew System::String(elements_->get(anitomy::kElementDeviceCompatibility).c_str());
        }

        System::String^ Anime::EpisodeNumber()
        {
            return gcnew System::String(elements_->get(anitomy::kElementEpisodeNumber).c_str());
        }

        System::String^ Anime::EpisodePrefix()
        {
            return gcnew System::String(elements_->get(anitomy::kElementEpisodePrefix).c_str());
        }

        System::String^ Anime::FileChecksum()
        {
            return gcnew System::String(elements_->get(anitomy::kElementFileChecksum).c_str());
        }

        System::String^ Anime::FileExtension()
        {
            return gcnew System::String(elements_->get(anitomy::kElementFileExtension).c_str());
        }

        System::String^ Anime::FileName()
        {
            return gcnew System::String(elements_->get(anitomy::kElementFileName).c_str());
        }

        System::String^ Anime::Language()
        {
            return gcnew System::String(elements_->get(anitomy::kElementLanguage).c_str());
        }

        System::String^ Anime::Other()
        {
            return gcnew System::String(elements_->get(anitomy::kElementOther).c_str());
        }

        System::String^ Anime::ReleaseGroup()
        {
            return gcnew System::String(elements_->get(anitomy::kElementReleaseGroup).c_str());
        }

        System::String^ Anime::ReleaseInformation()
        {
            return gcnew System::String(elements_->get(anitomy::kElementReleaseInformation).c_str());
        }

        System::String^ Anime::ReleaseVersion()
        {
            return gcnew System::String(elements_->get(anitomy::kElementReleaseVersion).c_str());
        }

        System::String^ Anime::Source()
        {
            return gcnew System::String(elements_->get(anitomy::kElementSource).c_str());
        }

        System::String^ Anime::Subtitles()
        {
            return gcnew System::String(elements_->get(anitomy::kElementSubtitles).c_str());
        }

        System::String^ Anime::VideoResolution()
        {
            return gcnew System::String(elements_->get(anitomy::kElementVideoResolution).c_str());
        }

        System::String^ Anime::VideoTerm()
        {
            return gcnew System::String(elements_->get(anitomy::kElementVideoTerm).c_str());
        }
    }
}
