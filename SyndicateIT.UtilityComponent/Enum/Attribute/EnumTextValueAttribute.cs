using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyndicateIT.UtilityComponent.Enum.Attribute
{
    [AttributeUsage(AttributeTargets.Field)]
    [Serializable()]
    public sealed class EnumTextValueAttribute : System.Attribute
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Gets the text2.
        /// </summary>
        public string Text2 { get; private set; }

        /// <summary>
        /// Gets or sets the resource manager.
        /// </summary>
        /// <value>
        /// The resource manager.
        /// </value>
        public Type ResourceManager { get; set; }


        /// <summary>
        /// Allows the creation of a friendly text representation of the enumeration.
        /// </summary>
        /// <param name="text">The reader friendly text to decorate the enum.</param>
        public EnumTextValueAttribute(string text, Type resourceManager)
        {
            Text = text;
            ResourceManager = resourceManager;
        }

        /// <summary>
        /// Allows the creation of a friendly text representation of the enumeration.
        /// </summary>
        /// <param name="text">The reader friendly text to decorate the enum.</param>
        public EnumTextValueAttribute(string text, string text2, Type resourceManager)
        {
            Text = text;
            Text2 = text2;
            ResourceManager = resourceManager;
        }
    }
}
