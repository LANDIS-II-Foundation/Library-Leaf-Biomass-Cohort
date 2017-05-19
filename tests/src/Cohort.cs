//  Copyright 2005-2010 Portland State University, University of Wisconsin
//  Authors:  Robert M. Scheller, James B. Domingo


using Landis.Core;
using Landis.SpatialModeling;

namespace Landis.Library.LeafBiomassCohorts
{
    /// <summary>
    /// A species cohort with biomass information.
    /// </summary>
    public class Cohort
        : ICohort
        
    {
        private ISpecies species;
        private CohortData data;

        //---------------------------------------------------------------------

        public ISpecies Species
        {
            get {
                return species;
            }
        }

        //---------------------------------------------------------------------

        public ushort Age
        {
            get {
                return data.Age;
            }
        }

        //---------------------------------------------------------------------

        public float WoodBiomass
        {
            get {
                return data.WoodBiomass;
            }
        }

        //---------------------------------------------------------------------

        public float LeafBiomass
        {
            get {
                return data.LeafBiomass;
            }
        }
        // TEST ---------------------------------------------------------------------

        public int Biomass
        {
            get {
                return (int) (data.LeafBiomass + data.WoodBiomass);
            }
        }
        //---------------------------------------------------------------------
        public int ComputeNonWoodyBiomass(ActiveSite site)
        {
            return (int)(WoodBiomass);
        }
       
        /// <summary>
        /// The cohort's age and biomass data.
        /// </summary>
        public CohortData Data
        {
            get {
                return data;
            }
        }


        //---------------------------------------------------------------------

        public Cohort(ISpecies species,
                      ushort   age,
                      float   woodBiomass,
                      float   leafBiomass)
        {
            this.species = species;
            this.data.Age = age;
            this.data.WoodBiomass = woodBiomass;
            this.data.LeafBiomass = leafBiomass;
        }

        //---------------------------------------------------------------------

        public Cohort(ISpecies   species,
                      CohortData cohortData)
        {
            this.species = species;
            this.data = cohortData;
        }

        //---------------------------------------------------------------------
        /// <summary>
        /// Increments the cohort's age by one year.
        /// </summary>
        public void IncrementAge()
        {
            data.Age += 1;
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Changes the cohort's WOOD biomass.
        /// </summary>
        public void ChangeWoodBiomass(float delta)
        {
            float newBiomass = data.WoodBiomass + delta;
            data.WoodBiomass = (float) System.Math.Max(0.0, newBiomass);
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Changes the cohort's LEAF biomass.
        /// </summary>
        public void ChangeLeafBiomass(float delta)
        {
            float newBiomass = data.LeafBiomass + delta;
            data.LeafBiomass = (float) System.Math.Max(0.0, newBiomass);
        }
        //---------------------------------------------------------------------

        /// <summary>
        /// Occurs when a cohort dies either due to senescence or biomass
        /// disturbances.
        /// </summary>
        public static event DeathEventHandler<DeathEventArgs> DeathEvent;

        //---------------------------------------------------------------------

        /// <summary>
        /// Raises a Cohort.DeathEvent.
        /// </summary>
        public static void Died(object     sender,
                                ICohort    cohort,
                                ActiveSite site,
                                ExtensionType disturbanceType)
        {
            if (DeathEvent != null)
                DeathEvent(sender, new DeathEventArgs(cohort, site, disturbanceType));
        }

        //---------------------------------------------------------------------

        /// <summary>
        /// Occurs when a cohort is killed by an age-only disturbance.
        /// </summary>
        public static event DeathEventHandler<DeathEventArgs> AgeOnlyDeathEvent;

        //---------------------------------------------------------------------

        /// <summary>
        /// Raises a Cohort.AgeOnlyDeathEvent.
        /// </summary>
        public static void KilledByAgeOnlyDisturbance(object     sender,
                                                      ICohort    cohort,
                                                      ActiveSite site,
                                                      ExtensionType disturbanceType)
        {
            if (AgeOnlyDeathEvent != null)
                AgeOnlyDeathEvent(sender, new DeathEventArgs(cohort, site, disturbanceType));
        }
    }
}
