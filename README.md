# Trees

## A C# Tree library

This library implements the basic set of typed trees found in computer science.

## Implementations

The current list of *mostly* implemented trees are
- Binary trees

The future list of trees to be implemented include
- Red/black trees

All tree implementations are fully functional and can be used as is. Implementations all fully implement ```ICollection```. Basic functionality is centered around the default ```IComparable``` implementations for the various types, and will use these compare methods when appropriate.
At this point, no LINQ extention methods for the implementations are included.

## Interface segregation

All objects implement either the ```ITree``` or ```ILeaf``` interfaces. These interfaces expose a limited and common set of operations in addition to ```ICollection```, like traversing the data structure or finding a specific item in the data structure. All implementations also implement the standard .NET interface ```ICollection``` and makes instances of ```IEnumerable``` available to the user.

## Extendibility

Implementations take ```Expressions``` or ```IComparer``` instances as parameters where applicable, so you can easily specify how your tree should perform branching without having to extend any classes.

## No LINQ

Except for the ```Expression``` parameters, which are compiled into the related lambda function only once, there are no LINQ used in this library. This is to not facilitate abstracted processing by more abstracted processing.

## Tests

A full suite of unit tests that illustrates all the available functionality is included.