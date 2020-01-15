﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindEnergy
{
    public static class Constants
    {
        /// <summary>
        /// мм рт. ст. в одном КПа
        /// </summary>
        public const double MMHGART_IN_1KPA = 7.50063755419211;


        #region Физические константы в единицах СИ

        /// <summary>
        /// Boltzman Constant. Units erg/deg(K) 
        /// </summary>
        public const double BOLTZMAN = 1.3807e-16;

        /// <summary>
        /// Elementary Charge. Units statcoulomb 
        /// </summary>
        public const double ECHARGE = 4.8032e-10;

        /// <summary>
        /// Electron Mass. Units g 
        /// </summary>
        public const double EMASS = 9.1095e-28;

        /// <summary>
        /// Proton Mass. Units g 
        /// </summary>
        public const double PMASS = 1.6726e-24;

        /// <summary>
        /// Gravitational Constant. Units dyne-cm^2/g^2
        /// </summary>
        public const double GRAV = 6.6720e-08;

        /// <summary>
        /// Planck constant. Units erg-sec 
        /// </summary>
        public const double PLANCK = 6.6262e-27;

        /// <summary>
        /// Speed of Light in a Vacuum. Units cm/sec 
        /// </summary>
        public const double LIGHTSPEED = 2.9979e10;

        /// <summary>
        /// Stefan-Boltzman Constant. Units erg/cm^2-sec-deg^4 
        /// </summary>
        public const double STEFANBOLTZ = 5.6703e-5;

        /// <summary>
        /// Avogadro Number. Units  1/mol 
        /// </summary>
        public const double AVOGADRO = 6.0220e23;

        /// <summary>
        /// Gas Constant. Units erg/deg-mol 
        /// </summary>
        public const double GASCONSTANT = 8.3144e07;

        /// <summary>
        /// Gravitational Acceleration at the Earths surface. Units cm/sec^2 
        /// </summary>
        public const double GRAVACC = 980.67;

        /// <summary>
        /// Solar Mass. Units g 
        /// </summary>
        public const double SOLARMASS = 1.99e33;

        /// <summary>
        /// Solar Radius. Units cm
        /// </summary>
        public const double SOLARRADIUS = 6.96e10;

        /// <summary>
        /// Solar Luminosity. Units erg/sec
        /// </summary>
        public const double SOLARLUM = 3.90e33;

        /// <summary>
        /// Solar Flux. Units erg/cm^2-sec
        /// </summary>
        public const double SOLARFLUX = 6.41e10;

        /// <summary>
        /// Astronomical Unit (radius of the Earth's orbit). Units cm
        /// </summary>
        public const double AU = 1.50e13;

        #endregion
    }
}
