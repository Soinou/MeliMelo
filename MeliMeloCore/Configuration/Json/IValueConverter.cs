﻿using MeliMelo.Core.Configuration.Values;
using MeliMelo.Utils;
using Newtonsoft.Json.Linq;

namespace MeliMelo.Core.Configuration.Json
{
    public class IValueConverter : JsonConverterBase<IValue>
    {
        protected override JObject Convert(object o)
        {
            JObject json = new JObject();

            (o as IValue).Serialize(json);

            return json;
        }

        protected override IValue Create(JObject json)
        {
            if (json["Path"] != null)
            {
                return new PathValue().Deserialize(json);
            }
            else if (json["String"] != null)
            {
                return new StringValue().Deserialize(json);
            }
            else
            {
                return new IntegerValue().Deserialize(json);
            }
        }
    }
}
