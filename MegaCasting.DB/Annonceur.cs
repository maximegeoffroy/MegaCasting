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
    
    public partial class Annonceur : Societe
    {
        public Annonceur()
        {
            this.Offres = new HashSet<Offre>();
        }
    
        public Nullable<int> NbAnnonces { get; set; }
    
        public virtual ICollection<Offre> Offres { get; set; }
    }
}