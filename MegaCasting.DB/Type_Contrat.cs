//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MegaCasting.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class Type_Contrat
    {
        public Type_Contrat()
        {
            this.Offres = new HashSet<Offre>();
        }
    
        public long Identifiant { get; set; }
        public string Libelle { get; set; }
    
        public virtual ICollection<Offre> Offres { get; set; }

        public override string ToString()
        {
            return this.Libelle;
        }
    }
}
