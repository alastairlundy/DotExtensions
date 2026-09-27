# Glossary

## Safe Enumeration

Enumeration that skips inaccessible file-system entries by design and propagates enumeration faults instead of silently stopping mid-run.

## Guard

An argument-validation member whose only behaviour is throwing when content violates a predicate; the predicate's semantics are owned by the BCL contract it cites, never redefined locally.

## BCL-owned contract

A semantic contract defined and documented in the .NET base class library; the library does not re-export members that restate it.
