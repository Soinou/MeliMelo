#include "meli_melo/screen/Temperature.h"

#include "meli_melo/screen/Redshift.h"

#include <Windows.h>

namespace MeliMelo
{
    namespace Screen
    {
        const int NeutralTemp = 6500;
        const int GammaRampSize = 256;

        bool Temperature::Set(int temperature)
        {
            if (temperature < 1100 || temperature > 25100)
                return false;

            HDC device = GetDC(nullptr);

            if (device == nullptr)
                return false;

            WORD ramp[3 * GammaRampSize];

            WORD* red = &ramp[0 * GammaRampSize];
            WORD* green = &ramp[1 * GammaRampSize];
            WORD* blue = &ramp[2 * GammaRampSize];

            for (int i = 0; i < GammaRampSize; i++)
            {
                WORD value = static_cast<WORD>(static_cast<double>(i) / GammaRampSize * 65536);
                red[i] = value;
                green[i] = value;
                blue[i] = value;
            }

            Redshift::color_ramp_fill(red, green, blue, GammaRampSize, temperature);

            bool result = SetDeviceGammaRamp(device, ramp) != 0;

            ReleaseDC(nullptr, device);

            return result;
        }

        bool Temperature::Reset()
        {
            return Set(NeutralTemp);
        }
    }
}
