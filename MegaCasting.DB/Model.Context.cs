﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MegaCastingEntities : DbContext
    {
        public MegaCastingEntities()
            : base("name=MegaCastingEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Domaine_Metier> Domaine_Metier { get; set; }
        public virtual DbSet<Metier> Metiers { get; set; }
        public virtual DbSet<Offre> Offres { get; set; }
        public virtual DbSet<Societe> Societes { get; set; }
        public virtual DbSet<Type_Contrat> Type_Contrat { get; set; }
        public virtual DbSet<Adresse> Adresses { get; set; }
    }
}