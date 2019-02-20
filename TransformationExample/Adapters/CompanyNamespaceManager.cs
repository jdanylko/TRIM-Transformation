using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace TransformationExample.Adapters
{
    public class CompanyNamespaceManager
    {
        private readonly XmlNamespaceManager _mgr = new XmlNamespaceManager(new NameTable());

        public CompanyNamespaceManager(NameValueCollection nameValueCollection)
        {
            foreach (string item in nameValueCollection)
            {
                AddNamespace(item, nameValueCollection[item]);
            }
        }

        public void AddNamespace(string prefix, string ns)
        {
            _mgr.AddNamespace(prefix, ns);
        }

        public void RemoveNamespace(string prefix, string ns)
        {
            _mgr.RemoveNamespace(prefix, ns);
        }

        public IEnumerable<XAttribute> GetAll()
        {
            return _mgr
                .GetNamespacesInScope(XmlNamespaceScope.Local)
                .Select(_ns => String.IsNullOrEmpty(_ns.Key) 
                    ? new XAttribute("xmlns", _ns.Value) 
                    : new XAttribute(XNamespace.Xmlns + _ns.Key, _ns.Value));
        }

        public XNamespace Get(string prefix)
        {
            var result = _mgr.LookupNamespace(prefix);
            return result;
        }
    }
}