﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Purify;

namespace PUrify
{
    class PurifierMono : IPurifier
    {
        private static Type uriType = typeof(Uri);
        private static FieldInfo mono_sourceField;
        private static FieldInfo mono_queryField;
        private static FieldInfo mono_pathField;
        private static FieldInfo mono_cachedToStringField;
        private static FieldInfo mono_cachedAbsoluteUriField;

        static PurifierMono()
        {
            mono_sourceField = uriType.GetField("source", BindingFlags.NonPublic | BindingFlags.Instance);
            mono_queryField = uriType.GetField("query", BindingFlags.NonPublic | BindingFlags.Instance);
            mono_pathField = uriType.GetField("path", BindingFlags.NonPublic | BindingFlags.Instance);
            mono_cachedToStringField = uriType.GetField("cachedToString", BindingFlags.NonPublic | BindingFlags.Instance);
            mono_cachedAbsoluteUriField = uriType.GetField("cachedAbsoluteUri",
                BindingFlags.NonPublic | BindingFlags.Instance);
        }

        public void Purify(Uri uri)
        {
			//UriInfo will throw on Path/Host/Query access if the uri is Relative.
			if (!uri.IsAbsoluteUri)
				return;
            var source = (string)mono_sourceField.GetValue(uri);
            mono_cachedToStringField.SetValue(uri, source);
            

			mono_cachedAbsoluteUriField.SetValue(uri, source);
            var uriInfo = new UriInfo(uri, source);
            mono_pathField.SetValue(uri, uriInfo.Path);
            mono_queryField.SetValue(uri, uriInfo.Query);
        }
	}	
}
