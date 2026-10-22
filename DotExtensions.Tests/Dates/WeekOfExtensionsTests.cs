using System;
using System.Globalization;
using DotExtensions.Dates;

namespace DotExtensions.Tests.Dates;

public class WeekOfExtensionsTests
{
    // Cultures are pinned explicitly in every test; no test reads ambient culture.
    private static readonly CultureInfo Invariant = CultureInfo.InvariantCulture;
    private static readonly CultureInfo MondayStart = new("en-GB");
    private static readonly CultureInfo SundayStart = new("en-US");

    public record LeadingPartialWeekCase(string Label, CultureInfo Culture, DateOnly Date);

    public static IEnumerable<LeadingPartialWeekCase> LeadingPartialWeekCases()
    {
        yield return new("invariant, September 2026", Invariant, new DateOnly(2026, 9, 1));
        yield return new("invariant, January 2026", Invariant, new DateOnly(2026, 1, 1));
        yield return new("Monday-start (en-GB), September 2026", MondayStart, new DateOnly(2026, 9, 1));
        yield return new("Monday-start (en-GB), January 2026", MondayStart, new DateOnly(2026, 1, 1));
        yield return new("Sunday-start (en-US), September 2026", SundayStart, new DateOnly(2026, 9, 1));
        yield return new("Sunday-start (en-US), January 2026", SundayStart, new DateOnly(2026, 1, 1));
    }

    [Test]
    [MethodDataSource(nameof(LeadingPartialWeekCases))]
    public async Task WeekOfMonth_ReturnsZero_ForDateBeforeCulturesFirstWeekStart(LeadingPartialWeekCase testCase)
    {
        // 2026-09-01 is a Tuesday and 2026-01-01 a Thursday: both precede the first
        // week start of every pinned culture above.
        await Assert.That(testCase.Date.WeekOfMonth(testCase.Culture)).IsEqualTo(0);
    }

