# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
 - Added base class for custom property drawers: `CinderPropertyDrawer`.
 - Added extension methods:
   - EnumeratorExtensions:
     - `IEnumerator.ToEnumerable()`: Converts an IEnumerator to an IEnumerable.

### Changed
 - Improved `[ConditionalField]` attributes to take values as `object`, without converting to `string` internally, this allows to check for `null`.
 - Improved examples for the `ConditionalFieldAttribute`.
 - Made custom property drawers (`[Disabled]`, `[DisabledOnPlay]`, `[ConditionalField]`) extend `CinderPropertyDrawer`.
 - Made the `[DisabledOnPlay]` attribute reuse `[Disabled]`'s code.

### Removed
### Fixed


## [v1.0.1] - 2024-02-06
### Added
 - Added basic code examples under `Samples~/Basic`.
 - Added `UPM Git Extension` to package dependencies.

### Changed
 - Updated `.gitignore` to ignore more IDE cache directories (`.vscode` and `.idea`).
 - Updated keywords on the `package.json`.



## [v1.0.0] - 2024-02-06

### Added
 - Created basic unity package with AssemblyDefinitions for the `Editor` and `Runtime` code.
 - The namespace for the `Runtime` AssemblyDefinition is: `CinderUtils`.
 - The namespace for the `Editor` AssemblyDefinition is: `CinderUtils.Editor`.
 - Added utility functions via extension methods under the `CinderUtils.Extensions` namespace.
   - CollectionExtensions:
     - `ICollection.GetRandom()`
   - EnumeratorExtensions:
     - `IEnumerable.NullOrEmpty()`
     - `IEnumerable.ForEach(Action action)`
     - `IEnumerable.ToDebugString()`
 - Implemented some utility attributes with `CustomPropertyDrawer` to enable more editor customization.
   - `[Disabled]`: Disables the field on the Editor, while still being visible.
   - `[DisabledOnPlay]`: Disables the field on the Editor while the Application is running, or the Editor is changing states.
   - `[ConditionalField]`: Shows the anotated field only if the value of the target field matches one of the passed values, otherwise it will be hidden. This behaviour can be reversed with the `inverse` flag. NOTE: It's an `abstact` class with `internal` constructor, use `[ShowIf]` and `[HideIf]` instead.
   - `[ShowIf]` and `[HideIf]`, which are variants of `[ConditionalField]` that set the `inverse` flag accordingly.









[unreleased]: https://github.com/ash-dvlpr/cinder-utils/compare/v1.0.1...HEAD
[v1.0.1]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.0.1
[v1.0.0]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.0.0