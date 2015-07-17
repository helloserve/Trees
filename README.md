# Trees

## A C# Tree library

This library implements the basic set of typed trees found in computer science.

## Implementations

The current list of implemented trees are
- Binary trees

The future list of trees to be impleted include
- Red/black trees

## Interface segregation

All objects implement either the ```ITree``` or ```ILeaf``` interfaces. These interfaces expose a limited and common set of operations, like adding an item to the data structure, traversing the data structure, or finding a specific item in the data structure.

## Extendibility

All implementations take ```Expressions``` or ```IComparer``` instances as parameters, so you can easily specify how your tree should perform branching without having to extend any classes.

## No LINQ

Except for the ```Expression``` parameters, which are compiled into the related lambda function only once, there are no LINQ used in this library. This is to not facilitate abstracted processing by more abstracted processing.
