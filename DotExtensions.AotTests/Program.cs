using AlastairLundy.DotExtensions.Numbers;
using AlastairLundy.DotExtensions.Strings;
using AlastairLundy.DotExtensions.Memory.Spans;
using DotExtensions.AotTests.Localizations;
// ReSharper disable LocalizableElement

// This program intentionally exercises a small subset of the public APIs from the
// libraries to help validate that they are analyzable and buildable in AoT scenarios.
// Keep this code free of reflection and dynamic features to remain AoT-friendly.

Console.WriteLine(Resources.DotExtensions_AoT_Messages_Intro);

// Strings
string sample = "hello world, world!";
string removedFirst = sample.RemoveFirst("world");
string removedAll = sample.RemoveAll("l");
Console.WriteLine($"RemoveFirst => {removedFirst}");
Console.WriteLine($"RemoveAll   => {removedAll}");

// Numbers
int digits = 123456.CountNumberOfDigits();
Console.WriteLine($"Digits in 123456 => {digits}");

// Memory/Spans
int[] data = { 1, 2, 3, 4, 5 };
var ro = data.AsReadOnly();
Console.WriteLine($"ReadOnly length => {ro.Count}");

Span<int> span = data.AsSpan();
span.Resize(7); // Ensure the resize extension is referenced
span[5] = 6;
span[6] = 7;
Console.WriteLine($"Span last two => {span[5]}, {span[6]}");

Console.WriteLine(Resources.DotExtensions_AoT_Messages_Outro);
