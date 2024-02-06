# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added
### Changed
### Removed
### Fixed


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









[unreleased]: https://github.com/ash-dvlpr/cinder-utils/compare/v1.0.0...HEAD
[v1.0.0]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.0.0