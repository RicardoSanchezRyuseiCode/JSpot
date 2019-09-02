using Ryusei.JSpot.Auth.Provider.Fty.Section;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ryusei.JSpot.Auth.Provider.Fty
{
    /// <summary>
    /// Name: ProviderBuilder
    /// Description: Class to define element of section in configuration
    /// Author: Ricardo Sanchez Romero (ricardo.sanchez@ryuseicode.com)
    /// LogBook:
    ///     2019-08-26: Creation
    /// </summary>
    public class ProviderBuilder
    {
        #region [Constants]
        private const string SECTION_NAME = "providerSection";
        public const string IAUTHAUTHORIZATIONSERVERPROVIDER = "IAuthAuthorizationServerProvider";
        public const string IAUTHENTICATIONTOKENPROVIDER = "IAuthenticationTokenProvider";
        #endregion

        #region [Static Attributes]
        /// <summary>
        /// Singleton
        /// </summary>
        private static ProviderBuilder Singleton { get; set; }
        #endregion

        #region [Attributes]
        /// <summary>
        /// AuthSection
        /// </summary>
        private ProviderSection AuthSection { get; set; }
        #endregion

        #region [Static constructor]
        /// <summary>
        /// Static constructor
        /// </summary>
        static ProviderBuilder()
        {
            Singleton = null;
        }
        #endregion

        #region [Constructor]
        /// <summary>
        /// Default constructor
        /// </summary>
        public ProviderBuilder()
        {
            this.AuthSection = ConfigurationManager.GetSection(SECTION_NAME) as ProviderSection;
        }
        #endregion

        #region [Static Methods]
        /// <summary>
        /// Name: GetInstance
        /// Description: Method to get instance of auth builder
        /// </summary>
        /// <returns></returns>
        public static ProviderBuilder GetInstance()
        {
            return Singleton == null ? Singleton = new ProviderBuilder() : Singleton;
        }
        #endregion

        #region [Methods]
        /// <summary>
        /// Name: Method to build a manager for oauth
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public T GetProvider<T>(string providerKey)
        {
            string name = this.AuthSection.Instances[providerKey].Type;
            Type type = Type.GetType(name);
            if (type != null)
                return (T)Activator.CreateInstance(type);
            foreach (var asm in AppDomain.CurrentDomain.GetAssemblies())
            {
                type = asm.GetType(name);
                if (type != null)
                    return (T)Activator.CreateInstance(type);
            }
            return default(T);
        }
        #endregion
    }
}
