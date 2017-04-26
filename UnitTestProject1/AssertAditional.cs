using EFSQLConnector;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest.WhiteBox
{
    class AssertAditional
    {
        public static void AssemblyMapEquals(assembly_map expected, assembly_map actual, string message)
        {
            Assert.IsNotNull(actual, message);
            var expectedText = "";
            var actualText = "";
            if (expected.id != 0)
            {
                expectedText += ", [id]:" + expected.id;
                actualText += ", [id]:" + actual.id;
            }
            if (expected.name != null)
            {
                expectedText += ", [name]:" + ToSafeString(expected.name);
                actualText += ", [name]:" + ToSafeString(actual.name);
            }
            if (expected.new_path != null )
            {
                expectedText += ", [new_path]:" + ToSafeString(expected.new_path);
                actualText += ", [new_path]:" + ToSafeString(actual.new_path);
            }
            if (expected.old_path != null)
            {
                expectedText += ", [old_path]:" + ToSafeString(expected.old_path);
                actualText += ", [old_path]:" + ToSafeString(actual.old_path);
            }
            if (expected.sdk_id != 0)
            {
                expectedText += ", [sdk_id]:" + expected.sdk_id;
                actualText += ", [sdk_id]:" + actual.sdk_id;
            }
            expectedText = expectedText.Substring(2);
            actualText = actualText.Substring(2);
            Assert.AreEqual(expectedText, actualText, message);
        }
        public static void NamespaceMapEquals(namespace_map expected, namespace_map actual, string message)
        {
            Assert.IsNotNull(actual, message);
            var expectedText = "";
            var actualText = "";
            if ( expected.id != 0 )
            {
                expectedText += ", [id]" + expected.id;
                actualText += ", [id]" + actual.id;
            }
            if (expected.new_namespace != null)
            {
                expectedText += ", [new_namespace]" + ToSafeString(expected.new_namespace);
                actualText += ", [new_namespace]" + ToSafeString(actual.new_namespace);
            }
            if (expected.old_namespace != null)
            {
                expectedText += ", [old_namespace]" + ToSafeString(expected.old_namespace);
                actualText += ", [old_namespace]" + ToSafeString(actual.old_namespace);
            }
            if (expected.sdk_id != 0)
            {
                expectedText += ", [sdk_id]" + expected.sdk_id;
                actualText += ", [sdk_id]" + actual.sdk_id;
            }
            expectedText = expectedText.Substring(2);
            actualText = actualText.Substring(2);
            Assert.AreEqual(expectedText, actualText, message);
        }
        public static void SDKMapEquals(sdk_map2 expected, sdk_map2 actual, string message)
        {
            Assert.IsNotNull(actual, message);
            var expectedText = "";
            var actualText = "";
            if ( expected.assembly_map_id != 0 )
            {
                expectedText += ", [assembly_map_id]" + expected.assembly_map_id;
                actualText += ", [assembly_map_id]" + actual.assembly_map_id;
            }
            if (expected.id != 0)
            {
                expectedText += ", [id]" + expected.id;
                actualText += ", [id]" + actual.id;
            }
            if (expected.model_identifier != null)
            {
                expectedText += ", [model_identifier]" + ToSafeString(expected.model_identifier);
                actualText += ", [model_identifier]" + ToSafeString(actual.model_identifier);
            }
            if (expected.namespace_map_id != 0)
            {
                expectedText += ", [namespace_map_id]" + expected.namespace_map_id;
                actualText += ", [namespace_map_id]" + actual.namespace_map_id;
            }
            if (expected.new_classname != null)
            {
                expectedText += ", [new_classname]" + ToSafeString(expected.new_classname);
                actualText += ", [new_classname]" + ToSafeString(actual.new_classname);
            }
            if (expected.old_classname != null)
            {
                expectedText += ", [old_classname]" + ToSafeString(expected.old_classname);
                actualText += ", [old_classname]" + ToSafeString(actual.old_classname);
            }
            if (expected.sdk_id != 0)
            {
                expectedText += ", [sdk_id]" + expected.sdk_id;
                actualText += ", [sdk_id]" + actual.sdk_id;
            }
            expectedText = expectedText.Substring(2);
            actualText = actualText.Substring(2);
            Assert.AreEqual(expectedText, actualText, message);

        }
        public static void SDKEquals(sdk2 expected, sdk2 actual, string message)
        {
            Assert.IsNotNull(actual, message);
            var expectedText = "";
            var actualText = "";
            if (expected.id != 0)
            {
                expectedText += ", [id]" + expected.id;
                actualText += ", [id]" + actual.id;
            }
            if (expected.name != null)
            {
                expectedText += ", [name]" + ToSafeString(expected.name);
                actualText += ", [name]" + ToSafeString(actual.name);
            }
            expectedText = expectedText.Substring(2);
            actualText = actualText.Substring(2);
            Assert.AreEqual(expectedText, actualText, message);
        }
        public static void SetEquals<T>(HashSet<T> expect, HashSet<T> actual, string message)
        {
            Assert.IsNotNull(actual, message);
            if (!actual.SetEquals(expect))
            {
                Assert.Fail("Expected Set:<" + string.Join(", ", expect.Select(x=>ToSafeString(x))) + ">. Actual Set:<" +
                    string.Join(", ", actual.Select(x => ToSafeString(x))) + ">. " + message);
            }
        }
        public static void DictionaryEquals<TKey,TValue>(Dictionary<TKey,TValue> expected,
            Dictionary<TKey, TValue> actual, string message)
        {
            Assert.IsNotNull(actual, message);
            var expectedText = string.Join(", ", expected.Select(x => "{" + ToSafeString(x.Key) + "=" + ToSafeString(x.Value) + "}"));
            var actualText = string.Join(", ", actual.Select(x => "{" + ToSafeString(x.Key) + "=" + ToSafeString(x.Value) + "}"));
            if ( expected.Count != actual.Count )
            {
                goto fail;
            }
            foreach (var kvp in expected)
            {
                TValue act;
                if (!actual.TryGetValue(kvp.Key, out act))
                    goto fail;
                if (!kvp.Value.Equals(act))
                    goto fail;
            }
            return;
            fail:
            Assert.Fail("Expected Dictionary:<" + expectedText + ">. Actual Dictionary:<" + actualText + ">. " + message);
        }
        public static void ListEquals<T>(List<T> expcted, List<T> actual, Func<T,T,bool> equals, string message)
        {
            Assert.IsNotNull(actual, message);
            Assert.AreEqual(expcted.Count, actual.Count, message);
            Assert.IsTrue(expcted.All(x=>actual.Any(y=>equals(x,y))), message);
        }
        public static string ToSafeString(object value)
        {
            if (value == null)
            {
                return "null";
            }
            else if (value.GetType() == typeof(string))
            {
                using (var writer = new System.IO.StringWriter())
                {
                    using (var provider = System.CodeDom.Compiler.CodeDomProvider.CreateProvider("CSharp"))
                    {
                        provider.GenerateCodeFromExpression(new System.CodeDom.CodePrimitiveExpression(value), writer, null);
                        return writer.ToString();
                    }
                }
            }
            else
            {
                return value.ToString();
            }
        }
    }
}
