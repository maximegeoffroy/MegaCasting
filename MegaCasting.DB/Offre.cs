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
    
    public partial class Offre
    {
        public long Identifiant { get; set; }
        public string Intitule { get; set; }
        public string Reference { get; set; }
        public System.DateTime DatePublication { get; set; }
        public long DureeDiffusion { get; set; }
        public System.DateTime DateDebutContrat { get; set; }
        public long NbPostes { get; set; }
        public string Localisation { get; set; }
        public string DescriptionPoste { get; set; }
        public string DescriptionProfil { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public long IdentifiantType_Contrat { get; set; }
        public long IdentifiantMetier { get; set; }
        public long IdentifiantDomaine_Metier { get; set; }
        public long IdentifiantAnnonceur { get; set; }
    
        public virtual Annonceur Annonceur { get; set; }
        public virtual Domaine_Metier Domaine_Metier { get; set; }
        public virtual Metier Metier { get; set; }
        public virtual Type_Contrat Type_Contrat { get; set; }

        public override string ToString()
        {
            return this.Intitule;
        }
    }
}
