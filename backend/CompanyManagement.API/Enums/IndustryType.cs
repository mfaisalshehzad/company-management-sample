using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using System.Runtime.Serialization;

namespace CompanyManagement.API.Enums
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum IndustryType
    {

        [EnumMember(Value = "Meat processing")]
        MeatProcessing,
        [EnumMember(Value = "Gardening and landscaping")]
        GardeningAndLandscaping,
        [EnumMember(Value = "IT services")]
        ITServices,
        [EnumMember(Value = "Aerospace technology")]
        AerospaceTechnology,
        [EnumMember(Value = "Consumer electronics")]
        ConsumerElectronics
    }
}
