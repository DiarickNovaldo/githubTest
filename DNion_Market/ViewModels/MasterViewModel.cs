using DNion_Market.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DNion_Market.ViewModels
{
    public class MasterViewModel
    {
        private static dnEntities dn = new dnEntities();

        [Key]
        public int ID { get; set; }
        public string NAME { get; set; }
        public string EMAIL { get; set; }
        public string ADDRESS { get; set; }
        public string GENDER { get; set; }
        public DateTime? TGL_LAHIR { get; set; }

        public static IEnumerable<DNion_Market.ViewModels.MasterViewModel> getCustomers()
        {
            var query = from a in dn.Customers
                        select new MasterViewModel
                        {
                            ID = a.id,
                            NAME = a.name,
                            EMAIL = a.email,
                            ADDRESS = a.address,
                            GENDER = a.gender,
                            TGL_LAHIR = a.tanggal_lahir
                        };

            return query.ToList().OrderBy(a => a.ID);
        }
    }
}