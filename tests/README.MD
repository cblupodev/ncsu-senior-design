###alias
Client code uses alias to reference the SDK.

###assemblyChange
The name of the output assembly is changed between the SDK versions

###basic
Class name changes between versions.

###basicNamespace
Root namespace changes between versions.

###fullyQualified
Client uses fully qualified name for SDK class (rather than using a 'using' statement).

###newConflicts
Client code has class name matching a class name in the new SDK.

###nonRootUsing
The client's "using SDK" statement is inside of the namespace block.  This test only applies to the C# client, the VB client only exists for consistency

###nothing
There is no difference between the SDK versions.

###oldConflicts
Client code has a class name matching a class name in the old SDK.

###unused
Client code does not use the SDK.  Tests that we're not making assumptions about a first element existing.