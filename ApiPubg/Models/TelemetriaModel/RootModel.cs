using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.TelemetriaModel
{
    public class RootModel
    {
        [JsonProperty("charcter")]
        public CharacterModel? Character { get; set; }

        [JsonProperty("victim")]
        public CharacterModel? Victim { get; set; }

        [JsonProperty("attacker")]
        public CharacterModel? Attacker { get; set; }

        [JsonProperty("victimGameResult")]
        public VictimGameResultModel? GameResult { get; set; }

        [JsonProperty("dBNOMaker")]
        public CharacterModel? Derrubou { get; set; }

        [JsonProperty("dBNODamageInfo")]
        public DBNODamageInfoModel? InforDerrubou { get; set; }

        [JsonProperty("finisher")]
        public CharacterModel? Finalizou { get; set; }

        [JsonProperty("finishDamageInfo")]
        public DBNODamageInfoModel? InforFinalizou { get; set; }

        [JsonProperty("_D")]
        public string? _D { get; set; }   // Data/hora do evento

        [JsonProperty("_T")]
        public string? _T { get; set; }   // Tipo do evento (ex: LogPlayerKill, LogItemPickup)

        public ItemModel? Item { get; set; }
        public VehicleModel? Vehicle { get; set; }
        public CommonModel? Common { get; set; }
        public int ElapsedTime { get; set; }
        public int NumAlivePlayers { get; set; }
        public List<ItemModel>? AttachedItems { get; set; }
    }
    // Eventos de combate.

    public class CharacterModel
    {
        public string? Name { get; set; }
        public int TeamId { get; set; }
        public float Health { get; set; }
        public Location? Location { get; set; }
        public string? AccountId { get; set; }
        public bool IsInBlueZone { get; set; }
        public bool IsInRedZone { get; set; }
    }
    public class VictimGameResultModel
    {
        public int TeamId { get; set; }
        public StatsModel? stats { get; set; }            
    }
    public class StatsModel
    {
        [JsonProperty("killCount")]
        public int KillCount { get; set; }

        [JsonProperty("distanceOnFoot")]
        public float DistanceAPe { get; set; }

        [JsonProperty("distanceOnSwim")]
        public float DistanceNadando { get; set; }

        [JsonProperty("distanceOnVehicle")]
        public float DistanceCarro { get; set; }

        [JsonProperty("distanceOnParachute")]
        public float DistanceParaquedas { get; set; }

        [JsonProperty("distanceOnFreefall")]
        public float DistanceQuedaLivre { get; set; }
    }

    public class DBNODamageInfoModel
    {
        [JsonProperty("damageReason")]
        public string? DamageReason { get; set; }

        [JsonProperty("damageTypeCategory")]
        public string? DamageTypeCategory { get; set; }

        [JsonProperty("damageCauserName")]
        public string? DamageCauserName { get; set; }

        [JsonProperty("additionalInfo")]
        public List<string>? AdditionalInfor { get; set; }

        [JsonProperty("distance")]
        public float Distanece { get; set; }
    }
    public class ItemModel
    {
        public string? ItemId { get; set; }
        public int StackCount { get; set; }
        public string? Equipment { get; set; }
        public string? SubCategory { get; set; }
    }
    public class VehicleModel
    {
        [JsonProperty("vehicleType")]
        public string? VehicleType { get; set; }

        [JsonProperty("vehicleId")]
        public string? VehicleId { get; set; }

        [JsonProperty("seatIndex")]
        public int SeatIndex { get; set; }

        [JsonProperty("healthPercent")]
        public double HealthPercent { get; set; }

        [JsonProperty("fuelPercent")]
        public double FuelPercent { get; set; }

        [JsonProperty("altitudeAbs")]
        public double AltitudeAbs { get; set; }

        [JsonProperty("altitudeRel")]
        public double AltitudeRel { get; set; }

        [JsonProperty("velocity")]
        public double Velocity { get; set; }

        [JsonProperty("isWheelsInAir")]
        public bool IsWheelsInAir { get; set; }

        [JsonProperty("isInWaterVolume")]
        public bool IsInWaterVolume { get; set; }

        [JsonProperty("isEngineOn")]
        public bool IsEngineOn { get; set; }

        [JsonProperty("location")]
        public Location? Location { get; set; }
    }

    public class Location
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }

    public class CommonModel
    {
        [JsonProperty("isGame")]
        public double IsGame { get; set; }   // valor float como 0.1 ou 1.0
    }

    public class PlayerKillEvent : CharacterModel;

        //[JsonProperty("_T")]
        //public string? EventType { get; set; } // "LogPlayerKill"

        //public CharacterModel? Killer { get; set; }
        //public CharacterModel? Victim { get; set; }

        //public string? DamageCauserName { get; set; }
        //public string? DamageReason { get; set; }
        //public double Distance { get; set; }

    public class PlayerDamageEvent : CharacterModel;

    //[JsonProperty("_T")]
    //public string? EventType { get; set; } // "LogPlayerTakeDamage"

    //public CharacterModel? Attacker { get; set; }
    //public CharacterModel? Victim { get; set; }

    //public double Damage { get; set; }
    //public string? DamageTypeCategory { get; set; }
    //public string? DamageCauserName { get; set; }


    //    public CharacterModel? Character { get; set; }
    //    public ItemModel? Item { get; set; }

    //    public VehicleModel? Vehicle { get; set; }
    //    public string? _D { get; set; }
    //    public string? _T { get; set; }

    //}

    //public class CharacterModel
    //{
    //    public string? Name { get; set; }
    //    public int teamId { get; set; }
    //    public float health { get; set; }
    //    public Location? location { get; set; }
    //}

    //public class ItemModel
    //{
    //    public string? ItemId { get; set; }
    //    public int StackCount { get; set; }
    //    public string? Equipment { get; set; }
    //    public string? SubCategory { get; set; }
    //}
    //public class VehicleModel
    //{
    //    [JsonProperty("vehicleType")]
    //    public string? VehicleType { get; set; }

    //    [JsonProperty("vehicleId")]
    //    public string? VehicleId { get; set; }

    //    [JsonProperty("seatIndex")]
    //    public int SeatIndex { get; set; }

    //    // aceitar decimais
    //    [JsonProperty("healthPercent")]
    //    public double HealthPercent { get; set; }

    //    [JsonProperty("fuelPercent")]
    //    public double FuelPercent { get; set; }

    //    [JsonProperty("altitudeAbs")]
    //    public double AltitudeAbs { get; set; }

    //    [JsonProperty("altitudeRel")]
    //    public double AltitudeRel { get; set; }

    //    [JsonProperty("velocity")]
    //    public double Velocity { get; set; }

    //    [JsonProperty("isWheelsInAir")]
    //    public bool IsWheelsInAir { get; set; }

    //    [JsonProperty("isInWaterVolume")]
    //    public bool IsInWaterVolume { get; set; }

    //    [JsonProperty("isEngineOn")]
    //    public bool IsEngineOn { get; set; }

    //    [JsonProperty("location")]
    //    public Location? Location { get; set; }
    //}
    //public class Location
    //{
    //    public double x { get; set; }
    //    public double y { get; set; }
    //    public double z { get; set; }
}

