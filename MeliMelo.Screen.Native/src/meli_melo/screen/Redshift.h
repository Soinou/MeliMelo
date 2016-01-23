#ifndef MELIMELO_SCREEN_REDSHIFT_H_
#define MELIMELO_SCREEN_REDSHIFT_H_

#include <cstdint>

namespace MeliMelo
{
    namespace Screen
    {
        namespace Redshift
        {
            /// \brief Fills a color ramp with the given temperature
            ///
            /// \param gamma_r Red gammas array
            /// \param gamma_g Green gammas array
            /// \param gamma_b Blue gammas array
            /// \param size Size of the arrays
            /// \param temperature Temperature to fill with
            void color_ramp_fill(uint16_t* gamma_r, uint16_t* gamma_g, uint16_t* gamma_b, int size,
                                 int temperature);
        }
    }
}

#endif // MELIMELO_SCREEN_REDSHIFT_H_
