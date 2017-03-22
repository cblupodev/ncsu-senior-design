###alias
Client code uses alias to reference the SDK.

###assemblyChange
The name of the output assembly is changed between the SDK versions

###assemblyMerge
The old SDK uses mutiple assemblies (dll files), but those are merged into a single assembly (dll file) in the new SDK.

###assemblySplit
The old SDK uses a single assembly (dll file), but that's split into two assemblies (dll files) in the new SDK.

###basic
Class name changes between versions.

###basicNamespace
Root namespace changes between versions.

###casting
The client code casts to a class in the SDK.

###classInClass
The SDK has a class defined inside of a class.

###emptyDLL
The SDK has an empty project, meaning a DLL with no classes.

###extendsSDKClass
The client code extends an SDK class.

###extraLibrary
The client code uses a dll file that's not from the SDK.

###fullyQualified
Client uses fully qualified name for SDK class (rather than using a 'using' statement).

###fullyQualifiedModelIdentifier
The ModelIdentifier in the new and old SDK uses the full path of the ModelIdentifier, rather than relying on using/namespace sharing.

###generics
The client code uses the SDK class as the type qualifier for a generic class.

###instantiatesSDKClass
The client creates a new instance of an SDK class.

###methodCallOnParameter
This tests that we can handle changeing (SDK.Sample1.Test.method()) into (SDK.Change1.Test.method())

###multiAssembly
The SDK uses multiple assemblies (dll files).

###multiBasic
Multiple namespaces, files, and classes per file in SDK.  Multiple files and classes per file in client.

###multiTypeGeneric
The client uses Dictionary<Sample1, Sample1>.

###namespaceInNamespace
The SDK has a namespace directive inside of a namespace directive, creating an implicit nesting.

###newConflicts
Client code has class name matching a class name in the new SDK.

###nonRootUsing
The client's "using SDK" statement is inside of the namespace block.  This test only applies to the C# client, the VB client only exists for consistency

###nothing
There is no difference between the SDK versions.

###oldConflicts
Client code has a class name matching a class name in the old SDK.

###returnClass
Client method has a return type of an SDK class.

###typeof
The client uses the typeof expression with an SDK class as the parameter.

###unused
Client code does not use the SDK.  Tests that we're not making assumptions about a first element existing.

##variables
Client has variables typed with SDK classes.