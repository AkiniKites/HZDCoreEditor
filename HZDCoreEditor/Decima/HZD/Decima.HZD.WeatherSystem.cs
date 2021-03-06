﻿namespace Decima.HZD
{
    [RTTI.Serializable(0x72F1332BB459B417, GameType.HZD)]
    public class WeatherSystem : CoreObject
    {
        [RTTI.Member(3, 0x160, "General")] public Ref<RenderEffectResource> SimulationRenderEffectResource;
        [RTTI.Member(4, 0x1D8, "General")] public Array<WindSimulationForceField> WindSimulationForceFields;
        [RTTI.Member(5, 0x230, "General")] public BoundingBox3 WorldBounds;
        [RTTI.Member(6, 0x250, "General")] public FRange TemperatureRange;
        [RTTI.Member(7, 0x354, "General")] public float WetnessDryingTime;
        [RTTI.Member(8, 0x358, "General")] public float WetnessSaturationTime;
        public BaseGGUUID UnknownSaveGUID;

        public void ReadSave(SaveState state)
        {
            if (state.SaveVersion > 15)
                UnknownSaveGUID = state.ReadIndexedGUID();
        }
    }
}