    [Test]
    public async Task WeekOfMonth_InvariantCulture_ReturnsKnownWeekCounts()
    {
        // Invariant culture weeks start on Sunday; September 2026 starts on a Tuesday
        // and its first Sunday is the 6th.
        await Assert.That(new DateOnly(2026, 9, 1).WeekOfMonth(Invariant)).IsEqualTo(0);
        await Assert.That(new DateOnly(2026, 9, 5).WeekOfMonth(Invariant)).IsEqualTo(0);
        await Assert.That(new DateOnly(2026, 9, 6).WeekOfMonth(Invariant)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 9, 8).WeekOfMonth(Invariant)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 9, 13).WeekOfMonth(Invariant)).IsEqualTo(2);
        await Assert.That(new DateOnly(2026, 9, 30).WeekOfMonth(Invariant)).IsEqualTo(4);

        // November 2026 starts exactly on the culture's first week start.
        await Assert.That(new DateOnly(2026, 11, 1).WeekOfMonth(Invariant)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 11, 2).WeekOfMonth(Invariant)).IsEqualTo(1);
    }

    [Test]
    public async Task WeekOfMonth_MondayStartCulture_ReturnsKnownWeekCounts()
    {
        // en-GB weeks start on Monday; September 2026 starts on a Tuesday and its
        // first Monday is the 7th, so Sunday the 6th is still week 0.
        await Assert.That(new DateOnly(2026, 9, 1).WeekOfMonth(MondayStart)).IsEqualTo(0);
        await Assert.That(new DateOnly(2026, 9, 6).WeekOfMonth(MondayStart)).IsEqualTo(0);
        await Assert.That(new DateOnly(2026, 9, 7).WeekOfMonth(MondayStart)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 9, 8).WeekOfMonth(MondayStart)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 9, 13).WeekOfMonth(MondayStart)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 9, 14).WeekOfMonth(MondayStart)).IsEqualTo(2);
        await Assert.That(new DateOnly(2026, 9, 30).WeekOfMonth(MondayStart)).IsEqualTo(4);

        // November 2026 starts on a Sunday: still week 0 under a Monday start.
        await Assert.That(new DateOnly(2026, 11, 1).WeekOfMonth(MondayStart)).IsEqualTo(0);
        await Assert.That(new DateOnly(2026, 11, 2).WeekOfMonth(MondayStart)).IsEqualTo(1);
    }

    [Test]
    public async Task WeekOfMonth_SundayStartCulture_ReturnsKnownWeekCounts()
    {
        await Assert.That(new DateOnly(2026, 9, 1).WeekOfMonth(SundayStart)).IsEqualTo(0);
        await Assert.That(new DateOnly(2026, 9, 6).WeekOfMonth(SundayStart)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 9, 8).WeekOfMonth(SundayStart)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 9, 13).WeekOfMonth(SundayStart)).IsEqualTo(2);
        await Assert.That(new DateOnly(2026, 9, 30).WeekOfMonth(SundayStart)).IsEqualTo(4);
    }

    [Test]
    public async Task WeekOfMonth_UsesThePassedCulture_OverAnyAmbientCulture()
    {
        DateOnly date = new(2026, 9, 13);

        // The two Sunday-first cultures agree; the Monday-first culture counts a
        // different number of elapsed week starts for the same date.
        await Assert.That(date.WeekOfMonth(Invariant)).IsEqualTo(date.WeekOfMonth(SundayStart));
        await Assert.That(date.WeekOfMonth(Invariant)).IsNotEqualTo(date.WeekOfMonth(MondayStart));
    }

    [Test]
    public async Task WeekOfYear_InvariantCulture_ReturnsKnownWeekCounts()
    {
        // Invariant culture: Sunday week start, CalendarWeekRule.FirstDay, so
        // 2026-01-01 is already in week 1 and the first Sunday (01-04) starts week 2.
        await Assert.That(new DateOnly(2026, 1, 1).WeekOfYear(Invariant)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 1, 4).WeekOfYear(Invariant)).IsEqualTo(2);
        await Assert.That(new DateOnly(2026, 6, 15).WeekOfYear(Invariant)).IsEqualTo(25);
        await Assert.That(new DateOnly(2026, 12, 31).WeekOfYear(Invariant)).IsEqualTo(53);
    }

    [Test]
    public async Task WeekOfYear_SundayStartCulture_ReturnsKnownWeekCounts()
    {
        await Assert.That(new DateOnly(2026, 1, 1).WeekOfYear(SundayStart)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 1, 4).WeekOfYear(SundayStart)).IsEqualTo(2);
        await Assert.That(new DateOnly(2026, 6, 15).WeekOfYear(SundayStart)).IsEqualTo(25);
        await Assert.That(new DateOnly(2026, 12, 31).WeekOfYear(SundayStart)).IsEqualTo(53);
    }

    [Test]
    public async Task WeekOfYear_MondayStartCulture_ReturnsKnownWeekCounts()
    {
        // en-GB: Monday week start, CalendarWeekRule.FirstFourDayWeek. 2026-01-01 is a Thursday,
        // so the week Mon 2025-12-29 .. Sun 2026-01-04 contains four days of 2026 and is week 1 -
        // matching Calendar.GetWeekOfYear (BCL returns 1 here, never 0). 01-05 (Monday) opens
        // the second week of the year.
        await Assert.That(new DateOnly(2026, 1, 1).WeekOfYear(MondayStart)).IsEqualTo(1);
        await Assert.That(new DateOnly(2026, 1, 5).WeekOfYear(MondayStart)).IsEqualTo(2);
        await Assert.That(new DateOnly(2026, 6, 15).WeekOfYear(MondayStart)).IsEqualTo(25);
        await Assert.That(new DateOnly(2026, 12, 31).WeekOfYear(MondayStart)).IsEqualTo(53);
    }

    // Monday + FirstFourDayWeek is exactly the ISO 8601 week rule, so these are documented
    // calendar facts (all verified against Calendar.GetWeekOfYear):
    // - 2026-01-01 (Thursday) lies in the week Mon 2025-12-29 .. Sun 2026-01-04, which holds four
    //   days of 2026 -> week 1 of 2026.
    // - 2021-01-01 (Friday) lies in ISO week 53 of 2020 (2020-W53 = Dec 28 2020 .. Jan 3 2021) -> 53.
    // - 2021-01-04 (Monday) opens ISO week 1 of 2021 -> 1.
    // - 2024-01-01 (Monday) is itself the ISO week-1 Monday of 2024 -> 1.
    [Test]
    public async Task WeekOfYear_MondayFirstFourDayWeek_MatchesDocumentedIsoWeekFacts()
    {
        await Assert.That(new DateOnly(2026, 1, 1).WeekOfYear(MondayStart)).IsEqualTo(1);
        await Assert.That(new DateOnly(2021, 1, 1).WeekOfYear(MondayStart)).IsEqualTo(53);
        await Assert.That(new DateOnly(2021, 1, 4).WeekOfYear(MondayStart)).IsEqualTo(1);
        await Assert.That(new DateOnly(2024, 1, 1).WeekOfYear(MondayStart)).IsEqualTo(1);
    }

    [Test]
    public async Task WeekOfYear_TakesItsRuleFromThePassedCulturesCalendarWeekRule()
    {
        // 2024 starts on a Monday, so with the invariant culture's Sunday week start
        // the three CalendarWeekRule values give three distinguishable answers.
        DateOnly january1st = new(2024, 1, 1);
        DateOnly january4th = new(2024, 1, 4);
        DateOnly january7th = new(2024, 1, 7);

        CultureInfo firstDay = WithCalendarWeekRule(CalendarWeekRule.FirstDay);
        CultureInfo firstFourDayWeek = WithCalendarWeekRule(CalendarWeekRule.FirstFourDayWeek);
        CultureInfo firstFullWeek = WithCalendarWeekRule(CalendarWeekRule.FirstFullWeek);

        // FirstDay: the partial week containing Jan 1 is week 1 until the first Sunday (Jan 7).
        await Assert.That(january1st.WeekOfYear(firstDay)).IsEqualTo(1);
        await Assert.That(january4th.WeekOfYear(firstDay)).IsEqualTo(1);
        await Assert.That(january7th.WeekOfYear(firstDay)).IsEqualTo(2);

        // FirstFourDayWeek: the leading week Sun Dec 31 2023 .. Sat Jan 6 2024 holds six days of
        // 2024, so it is already week 1 (BCL: 1, not 0); Jan 7 opens week 2.
        await Assert.That(january1st.WeekOfYear(firstFourDayWeek)).IsEqualTo(1);
        await Assert.That(january4th.WeekOfYear(firstFourDayWeek)).IsEqualTo(1);
        await Assert.That(january7th.WeekOfYear(firstFourDayWeek)).IsEqualTo(2);

        // FirstFullWeek: the first full week of 2024 starts Sun Jan 7, so Jan 1-6 belong to the
        // previous year's trailing week (2023 has 53 such weeks here - BCL: 53, not 0).
        await Assert.That(january1st.WeekOfYear(firstFullWeek)).IsEqualTo(53);
        await Assert.That(january4th.WeekOfYear(firstFullWeek)).IsEqualTo(53);
        await Assert.That(january7th.WeekOfYear(firstFullWeek)).IsEqualTo(1);
    }

    // The implementation must be indistinguishable from the BCL oracle for every rule and every
    // pinned culture. FirstDay already matched before the fix; FirstFourDayWeek/FirstFullWeek
    // previously returned 0 for days before week 1, where the BCL reports 1 or the prior year's
    // trailing 52/53.
    [Test]
    public async Task WeekOfYear_MatchesBclCalendarGetWeekOfYear_AcrossRulesAndCultures()
    {
        CultureInfo[] cultures =
        [
            Invariant,
            MondayStart,
            SundayStart,
            WithCalendarWeekRule(CalendarWeekRule.FirstDay),
            WithCalendarWeekRule(CalendarWeekRule.FirstFourDayWeek),
            WithCalendarWeekRule(CalendarWeekRule.FirstFullWeek),
        ];

        // A full year (covers pre-week-1 days in early January) plus two January windows that
        // straddle known 52/53-week year boundaries (2020/2021 and 2023/2024).
        List<DateOnly> dates = [];
        dates.AddRange(EnumerateDates(new DateOnly(2026, 1, 1), new DateOnly(2026, 12, 31)));
        dates.AddRange(EnumerateDates(new DateOnly(2020, 12, 20), new DateOnly(2021, 1, 14)));
        dates.AddRange(EnumerateDates(new DateOnly(2023, 12, 20), new DateOnly(2024, 1, 14)));

        List<string> mismatches = [];

        foreach (DateOnly date in dates)
        {
            foreach (CultureInfo culture in cultures)
            {
                int expected = culture.Calendar.GetWeekOfYear(
                    new DateTime(date.Year, date.Month, date.Day),
                    culture.DateTimeFormat.CalendarWeekRule,
                    culture.DateTimeFormat.FirstDayOfWeek);

                int actual = date.WeekOfYear(culture);

                if (actual != expected)
                {
                    mismatches.Add(
                        $"{date:yyyy-MM-dd} {culture.Name} {culture.DateTimeFormat.CalendarWeekRule}/{culture.DateTimeFormat.FirstDayOfWeek}: expected {expected}, actual {actual}");
                }
            }
        }

        // A non-empty join surfaces every failing date/culture combination in the assertion message.
        await Assert.That(string.Join(" | ", mismatches)).IsEqualTo(string.Empty);
    }

    private static IEnumerable<DateOnly> EnumerateDates(DateOnly start, DateOnly endInclusive)
    {
        for (DateOnly date = start; date <= endInclusive; date = date.AddDays(1))
        {
            yield return date;
        }
    }

    [Test]
    public async Task WeekOfYear_ReplicatesCultureRuleAndWeekStart_Exactly()
    {
        // A clone carrying only en-GB's CalendarWeekRule and FirstDayOfWeek must
        // reproduce en-GB's answers: those two culture fields are the whole contract.
        CultureInfo clone = (CultureInfo)Invariant.Clone();
        clone.DateTimeFormat.CalendarWeekRule = MondayStart.DateTimeFormat.CalendarWeekRule;
        clone.DateTimeFormat.FirstDayOfWeek = MondayStart.DateTimeFormat.FirstDayOfWeek;

        DateOnly[] dates =
        [
            new(2026, 1, 1),
            new(2026, 1, 5),
            new(2026, 6, 15),
            new(2026, 12, 31),
        ];

        foreach (DateOnly date in dates)
        {
            await Assert.That(date.WeekOfYear(clone)).IsEqualTo(date.WeekOfYear(MondayStart));
        }
    }

    private static CultureInfo WithCalendarWeekRule(CalendarWeekRule rule)
    {
        CultureInfo culture = (CultureInfo)Invariant.Clone();
        culture.DateTimeFormat.CalendarWeekRule = rule;

        return culture;
    }
}
