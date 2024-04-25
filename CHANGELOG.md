# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).


## Known Bugs
- If you are inspecting the values of a script that has one of CinderUtil's custom attribute drawers, like `[Disabled]/[DisabledOnPlay]` when the patent object gets destroyed, you'll get the following Editor only exception: `SerializedObject target has been destroyed`.


## [Unreleased]
### Added
 - CollectionExtensions:
   - `ICollection.LoopingGet(int i)`
   - `IDictionary.GetValue(key, defaultValue = default)`
### Changed
### Removed
### Fixed



## [v1.1.2] - 2024-04-24
### Added
 - Added extension methods:
   - TypeExtensions:
     - `Type.Is<T>()`: Reflection alternative to `someObj is SomeType`. Usage would be `someObj.GetType().Is<SomeType>()`.



## [v1.1.1] - 2024-04-14
### Added
 - Added `CinderConfigurationMenu` class as general place to add menu entries for the package.
 - Added `CINDER_DEBUG` define that can be toggled with the `Enable Debug Mode`/`Disable Debug Mode` entries on the `CinderUtils` menu.
 - Created `CinderDebug` static class for general use. All the methods will be ignored without the `CINDER_DEBUG` define being set.

### Changed
 - Internal library logging now make use `CinderDebug` instead of `UnityEngine.Debug`, making it dependant on the `CINDER_DEBUG` define.



## [v1.1.0] - 2024-04-10
### Added
 - Created a generic event bus system under the `CinderUtils.Events` namespace:
   - To declare an event type, implement the `IEvent` interface for some type (be it a class or a struct).
   - Making use of the static `EventBus` class, you can `Register<T>(EventBinding<T> binding)` bindings to recieve events, or `Raise<T>(T event)` an event so that it gets propagated across bindings.
   - All events are sent through the `EventBus<T>` corresponding to the type parameter `T` of the `Raise<T>()`, tho you could pass in a an event object of a derived class through the base clases's channel if you cast the object.
   - To recieve events, you must create an `EventBinding<T>`, to which you register handler methods to the `OnEvent` and `OnEventNotify` events, and then `Register()` the binding on the `EventBus`. Make sure to `Deregister()` the binding when the object gets destroyed or disabled.
 - Added examples for the event system under `Samples~/Events`.
 - Created some Reflection utilities under the `CinderUtils.Reflection` namespace:
   - `AssemblyUtils` Used to retrieve Types from the game's Assemblies:
     - `PredefinedAssembly`: Used to be able to index into the `PredefinedAssemblyCache`.
     - `PredefinedAssemblyCache`: Easily accesible cache with the Assemblies of unity's predefined assemblies.
     - `GetSubtypesOf<T>(bool, ICollection<PredefinedAssembly>)`: Used to get all the subtypes of some Type `T`. Used internally to manage the `EventBus` cleanup.

### Changed
 - Moved basic code examples from `Samples~/Basic` to `Samples~/General`.



## [v1.0.2] - 2024-02-07
### Added
 - Added base class for custom property drawers: `CinderPropertyDrawer`.
 - Added extension methods:
   - EnumeratorExtensions:
     - `IEnumerator.ToEnumerable()`: Converts an IEnumerator to an IEnumerable.
   - TransformExtensions:
     - `Transform.Children()`: Returns an `IEnumerable` of all the child Transforms of that transform.
   - GameObjectExtensions:
     - `GameObject.OrNull()`: Returns the object or null if the object is null or deinitializing.
     - `GameObject.Children()`: Returns an `IEnumerable` of all the child GameObjects of that GameObject.

### Changed
 - Improved `[ConditionalField]` attributes to take values as `object`, without converting to `string` internally, this allows to check for `null`.
 - Improved examples for the `ConditionalFieldAttribute`.
 - Made custom property drawers (`[Disabled]`, `[DisabledOnPlay]`, `[ConditionalField]`) extend `CinderPropertyDrawer`.



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









[unreleased]: https://github.com/ash-dvlpr/cinder-utils/compare/v1.1.2...master
[v1.1.2]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.1.2
[v1.1.1]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.1.1
[v1.1.0]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.1.0
[v1.0.2]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.0.2
[v1.0.1]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.0.1
[v1.0.0]: https://github.com/ash-dvlpr/cinder-utils/releases/tag/v1.0.0