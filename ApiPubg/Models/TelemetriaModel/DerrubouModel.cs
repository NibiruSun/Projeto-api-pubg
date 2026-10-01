using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiPubg.Models.TelemetriaModel
{
    public partial class DerrubouModel : ObservableObject
   {
        private string? _name;
        public string? Name 
        { 
            get => _name;
            set => SetProperty(ref _name, value); 
        }
        private float _health;
        public float Health
        { 
            get => _health;
            set => SetProperty(ref _health, value );
        }
        private string? _damageReason;
        public string? DamageReason
        {
            get => _damageReason;
            set => SetProperty(ref _damageReason, value);
        }
        private string? _damageTypeCategory;
        public string? DamageTypeCategory
        {
            get => _damageTypeCategory;
            set => SetProperty(ref _damageTypeCategory, value); 
        }
        private string? _damageCauserName;
        public string? DamageCauserName 
        {
            get => _damageCauserName;
            set => SetProperty(ref _damageCauserName, value);
        }

        //public List<string>? AdditionalInfor { get; set; }
        private float _distance;
        public float Distance
        {
            get => _distance;
            set => SetProperty(ref _distance, value);
        }
    }
}
