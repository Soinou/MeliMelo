#ifndef MELIMELO_ANIMES_MARSHAL_H_
#define MELIMELO_ANIMES_MARSHAL_H_

#include "vcclr.h"

namespace MeliMelo
{
    namespace Animes
    {
        namespace Marshal
        {
            // Converts a string to another type
            template <typename T>
            static T to(System::String^ str)
            {}

            // Converts a string to an array of wchar_t
            template<>
            static wchar_t* to(System::String^ str)
            {
                pin_ptr<const wchar_t> cpwc = PtrToStringChars(str);
                int len = str->Length + 1;
                wchar_t* pwc = new wchar_t[len];
                wcscpy_s(pwc, len, cpwc);
                return pwc;
            }
        }
    }
}

#endif // MELIMELO_ANIMES_MARSHAL_H_
