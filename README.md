# YatzyPoints
By Edvin Larsson

## Table of Contents
1. [NuGet Package Link](#nuget-package-link)
2. [Installation](#installation)
3. [Usage](#usage)
   - [Method: CategoriesWithHighestPoints](#method-categorieswithhighestpoints)
   - [Method: Points](#method-points)
4. [Practical Examples and Testing](#practical-examples-and-testing)


## NuGet Package Link

[NuGet Gallery | edvinpontuslarsson.yatzy.points 2.0.0](https://www.nuget.org/packages/edvinpontuslarsson.yatzy.points/2.0.0)

## Installation

The library can be installed using the command:
```bash
dotnet add package edvinpontuslarsson.yatzy.points --version 2.0.0
```
For alternative installation options, refer to the NuGet package link provided above.

## Usage

After installation, use the following line of code to access the public functionalities of the library:

```csharp
using static YatzyPoints.YatzyPoints;
```

The library contains one public class, "YatzyPoints", which includes one public enum and two public static methods.

The public enum "Categories" consists of the following:

- **ones**: Ones
- **twos**: Twos
- **threes**: Threes
- **fours**: Fours
- **fives**: Fives
- **sixes**: Sixes
- **pair**: Pair
- **two_pair**: Two Pairs
- **three_of_a_kind**: Three of a Kind
- **four_of_a_kind**: Four of a Kind
- **small_straight**: Small Straight
- **big_straight**: Big Straight
- **full_house**: Full House
- **chance**: Chance
- **yatzy**: Yatzy

The values in the enum represent possible categories for calculating the score of dice rolls.

### Method: CategoriesWithHighestPoints

This public static method calculates the category/categories that yield the highest points for a given set of dice:

```csharp
public static Dictionary<Categories, int> CategoriesWithHighestPoints(string eyes, Categories[]? excludeCategories = null)
```

The "eyes" string parameter must consist of numeric characters 1-6, separated by commas. Spaces are ignored and can be included or omitted. Invalid string formats will throw an exception.

The optional "excludeCategories" parameter can be used to exclude categories even if they provide the highest score. The method returns a Dictionary with categories as keys and their respective scores as values.

### Method: Points

This public static method calculates points given dice rolls and a selected category:

```csharp
public static int Points(string eyes, Categories category)
```

Refer to the "CategoriesWithHighestPoints" method description for the format of the "eyes" string parameter. Invalid formats will result in an exception.

## Practical Examples and Testing

For practical examples of how the library can be used, refer to the unit tests in the repository under the directory "TestYatzyPoints". These tests cover the functionality of the two public methods.
