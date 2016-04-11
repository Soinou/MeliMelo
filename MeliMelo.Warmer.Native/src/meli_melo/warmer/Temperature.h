#ifndef MELI_MELO_WARMER_TEMPERATURE_H_
#define MELI_MELO_WARMER_TEMPERATURE_H_

#include <Windows.h>

using namespace System;

namespace MeliMelo
{
    namespace Warmer
    {
        /// \brief Represents the screen temperature
        public ref class Temperature abstract sealed
        {
        public:
            /// \brief Changes the screen temperature
            ///
            /// \param temperature New screen temperature
            /// \return If the temperature could be changed
            static bool Set(int temperature);

            /// \brief Resets the screen temperature to the default
            ///
            /// \return If the temperature could be changed
            static bool Reset();
        };
    }
}

#endif // MELI_MELO_WARMER_TEMPERATURE_H_
