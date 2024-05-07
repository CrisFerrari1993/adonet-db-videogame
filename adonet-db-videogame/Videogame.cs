using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public class Videogame
    {
        private string _name;
        private string _overview;
        private DateTime _relase_date;
        private DateTime _create_at;
        private DateTime _updated_at;
        private int _software_house_id;

        public string Name {
            get => _name;
            set 
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new Exception("Il titolo deve contenere almeno un carattere");
                }
                _name = value;
            } 
        }
        public string Overview {
            get => _overview;
            set
            {
                _overview = value;
            }
        }

        public DateTime Relase_date { get; set; }
        public DateTime Create_at { get; set; }
        public DateTime Update_at { get; set;}
        public int Software_house_id 
        {
            get => _software_house_id;
            set 
            {
                if(value <= 0)
                {
                    throw new Exception("Non è possibile inserire un id negativo o zero");
                }
                _software_house_id = value;
            }
        }

        public Videogame(string nome, string overview, DateTime relase, DateTime create_at, DateTime update_at, int softwareHouseId)
        {
            Name = nome;
            Overview = overview;
            Relase_date = relase;
            Create_at = create_at;
            Update_at = update_at;
            Software_house_id = softwareHouseId;
        }
    }
}
