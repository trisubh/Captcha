using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;



namespace Captacha
{
    public class CaptchaElement : ConfigurationElement
    {
        [ConfigurationProperty("id", IsRequired = true, IsKey = true)]
        public int Id
        {
            get { return (int)base["id"]; }
        }

        [ConfigurationProperty("mode", IsRequired = false)]
        public string mode
        {
            get { return (string)base["mode"]; }
        }

        [ConfigurationProperty("strict", IsRequired = false)]
        public string strict
        {
            get { return (string)base["strict"]; }
        }
    }

    [ConfigurationCollection(typeof(CaptchaElement))]
    public class CaptchaElementCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "Captcha";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName,
              StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new CaptchaElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((CaptchaElement)(element)).Id;
        }

        public CaptchaElement this[int idx]
        {
            get { return (CaptchaElement)BaseGet(idx); }
        }
    }



    public class CaptchaSection : ConfigurationSection
    {
        [ConfigurationProperty("Captchas")]
        public CaptchaElementCollection Captchas
        {
            get { return ((CaptchaElementCollection)(base["Captchas"])); }
            set { base["Captchas"] = value; }
        }
    }


}